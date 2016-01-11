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
    public class HostsController : ApiController
    {
        public IEnumerable<String> Get(string servers)
        {
            HostData H = new HostData();
            return H.GetFirstHostContent(servers);
            
        
        }
        public IEnumerable<string> Get()
        {
            string[] I ={ "Please send 1 server name at least" };
            return I;
        }
    }
}
