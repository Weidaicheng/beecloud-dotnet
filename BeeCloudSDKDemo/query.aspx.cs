using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BeeCloud;
using BeeCloud.Model;

namespace BeeCloudSDKDemo
{
    public partial class query : System.Web.UI.Page
    {
        private static List<BCBill> bills = new List<BCBill>();
        private static string typeChannel;

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["querytype"];
            if (type == "aliquery")
            {
                typeChannel = "Ali";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "Ali" + "</span><br/>");
                BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCUtil.GetTimeStamp(DateTime.Now.ToUniversalTime()), "ALI", null, null, null, null, 50);
                bills = result.bills;
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
            }
            if (type == "wxquery")
            {
                typeChannel = "WX";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "WX" + "</span><br/>");
                BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCUtil.GetTimeStamp(DateTime.Now.ToUniversalTime()), "WX", null, null, null, null, 50);
                bills = result.bills;
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
            }
            if (type == "unionquery")
            {
                typeChannel = "UN";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "UN" + "</span><br/>");
                BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCUtil.GetTimeStamp(DateTime.Now.ToUniversalTime()), "UN", null, null, null, null, 50);
                bills = result.bills;
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
            }
            this.bind();
        }

        protected void bind()
        {
            QueryGridView.DataSource = bills;
            QueryGridView.DataBind();
        }

        protected void QueryGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells[4].Text == "False")
            {
                e.Row.Cells[6].Visible = false;
            }
        }

        protected void QueryGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "refund")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string billNo = bills[rowIndex].billNo;
                int totalFee = bills[rowIndex].totalFee;
                if (typeChannel == "Ali")
                {
                    BCRefundResult refundResult = BCPay.BCRefundByChannel(BCUtil.GetTimeStamp(DateTime.Now), BCPay.RefundChannel.ALI.ToString(), DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), billNo, totalFee, null);
                    if (refundResult.resultCode == 0)
                    {
                        Response.Redirect(refundResult.url);
                    }
                    else
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + refundResult.resultCode + "</span><br/>");
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + refundResult.resultMsg + "</span><br/>");
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + refundResult.errDetail + "</span><br/>");
                    }
                }
                if (typeChannel == "WX")
                {
                    BCRefundResult refundResult = BCPay.BCRefundByChannel(BCUtil.GetTimeStamp(DateTime.Now), BCPay.RefundChannel.WX.ToString(), DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), billNo, totalFee, null);
                    if (refundResult.resultCode == 0)
                    {
                        Response.Write("<script>alert('退款成功！')</script>");
                    }
                    else
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + refundResult.resultCode + "</span><br/>");
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + refundResult.resultMsg + "</span><br/>");
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + refundResult.errDetail + "</span><br/>");
                    }
                }
                if (typeChannel == "UN")
                {
                    BCRefundResult refundResult = BCPay.BCRefundByChannel(BCUtil.GetTimeStamp(DateTime.Now), BCPay.RefundChannel.UN.ToString(), DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), billNo, totalFee, null);
                    if (refundResult.resultCode == 0)
                    {
                        Response.Write("<script>alert('退款成功！')</script>");
                    }
                    else
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + refundResult.resultCode + "</span><br/>");
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + refundResult.resultMsg + "</span><br/>");
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + refundResult.errDetail + "</span><br/>");
                    }
                }
            }

        }

    }
}