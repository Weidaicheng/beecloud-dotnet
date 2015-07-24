using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeCloudSDKDemo
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BeeCloud.BeeCloud.registerApp("5b24c3c9-9491-4cdf-95ac-ddeee4aa5cec", "11069a6b-1170-4b98-a420-d685fd229fe9");
        }
    }
}