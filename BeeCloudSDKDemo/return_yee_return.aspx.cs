using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeCloudSDKDemo
{
    public partial class return_yee_return : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String respCode = Request.Form["respCode"];
            String respMsg = Request.Form["respMsg"];

            //商户订单号

            string out_trade_no = Request.QueryString["r6_Order"];

            //交易状态
            string trade_status = Request.QueryString["r1_Code"];


            if (trade_status == "1")
            {
                Response.Write("支付成功");
            }
            else
            {
                Response.Write("trade_status=" + trade_status);
            }

        }
    }
}