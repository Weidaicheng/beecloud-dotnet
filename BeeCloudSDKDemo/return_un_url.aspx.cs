using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeCloudSDKDemo
{
    public partial class return_un_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            String respCode = Request.Form["respCode"];
            String respMsg = Request.Form["respMsg"];

            if (respCode == "00" && respMsg == "success")
            {
                //支付成功
                Response.Write("支付成功");
            }

        }
    }
}