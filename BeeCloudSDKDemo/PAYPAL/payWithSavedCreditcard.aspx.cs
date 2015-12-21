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
            BCInternationlBill bill = new BCInternationlBill(BCPay.InternationalPay.PAYPAL_SAVED_CREDITCARD.ToString(), 1, BCUtil.GetUUID(), "dotnet paypal", "USD");
            bill.creditCardId = "CARD-1K997489XXXXXXXXXXXXXXX";
            try
            {
                bill = BCPay.BCInternationalPay(bill);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "成功" + "</span><br/>");
            }
            catch (Exception excption)
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
            }
        }
    }
}