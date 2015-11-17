using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BeeCloud;
using BeeCloud.Model;
using ThoughtWorks.QRCode.Codec;
using System.IO;
using System.Drawing.Imaging;
using System.Text;
using System.Drawing;

namespace BeeCloudSDKDemo
{
    public partial class pay : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Form["paytype"];
            if (type == "alipay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.ALI_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水", null, "http://localhost:50003/return_ali_url.aspx", 300, null, null, null);
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
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.WX_NATIVE.ToString(), 1, BCUtil.GetUUID(), "dotNet自制自来水", null, null, 300, null, null, "2");
                //Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                //Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                //Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCWxNativePayResult payResult = result as BCWxNativePayResult;
                    //Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.codeURL + "</span><br/>");
                    string str = payResult.codeURL;

                    //初始化二维码生成工具
                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    qrCodeEncoder.QRCodeVersion = 0;
                    qrCodeEncoder.QRCodeScale = 4;

                    //将字符串生成二维码图片
                    Bitmap image = qrCodeEncoder.Encode(str, Encoding.Default);
                    //保存为PNG到内存流  
                    MemoryStream ms = new MemoryStream();
                    image.Save(ms, ImageFormat.Png);

                    //输出二维码图片
                    Response.BinaryWrite(ms.GetBuffer());
                    Response.ContentType = "image/Png";
                }
            }
            else if (type == "unionpay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.UN_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自制自来水", null, "http://localhost:50003/return_un_url.aspx", 300, null, null, "2");
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
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.ALI_QRCODE.ToString(), 1, BCUtil.GetUUID(), "dotNet自制自来水", null, "http://localhost:50003/return_ali_url.aspx", 300, null, null, "0");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCAliQrcodePayResult payResult = result as BCAliQrcodePayResult;
                    Response.Write("<iframe src=" + payResult.url + " name=\"testIframe\" allowtransparency=\"true\" background-color=\"transparent\" title=\"test\" frameborder=\"0\" width=\"300\" height=\"300\" scrolling=\"no\"></iframe>");
                }
            }
            else if (type == "aliwappay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.ALI_WAP.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水", null, "http://localhost:50003/return_ali_url.aspx", 300, null, null, null);
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
            else if (type == "jdpay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.JD_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水", null, "http://localhost:50003/return_jd_url.aspx", null, null, null, null);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCJDPayResult payResult = result as BCJDPayResult;
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.html + "</span><br/>");
                }
            }
            else if (type == "jdwappay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.JD_WAP.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水", null, "http://localhost:50003/return_jd_url.aspx", null, null, null, null);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCJDPayResult payResult = result as BCJDPayResult;
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.html + "</span><br/>");
                }
            }
            else if (type == "ybpay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.YEE_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水", null, "http://localhost:50003/return_yee_url.aspx", 300, null, null, null);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCYEEPayResult payResult = result as BCYEEPayResult;
                    Response.Write("<a href=" + payResult.url + ">付款地址</a><br/>");
                }
            }
            else if (type == "ybwappay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.YEE_WAP.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水", null, null, 300, null, null, null);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCYEEPayResult payResult = result as BCYEEPayResult;
                    Response.Write("<a href=" + payResult.url + ">付款地址</a><br/>");
                }
            }
            else if (type == "kqpay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.KUAIQIAN_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水", null, "http://localhost:50003/return_kq_url.aspx", 300, null, null, null);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCKuaiQianPayResult payResult = result as BCKuaiQianPayResult;
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.html + "</span><br/>");
                }
            }
            else if (type == "kqwappay")
            {
                BCPayResult result = BCPay.BCPayByChannel(BCPay.PayChannel.KUAIQIAN_WAP.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水", null, "http://localhost:50003/return_kq_url.aspx", 300, null, null, null);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.errDetail + "</span><br/>");
                if (result.resultCode == 0)
                {
                    BCKuaiQianPayResult payResult = result as BCKuaiQianPayResult;
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + payResult.html + "</span><br/>");
                }
            }
            else if (type == "alitransfer")
            {
                BCTransferData data = new BCTransferData();
                data.transferId = BCUtil.GetUUID();
                data.receiverAccount = "xx@xx.com";
                data.receiverName = "某某某";
                data.transferFee = 100;
                data.transferNote = "note";
                BCTransferData data2 = new BCTransferData();
                data2.transferId = BCUtil.GetUUID();
                data2.receiverAccount = "xx@xx.com";
                data2.receiverName = "某某";
                data2.transferFee = 100;
                data2.transferNote = "note";
                List<BCTransferData> list = new List<BCTransferData>();
                list.Add(data);
                list.Add(data2);
                BCTransferResult result = BCPay.BCTransfer(BCPay.TransferChannel.ALI.ToString(), BCUtil.GetUUID(), "毛毛", list);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultCode + "</span><br/>");
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result.resultMsg + "</span><br/>");
                if (result.resultCode == 0)
                {
                    Response.Write("<a href=" + result.url + ">付款地址</a><br/>");
                }
            }
            else if (type == "wxtransfer")
            {
                Response.Write("<span style='color:#00CD00;font-size:20px'>即将支持</span><br/>");
            }
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + type + "</span>");
        }

        
    }
}