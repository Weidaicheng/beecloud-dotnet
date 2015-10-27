using BeeCloud;
using BeeCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeCloudSDKDemo.PAYPAL
{
    public partial class payWithSavedCreditcard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //这里填入你在信用卡付款后获得的信用卡id。
            BCPayResult result = BCPay.BCInternationalPay(BCPay.InternationalPay.PAYPAL_SAVED_CREDITCARD.ToString(), 1, BCUtil.GetUUID(), "dotnet paypal", "USD", null, "CARD-1K997489XXXXXXXXXXXXXXX", null);
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
        }
    }
}