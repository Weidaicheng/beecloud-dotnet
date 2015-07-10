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
    public partial class pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["paytype"];
            if (type == "alipay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.GetTimeStamp(DateTime.Now), BCPay.PayChannel.ALI_WEB.ToString(), 1, "20150708abcdef001", "自来水", null, null, null, null, null);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCAliWebPayResult payResult = result as BCAliWebPayResult;
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.html + "</span><br/>");
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.url + "</span><br/>");
                }
            }
            else if (type == "wechatQr")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.GetTimeStamp(DateTime.Now), BCPay.PayChannel.WX_NATIVE.ToString(), 1, "20150520abcdef001", "自制自来水", null, null, null, null, "2");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCWxNativePayResult payResult = result as BCWxNativePayResult;
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.codeURL + "</span><br/>");
                }
            }
            else if (type == "unionpay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.GetTimeStamp(DateTime.Now), BCPay.PayChannel.UN_WEB.ToString(), 1, "20150520abcdef001", "自制自来水", null, "http://localhost:8088", null, null, "2");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCUnWebPayResult payResult = result as BCUnWebPayResult;
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.html + "</span><br/>");
                }
            }
            else if (type == "qralipay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.GetTimeStamp(DateTime.Now), BCPay.PayChannel.ALI_QRCODE.ToString(), 1, "20150520abcdef001", "自制自来水", null, null, null, null, "2");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCAliQrcodePayResult payResult = result as BCAliQrcodePayResult;
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.url + "</span><br/>");
                }
            }
            else
            {
                BCWxJSAPIPayResult result = BCPay.BCPayByChannel(BCPay.GetTimeStamp(DateTime.Now), BCPay.PayChannel.WX_JSAPI.ToString(), 1, "20150520abcdef004", "自制自来水", null, null, "o3kKrjlUsMnv__cK5DYZMl0JoAkY", null, null) as BCWxJSAPIPayResult;
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                if (result.resultCode == 0)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.appId + "</span><br/>");
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.noncestr + "</span><br/>");
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.package + "</span><br/>");
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.paySign + "</span><br/>");
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.signType + "</span><br/>");
                }
            }
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + type + "</span>");
        }

        
    }
}