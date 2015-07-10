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
            BeeCloud.BeeCloud.registerApp("0950c062-5e41-44e3-8f52-f89d8cf2b6eb", "a5571c5a-591e-4fb9-bd92-0283782af00d");
        }
    }
}