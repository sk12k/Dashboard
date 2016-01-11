using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using iControl;
using DashboardAPI.Dal;
using System.Xml;
using System.Data;
namespace DashboardAPI.Models
{
    [Serializable]
    [DataContract]
    public class LBData
    {
        public static int count = 0;
        [DataMember (Name="ApplicationName",Order=0)]
        public string ApplicationName { get; set; }
        [DataMember(Name = "DC", Order = 1)]
        public IEnumerable<DCName> DataCenters { get; set; }
        public LBData()
        {
            
        }
        public LBData(int LogicalID)
        {
            DataSet D = new DataSet();
            D = new BLLBDBDal().GetPoolDetails(LogicalID);
            F5LoadBalancer PoolsMemberStatusFinder = new F5LoadBalancer();
            DataCenters =   PoolsMemberStatusFinder.FindPoolMemberList(D);
            ApplicationName = LogicalID.ToString();
        }
    }
        public class DCName
        {
            [DataMember(Name = "DC", Order = 1)]
            public string DataCenterName { get; set; }
            [DataMember(Name = "ProdNodes", Order = 0, IsRequired=false)]
            public Dictionary<string, string> ProdNodes { get; set; }
            [DataMember(Name = "StgNodes", Order = 1, IsRequired = false)]
            public Dictionary<string, string> StgNodes { get; set; }
            public DCName()
            {

            }
            public DCName(string DCName)
            {
                
            }
        }        
        
    public class F5LoadBalancer
    {
        public LocalLBPoolMemberMemberSessionStatus[][] Status;
        public iControl.Interfaces LoadAll = new iControl.Interfaces();
        public bool LoginUser(string URI, string username, string ipassword)
        {
            URI  = this.URLFinder(URI);
            LoadAll.ConnectionInfo.clear();
            bool bInitialized = LoadAll.initialize(URI, Constants.username,Constants.password);
            return bInitialized;
        }
        public string URLFinder(string DCLB)
        {
            //DCLB="SJV/PCI";
            XmlDocument XD = new XmlDocument();
            XD.Load(Constants.LBURLPath);
           // XD.Load(@"C:\Users\ndhir1\documents\visual studio 2012\Projects\BLContainer\BLContainer\LBURL.xml");
            string DCZone = Constants.primarynode + "/" + DCLB;
            XmlNode DC = XD.SelectSingleNode(DCZone);
            string URI = DC.SelectSingleNode("Primary").Attributes.GetNamedItem("URI").Value;
            return URI;
        }
        public DCName[] FindPoolMemberList(DataSet D)
        {
            XmlDocument XD = new XmlDocument();
            XD.Load(Constants.DCZoneInfo);
            XmlNode ZoneInfo = XD.SelectSingleNode("DCZone").SelectSingleNode("ZoneInfo");
            XmlNodeList Zones = ZoneInfo.SelectNodes("Zone");
            Dictionary<int, string> LBZoneData = new Dictionary<int, string>();
            foreach (XmlNode Zone in Zones)
            {
                LBZoneData.Add(Convert.ToInt32(Zone.Attributes.GetNamedItem("ID").Value), Zone.Attributes.GetNamedItem("Name").Value);
            }
            XmlNode DCInfo = XD.SelectSingleNode("DCZone").SelectSingleNode("DCInfo");
            XmlNodeList DCs = DCInfo.SelectNodes("DC");
            Dictionary<int, string> LBDCData = new Dictionary<int, string>();
            foreach (XmlNode DC in DCs)
            {
                LBDCData.Add(Convert.ToInt32(DC.Attributes.GetNamedItem("ID").Value), DC.Attributes.GetNamedItem("Name").Value);
            }
            int i = 0;
            List<DCName> DCDatas = new List<DCName>();
            Dictionary<string, List<string>> LBURLPoolList = new Dictionary<string, List<string>>();
            Dictionary<string, string> PoolType = new Dictionary<string, string>();
            List<string> DCZone = new List<string>();
            List<string> Poollist = new List<string>();
            foreach (DataTable Table in D.Tables)
            {
                Poollist.Clear();
                foreach (DataRow row in Table.Rows)
                {
                    string temp;
                    temp = LBDCData[Convert.ToInt32(row[1].ToString())] + "/" + LBZoneData[Convert.ToInt32(row[1].ToString())];
                    
                    if (!DCZone.Contains(temp))
                    {
                        DCZone.Add(temp);
                        Poollist.Add(row[0].ToString());
                        LBURLPoolList.Add(temp,Poollist);
                    }
                    else
                    {
                        LBURLPoolList[temp].Add(row[0].ToString());
                    }
                        if (Convert.ToInt32(row[3]) == 1)
                        {
                            PoolType.Add(row[0].ToString(), "Prod");
                        }
                        else if (Convert.ToInt32(row[3]) == 2)
                        {
                            PoolType.Add(row[0].ToString(), "Stg");
                        }
                   
                   
                }
           }
            Dictionary<string, string> ScreenNameStatus = FindNodeStatusinPool(LBURLPoolList);
            int counter = 0;
            DCName DCData;
           
               DCData = new DCName();
               DCData.ProdNodes = new Dictionary<string, string>();
               DCData.StgNodes = new Dictionary<string, string>();

                for (i = 0; i < PoolType.Count; i++)
                {
                    
                    for (counter = 0; counter < this.Status[i].Length; counter++)
                    {                       
                            if (i == 0 & counter == 0)
                                DCData.DataCenterName = ScreenNameStatus[this.Status[i][counter].member.address].Substring(0,3).ToUpper();
                            if (PoolType[Poollist[i]] == "Prod" && !DCData.ProdNodes.ContainsKey(ScreenNameStatus[this.Status[i][counter].member.address.ToString()]))
                                DCData.ProdNodes.Add(ScreenNameStatus[this.Status[i][counter].member.address.ToString()], this.Status[i][counter].session_status.ToString());
                            else if (PoolType[Poollist[i]] == "Stg" & !DCData.StgNodes.ContainsKey(ScreenNameStatus[this.Status[i][counter].member.address.ToString()]))
                                DCData.StgNodes.Add(ScreenNameStatus[this.Status[i][counter].member.address.ToString()], this.Status[i][counter].session_status.ToString());                       
                    }
                }
                DCDatas.Add(DCData);
                
            
            return DCDatas.ToArray();
        }
        public Dictionary<string,string> FindNodeStatusinPool(Dictionary<string,List<string>> PoolsinURL)
        {
            Dictionary<string, string> ScreenNameStatus = new Dictionary<string, string>();
            List<string> Addresses = new List<string>();
            foreach (var item in PoolsinURL)
            {
               
                if (this.LoginUser(item.Key.ToString(), Constants.username, Constants.password))
                {
                    //CommonIPPortDefinition[][] MemberList = LoadAll.LocalLBPool.get_member(item.Value.ToArray());
                    foreach (string val in item.Value.ToArray())
                    {
                        if (val.Contains("runway") | val.Contains("rwy"))
                        {
                            item.Value.Remove(val);
                        }
                    }
                    Status =  LoadAll.LocalLBPoolMember.get_session_status(item.Value.ToArray());
                    foreach (LocalLBPoolMemberMemberSessionStatus[] Pools in Status)
                    { 
                       foreach (LocalLBPoolMemberMemberSessionStatus Pool in Pools)
                        {
                           if(!Addresses.Contains(Pool.member.address))
                            Addresses.Add(Pool.member.address);
                        }
                    }
                    string[] MemberName = LoadAll.LocalLBNodeAddress.get_screen_name(Addresses.ToArray());
                    int i=0;
                    foreach (string Member in Addresses.ToArray())
                    {
                        ScreenNameStatus.Add(Addresses[i].ToString(), MemberName[i++].Split('/')[2]);
                    }

                }     
      
            }
            return ScreenNameStatus;
        }
    }
}