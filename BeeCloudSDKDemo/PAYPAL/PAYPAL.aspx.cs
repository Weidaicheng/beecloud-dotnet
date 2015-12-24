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
            BCInternationlBill bill = new BCInternationlBill(BCPay.InternationalPay.PAYPAL_PAYPAL.ToString(), 1, BCUtil.GetUUID(), "dotnet paypal", "USD");
            bill.returnUrl = "http://localhost:50003/paypal/return_paypal_url.aspx";
            try
            {
                bill = BCPay.BCInternationalPay(bill);
                Response.Write("<a href=" + bill.url + ">付款地址</a><br/>");
            }
            catch (Exception excption)
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
            }
        }
    }
}