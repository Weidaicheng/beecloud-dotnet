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
    public partial class payWithCreditCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cardNo = Request.Form["cardNo"];
            string expire = Request.Form["expire"];
            string cvv = Request.Form["cvv"];
            string firstName = Request.Form["firstName"];
            string lastName = Request.Form["lastName"];
            string cardType = Request.Form["cardType"];
            BCCreditCardInfo info = new BCCreditCardInfo();
            info.card_number = cardNo;
            info.expire_month = int.Parse(expire.Split('-')[1]);
            info.expire_year = int.Parse(expire.Split('-')[0]);
            info.cvv = int.Parse(cvv);
            info.first_name = firstName;
            info.last_name = lastName;
            info.card_type = cardType;
            BCPayResult result = BCPay.BCInternationalPay(BCPay.InternationalPay.PAYPAL_CREDITCARD.ToString(), 1, BCUtil.GetUUID(), "dotnet paypal", "USD", info, null, null);
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
            if (result.resultCode == 0)
            {
                BCPayPalResult payResult = result as BCPayPalResult;
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.creditCardId + "</span><br/>");
            }
        }
    }
}