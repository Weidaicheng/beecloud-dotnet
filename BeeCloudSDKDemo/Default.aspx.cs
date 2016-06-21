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
            BeeCloud.BeeCloud.registerApp("e66e760b-0f78-44bb-a9ae-b22729d51678", "6fb7db77-96ed-46ef-ae10-1118ee564dd3", "97ca13e4-6f40-4790-9734-ddcdc1da21db", "a1900cf2-2570-49a3-bfb8-c6e7a1bc1e21");
            //设置为false进入生产环境，设置为true代表测试环境
            BeeCloud.BeeCloud.setTestMode(false);
        }
    }
}