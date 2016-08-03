using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BeeCloud;
using BeeCloud.Model.BCSubscription;

namespace BeeCloudSDKDemo
{
    public partial class subscription : System.Web.UI.Page
    {
        List<BCPlan> plans = new List<BCPlan>();
        List<BCSubscription> subs = new List<BCSubscription>();

        protected void Page_Load(object sender, EventArgs e)
        {
            BeeCloud.BeeCloud.registerApp("95d87fff-989c-4426-812c-21408644cf88", "8aaad136-b899-4793-9564-0ebc72ae86f2", "688dbe68-a7e9-4f16-850a-21270949afe8", null);
            plans = BCPay.queryPlansByCondition(null, null, null, null, null, null, 0, 5, false);
            subs = BCPay.querySubscriptionsByCondition(null, null, null, null, null, null, 5, false);
            this.bind();
        }

        protected void bind()
        {
            PlansGridView.DataSource = plans;
            PlansGridView.DataBind();

            SubscriptionGridView.DataSource = subs;
            SubscriptionGridView.DataBind();
        }

        protected void PlansGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //
        }

        protected void PlansGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "sub")
            {
                try
                {
                    string smsid = BCPay.sendSMS("订阅人手机号");
                    BCSubscription sub = BCPay.createSubscription(smsid, 
                        "手机收到的验证码，用户输入，获取后传入", 
                        new BCSubscription("用户ID", 
                            plans[Convert.ToInt32(e.CommandArgument)].ID, 
                            "订阅用户银行名称（支持列表可参考getBanks()获取支持银行列表)", 
                            "卡号", 
                            "姓名", 
                            "身份证号", 
                            "银行预留手机号，要与发验证码的手机号一致"));
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('订阅成功')", true);
                }
                catch (Exception ex)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + ex.Message + "</span><br/>");
                }
                
            }
        }

        protected void SubscriptionGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //
        }
    }
}