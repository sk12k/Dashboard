using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using DashboardAPI.Dal;


namespace DashboardAPI.Dal
{
    public class BLLBDBDal
    {
        public SqlConnection ConnectionStr;
        public SqlCommand cmd;
        public CommandType CmdType;
        List<SqlParameter> Params;
        public string[] result;
        string cmdText;

        public DataSet GetApplicationDetails(string apps)
        {
            //ConnectionStr = new SqlConnection(Constants.DBConn);
            CmdType = CommandType.StoredProcedure;
            SqlParameter Sq = new SqlParameter();
            Sq.SqlDbType = SqlDbType.TinyInt;
            Sq.ParameterName = "@DCID";
            Sq.SqlValue = 2;
            SqlParameter Sq1 = new SqlParameter();
            Sq1.SqlDbType = SqlDbType.TinyInt;
            Sq1.ParameterName = "@LID";
            Sq1.SqlValue = 151;
            SqlParameter Sq2 = new SqlParameter();
            Sq2.SqlDbType = SqlDbType.TinyInt;
            Sq2.ParameterName = "@PoolTypeID";
            Sq2.SqlValue = 1;
            Params = new List<SqlParameter>();
            Params.Add(Sq1);
            Params.Add(Sq2);
            Params.Add(Sq);
            cmdText = "GetMember";
            DataSet D = PrepareandExecuteCommand(cmd, CmdType, cmdText, Params.ToArray());
            return D;
        }
        private static DataSet PrepareandExecuteCommand(SqlCommand cmd, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            //if (conn.State != ConnectionState.Open)
            //    conn.Open();
            SqlConnection conn = new SqlConnection(Constants.DBConn);
            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
            try
            {
                SqlDataAdapter dp = new SqlDataAdapter();
                SqlDataReader r = null;
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                conn.Open();
                //r = cmd.ExecuteReader();
                dp.SelectCommand = cmd;
                dp.Fill(ds);
             //  int Re =   cmd.ExecuteNonQuery();
                conn.Close();
                List<string> result = new List<string>();
               // result.Add(cmd.Parameters["@IP"].Value.ToString());
                //result.Add(cmd.Parameters["@Port"].Value.ToString());
               
               
                return ds;
            }
            catch (Exception E)
            {
                string Err = E.StackTrace.ToString();
                conn.Close();
                return new DataSet();
            }
        }
        public DataSet GetPoolDetails(int LogicalID)
        {
            Params = new List<SqlParameter>();
            SqlParameter SQ = new SqlParameter();
            SQ.ParameterName = "@LogicalID";
            SQ.SqlDbType = SqlDbType.Int;
            SQ.SqlValue = LogicalID;
            Params.Add(SQ);
            CmdType = CommandType.StoredProcedure;
            cmdText= "GetPools";
            DataSet D = PrepareandExecuteCommand(cmd, CmdType, cmdText, Params.ToArray());
            return D;
        }
    }
}