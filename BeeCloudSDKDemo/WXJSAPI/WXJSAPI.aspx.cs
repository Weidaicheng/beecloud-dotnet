using BeeCloud;
using BeeCloud.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WxPayAPI;

namespace BeeCloudSDKDemo.WXJSAPI
{
    public partial class WXJSAPI : System.Web.UI.Page
    {
        protected string appid = "wx119a2bda81854ae0";
        protected string timeStamp;
        protected string noncestr;
        protected string package;
        protected string signType;
        protected string paySign;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JsApiPay jsApiPay = new JsApiPay(this);
                try
                {
                    //调用【网页授权获取用户信息】接口获取用户的openid和access_token
                    jsApiPay.appid = appid;
                    jsApiPay.appsecret = "53e3943476118a3dff21fb95848de6d7";
                    jsApiPay.GetOpenidAndAccessToken();

                    //ViewState["openid"] = jsApiPay.openid;
                    Response.Write(jsApiPay.openid);

                    BCBill bill = new BCBill(BCPay.PayChannel.WX_JSAPI.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                    bill.openId = jsApiPay.openid;
                    try
                    {
                        BCBill resultBill = BCPay.BCPayByChannel(bill);
                        timeStamp = resultBill.timestamp;
                        noncestr = resultBill.noncestr;
                        package = resultBill.package;
                        paySign = resultBill.paySign;
                        signType = resultBill.signType;
                    }
                    catch (Exception excption)
                    {
                        Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<span style='color:#FF0000;font-size:20px'>" + "页面加载出错，请重试" + ex.Message + "</span>");
                }
            }
        }
    }
}