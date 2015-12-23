﻿using System;
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
                BCBill bill = new BCBill(BCPay.PayChannel.ALI_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.returnUrl = "http://localhost:50003/return_ali_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.html + "</span><br/>");
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.url + "</span><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "wechatQr")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.WX_NATIVE.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    string str = resultBill.codeURL;

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
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "unionpay")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.UN_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.returnUrl = "http://localhost:50003/return_un_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.html + "</span><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "qralipay")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.ALI_QRCODE.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.qrPayMode = "0";
                bill.returnUrl = "http://localhost:50003/return_ali_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    //Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.html + "</span><br/>");
                    Response.Redirect(resultBill.url);
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "aliwappay")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.ALI_WAP.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.returnUrl = "http://localhost:50003/return_ali_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.html + "</span><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "jdpay")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.JD_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.returnUrl = "http://localhost:50003/return_jd_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.html + "</span><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "jdwappay")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.JD_WAP.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.returnUrl = "http://localhost:50003/return_jd_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.html + "</span><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "ybpay")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.YEE_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.returnUrl = "http://localhost:50003/return_yee_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    Response.Write("<a href=" + resultBill.url + ">付款地址</a><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "ybwappay")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.YEE_WAP.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.returnUrl = "http://localhost:50003/return_yee_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    Response.Write("<a href=" + resultBill.url + ">付款地址</a><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "kqpay")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.KUAIQIAN_WEB.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.returnUrl = "http://localhost:50003/return_kq_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.html + "</span><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "kqwappay")
            {
                BCBill bill = new BCBill(BCPay.PayChannel.KUAIQIAN_WAP.ToString(), 1, BCUtil.GetUUID(), "dotNet自来水");
                bill.returnUrl = "http://localhost:50003/return_kq_url.aspx";
                try
                {
                    BCBill resultBill = BCPay.BCPayByChannel(bill);
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + resultBill.html + "</span><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "alitransfers")
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

                try
                {
                    BCTransfersParameter para = new BCTransfersParameter();
                    para.channel = BCPay.TransferChannel.ALI.ToString();
                    para.batchNo = BCUtil.GetUUID();
                    para.accountName = "毛毛";
                    para.transfersData = list;
                    string transfersURL = BCPay.BCTransfers(para);
                    Response.Write("<a href=" + transfersURL + ">付款地址</a><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "alitransfer")
            {
                try
                {
                    BCTransferParameter para = new BCTransferParameter();
                    para.channel = BCPay.TransferChannel.ALI_TRANSFER.ToString();
                    para.transferNo = BCUtil.GetUUID();
                    para.totalFee = 100;
                    para.desc = "C# 单笔打款";
                    para.channelUserId = "XXX@163.com";
                    para.channelUserName = "毛毛";
                    para.accountName = "XXX有限公司";
                    string aliURL = BCPay.BCTransfer(para);
                    Response.Write("<a href=" + aliURL + ">付款地址</a><br/>");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "wxtransfer")
            {
                try
                {
                    BCTransferParameter para = new BCTransferParameter();
                    para.channel = BCPay.TransferChannel.WX_TRANSFER.ToString();
                    para.transferNo = "1000000000";
                    para.totalFee = 100;
                    para.desc = "C# 单笔打款";
                    para.channelUserId = "XXXXXXXXXXXXXXXXXX";
                    BCPay.BCTransfer(para);
                    Response.Write("完成");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            else if (type == "wxredpack")
            {
                BCRedPackInfo info = new BCRedPackInfo();
                info.actName = "C# 红包";
                info.sendName = "BeeCloud";
                info.wishing = "啦啦啦";

                try
                {
                    BCTransferParameter para = new BCTransferParameter();
                    para.channel = BCPay.TransferChannel.WX_REDPACK.ToString();
                    para.transferNo = "1000000001";
                    para.totalFee = 100;
                    para.desc = "C# 红包";
                    para.channelUserId = "XXXXXXXXXXXXXXXX";
                    para.info = info;
                    BCPay.BCTransfer(para);
                    Response.Write("完成");
                }
                catch (Exception excption)
                {
                    Response.Write("<span style='color:#00CD00;font-size:20px'>" + excption.Message + "</span><br/>");
                }
            }
            Response.Write("<span style='color:#00CD00;font-size:20px'>" + type + "</span>");
        }

        
    }
}