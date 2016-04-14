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

                try
                {
                    BCQueryBillParameter para = new BCQueryBillParameter();
                    para.channel = "ALI";
                    para.limit = 50;
                    bills = BCPay.BCPayQueryByCondition(para);
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            if (type == "wxquery") 
            {
                typeChannel = "WX";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "WX" + "</span><br/>");
                try
                {
                    BCQueryBillParameter para = new BCQueryBillParameter();
                    para.channel = "WX";
                    para.limit = 50;
                    bills = BCPay.BCPayQueryByCondition(para);
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            if (type == "unionquery")
            {
                typeChannel = "UN";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "UN" + "</span><br/>");
                try
                {
                    BCQueryBillParameter para = new BCQueryBillParameter();
                    para.channel = "UN";
                    para.limit = 50;
                    bills = BCPay.BCPayQueryByCondition(para);
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            if (type == "jdquery")
            {
                typeChannel = "JD";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "JD" + "</span><br/>");
                try
                {
                    BCQueryBillParameter para = new BCQueryBillParameter();
                    para.channel = "JD";
                    para.limit = 50;
                    bills = BCPay.BCPayQueryByCondition(para);
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            if (type == "ybquery")
            {
                typeChannel = "YEE";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "YEE" + "</span><br/>");
                try
                {
                    BCQueryBillParameter para = new BCQueryBillParameter();
                    para.channel = "YEE";
                    para.limit = 50;
                    bills = BCPay.BCPayQueryByCondition(para);
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            if (type == "kqquery")
            {
                typeChannel = "KUAIQIAN";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "KUAIQIAN" + "</span><br/>");
                try
                {
                    BCQueryBillParameter para = new BCQueryBillParameter();
                    para.channel = "KUAIQIAN";
                    para.limit = 50;
                    bills = BCPay.BCPayQueryByCondition(para);
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            if (type == "beepay")
            {
                typeChannel = "BC_GATEWAY";
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "BC_GATEWAY" + "</span><br/>");
                try
                {
                    BCQueryBillParameter para = new BCQueryBillParameter();
                    para.channel = "BC_GATEWAY";
                    para.limit = 50;
                    bills = BCPay.BCPayQueryByCondition(para);
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
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
                //e.Row.Cells[6].Visible = false;
                e.Row.Cells[6].Enabled = false;
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
                    BCRefund refund = new BCRefund(billNo, DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), totalFee);
                    refund.channel = BCPay.RefundChannel.ALI.ToString();
                    try
                    {
                        refund = BCPay.BCRefundByChannel(refund);
                        Response.Redirect(refund.url);
                    }
                    catch (Exception excption)
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                    }
                }
                if (typeChannel == "WX")
                {
                    BCRefund refund = new BCRefund(billNo, DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), totalFee);
                    refund.channel = BCPay.RefundChannel.WX.ToString();
                    try
                    {
                        refund = BCPay.BCRefundByChannel(refund);
                        Response.Write("<script>alert('退款成功！')</script>");
                    }
                    catch (Exception excption)
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                    }
                }
                if (typeChannel == "UN")
                {
                    BCRefund refund = new BCRefund(billNo, DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), totalFee);
                    refund.channel = BCPay.RefundChannel.UN.ToString();
                    try
                    {
                        refund = BCPay.BCRefundByChannel(refund);
                        Response.Write("<script>alert('退款成功！')</script>");
                    }
                    catch (Exception excption)
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                    }
                }
                if (typeChannel == "JD")
                {
                    BCRefund refund = new BCRefund(billNo, DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), totalFee);
                    refund.channel = BCPay.RefundChannel.JD.ToString();
                    try
                    {
                        refund = BCPay.BCRefundByChannel(refund);
                        Response.Write("<script>alert('退款成功！')</script>");
                    }
                    catch (Exception excption)
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                    }
                }
                if (typeChannel == "YEE")
                {
                    BCRefund refund = new BCRefund(billNo, DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), totalFee);
                    refund.channel = BCPay.RefundChannel.YEE.ToString();
                    try
                    {
                        refund = BCPay.BCRefundByChannel(refund);
                        Response.Write("<script>alert('退款成功！')</script>");
                    }
                    catch (Exception excption)
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                    }
                }
                if (typeChannel == "KUAIQIAN")
                {
                    BCRefund refund = new BCRefund(billNo, DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), totalFee);
                    refund.channel = BCPay.RefundChannel.KUAIQIAN.ToString();
                    try
                    {
                        refund = BCPay.BCRefundByChannel(refund);
                        Response.Write("<script>alert('退款成功！')</script>");
                    }
                    catch (Exception excption)
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                    }
                }
                if (typeChannel == "BC_GATEWAY")
                {
                    BCRefund refund = new BCRefund(billNo, DateTime.Today.ToString("yyyyMMdd") + BCUtil.GetUUID().Substring(0, 8), totalFee);
                    refund.channel = BCPay.RefundChannel.BC.ToString();
                    try
                    {
                        refund = BCPay.BCRefundByChannel(refund);
                        Response.Write("<script>alert('退款成功！')</script>");
                    }
                    catch (Exception excption)
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                    }
                }
            }

        }

    }
}