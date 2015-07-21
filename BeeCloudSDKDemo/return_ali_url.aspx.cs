using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeCloudSDKDemo
{
    public partial class _return : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                //商户订单号

                string out_trade_no = Request.QueryString["out_trade_no"];

                //支付宝交易号

                string trade_no = Request.QueryString["trade_no"];

                //交易状态
                string trade_status = Request.QueryString["trade_status"];


                if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                {
                    //判断该笔订单是否在商户网站中已经做过处理
                    Response.Write("支付成功");
                }
                else
                {
                    Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                }

                //打印页面
                Response.Write("验证成功<br />");
            }
            else
            {
                Response.Write("无返回参数");
            }
        }

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }
    }
}