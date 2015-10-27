using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BeeCloud;
using BeeCloud.Model;

namespace BeeCloudSDKDemo.PAYPAL
{
    public partial class PAYPAL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BCPayResult result = BCPay.BCInternationalPay(BCPay.InternationalPay.PAYPAL_PAYPAL.ToString(), 1, BCUtil.GetUUID(), "dotnet paypal", "USD", null, null, "http://localhost:50003/paypal/return_paypal_url.aspx");
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
            if (result.resultCode == 0)
            {
                BCPayPalResult payResult = result as BCPayPalResult;
                Response.Write("<a href=" + payResult.url + ">付款地址</a><br/>");
            }
        }
    }
}