using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DashboardAPI.Models;
using DashboardAPI.Dal;
using DashboardAPI.Models;

namespace DashboardAPI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            new F5LoadBalancer().FindVIPforPool();
        }
    }
}