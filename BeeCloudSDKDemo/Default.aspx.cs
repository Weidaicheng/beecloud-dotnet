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
            //本demo账号支付宝易宝快钱无法在生产环境测试
            BeeCloud.BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "e14ae2db-608c-4f8b-b863-c8c18953eef2", "4bfdd244-574d-4bf3-b034-0c751ed34fee");
            //设置为false进入生产环境，设置为true代表测试环境
            BeeCloud.BeeCloud.setTestMode(true);
        }
    }
}