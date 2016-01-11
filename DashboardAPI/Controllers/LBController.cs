using DashboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using iControl;

namespace DashboardAPI.Controllers
{
    public class LBController : ApiController
    {
        public IEnumerable<LBData> Get(int ID)
        {
            LBData[] L = new LBData[1];
            L[0] = new LBData(ID);        
            return L;   
        }
        public IEnumerable<LBData> Get()
        {
            LBData[] L = new LBData[4];
            L[0] = new LBData(151);
            L[1] = new LBData(139);
            L[2] = new LBData(250);
            L[3] = new LBData(131);
            return L;
        }
    }
}
