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
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["querytype"];
            if (type == "aliquery")
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "Ali" + "</span><br/>");
                BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCPay.GetTimeStamp(DateTime.Now.ToUniversalTime()), "ALI", null, null, null, null, 50);
                for (int i = 0; i < result.count; i++)
                {
                    Response.Write(result.bills[i].billNo.ToString() + "--");
                    Response.Write(result.bills[i].createdTime.ToString() + "--");
                    Response.Write(result.bills[i].result.ToString() + "--");
                    Response.Write(result.bills[i].title.ToString() + "--");
                    Response.Write(result.bills[i].totalFee.ToString() + "<br/>");
                }
            }
            if (type == "wxquery")
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "WX" + "</span><br/>");
                BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCPay.GetTimeStamp(DateTime.Now.ToUniversalTime()), "WX", null, null, null, null, 50);
                for (int i = 0; i < result.count; i++)
                {
                    Response.Write(result.bills[i].billNo.ToString() + "--");
                    Response.Write(result.bills[i].createdTime.ToString() + "--");
                    Response.Write(result.bills[i].result.ToString() + "--");
                    Response.Write(result.bills[i].title.ToString() + "--");
                    Response.Write(result.bills[i].totalFee.ToString() + "<br/>");
                }
            }
            if (type == "unionquery")
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + "UN" + "</span><br/>");
                BCPayQueryResult result = BCPay.BCPayQueryByCondition(BCPay.GetTimeStamp(DateTime.Now.ToUniversalTime()), "UN", null, null, null, null, 50);
                for (int i = 0; i < result.count; i++)
                {
                    Response.Write(result.bills[i].billNo.ToString() + "--");
                    Response.Write(result.bills[i].createdTime.ToString() + "--");
                    Response.Write(result.bills[i].result.ToString() + "--");
                    Response.Write(result.bills[i].title.ToString() + "--");
                    Response.Write(result.bills[i].totalFee.ToString() + "<br/>");
                }
            }
        }
    }
}