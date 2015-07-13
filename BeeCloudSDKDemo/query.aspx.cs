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
        private List<BCBill> bills;

        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["querytype"];
            if (type == "aliquery")
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "Ali" + "</span><br/>");
                BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCUtil.GetTimeStamp(DateTime.Now.ToUniversalTime()), "ALI", null, null, null, null, 50);
                bills = result.bills;
            }
            if (type == "wxquery")
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "WX" + "</span><br/>");
                BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCUtil.GetTimeStamp(DateTime.Now.ToUniversalTime()), "WX", null, null, null, null, 50);
                bills = result.bills;   
            }
            if (type == "unionquery")
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "UN" + "</span><br/>");
                BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCUtil.GetTimeStamp(DateTime.Now.ToUniversalTime()), "UN", null, null, null, null, 50);
                bills = result.bills;   
            }
            this.bind();
        }

        protected void Refund_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string billNo = btn.CommandArgument;
        }

        protected void bind()
        {
            QueryGridView.DataSource = bills;
            QueryGridView.DataBind();
        }

        protected void QueryGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells[3].Text == "False")
            {
                e.Row.Cells[5].Visible = false;
            }
        }

        protected void QueryGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "refund")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument); 
                Response.Write(rowIndex + "当前" + e.CommandArgument.ToString());
            }
        }

    }
}