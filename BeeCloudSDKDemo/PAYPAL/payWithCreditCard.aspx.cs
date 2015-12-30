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
            BCInternationlBill bill = new BCInternationlBill(BCPay.InternationalPay.PAYPAL_CREDITCARD.ToString(), 1, BCUtil.GetUUID(), "dotnet paypal", "USD");
            bill.info = info;
            try
            {
                bill = BCPay.BCInternationalPay(bill);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + bill.creditCardId + "</span><br/>");
            }
            catch (Exception excption)
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
            }
        }
    }
}