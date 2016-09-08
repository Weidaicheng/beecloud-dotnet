using BeeCloud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeCloudSDKDemo
{
    public partial class auth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bool result = BCPay.BCAuthentication("xxx", "xxxxxxxxxxxxxxxxxxxxxxx", null, null);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result + "</span><br/>");
            }
            catch (Exception excption)
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
            }
        }
    }
}