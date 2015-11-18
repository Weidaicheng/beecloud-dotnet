using System.Net;
using System.Text;
using System.Web;
using System.Collections.Generic;
using LitJson;
using BeeCloud.Model;
using System;

namespace BeeCloud
{
    public class BCPay
    {   
        public enum PayChannel
        {
            WX_NATIVE,
            WX_JSAPI,
            ALI_WEB,
            ALI_QRCODE,
            ALI_WAP,
            UN_WEB,
            JD_WAP,
            JD_WEB,
            YEE_WAP,
            YEE_WEB,
            KUAIQIAN_WAP,
            KUAIQIAN_WEB,
            BD_WEB,
            BD_WAP
        };

        public enum InternationalPay
        {
            PAYPAL_PAYPAL,
            PAYPAL_CREDITCARD,
            PAYPAL_SAVED_CREDITCARD
        };

        public enum QueryChannel
        {
            WX,
            ALI,
            UN,
            WX_APP,
            WX_NATIVE,
            WX_JSAPI,
            ALI_APP,
            ALI_WEB,
            ALI_QRCODE,
            ALI_WAP,
            UN_APP,
            UN_WEB,
            JD_WAP,
            JD_WEB,
            YEE_WAP,
            YEE_WEB,
            KUAIQIAN_WAP,
            KUAIQIAN_WEB,
            BD_WEB,
            BD_WAP,
            PAYPAL
        };

        public enum RefundChannel
        {
            WX,
            ALI,
            UN,
            JD,
            YEE,
            KUAIQIAN,
            BD
        };

        public enum RefundStatusChannel
        {
            WX,
            YEE,
            KUAIQIAN,
            BD
        };

        public enum TransferChannel
        {
            ALI,
            WX_REDPACK, 
            WX_TRANSFER, 
            ALI_TRANSFER
        };

        #region 支付
        //准备支付数据
        public static string preparePayParameters(string channel, int totalFee, string billNo, string title, Dictionary<string, string> optional, string returnUrl, int? billTimeout, string openId, string showURL, string qrPayMode)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["total_fee"] = totalFee;
            data["bill_no"] = billNo;
            data["title"] = title;
            data["return_url"] = returnUrl;

            data["bill_timeout"] = billTimeout;

            data["openid"] = openId;
            data["show_url"] = showURL;
            data["qr_pay_mode"] = qrPayMode;


            if (optional != null && optional.Count > 0)
            {
                data["optional"] = new JsonData();
                foreach (string key in optional.Keys)
                {
                    data["optional"][key] = optional[key];
                }
            }

            string paraString = data.ToJson();
            return paraString;
        }

        //处理支付回调
        public static BCPayResult handlePayResult(string respString, string channel)
        {
            JsonData responseData = JsonMapper.ToObject(respString);

            if (channel == "WX_NATIVE")
            {
                BCWxNativePayResult result = new BCWxNativePayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.codeURL = responseData["code_url"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            if (channel == "WX_JSAPI")
            {
                BCWxJSAPIPayResult result = new BCWxJSAPIPayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.appId = responseData["app_id"].ToString();
                    result.package = responseData["package"].ToString();
                    result.noncestr = responseData["nonce_str"].ToString();
                    result.timestamp = responseData["timestamp"].ToString();
                    result.paySign = responseData["pay_sign"].ToString();
                    result.signType = responseData["sign_type"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            if (channel == "ALI_WEB" || channel == "ALI_WAP")
            {
                BCAliWebPayResult result = new BCAliWebPayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.html = responseData["html"].ToString();
                    result.url = responseData["url"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            if (channel == "ALI_QRCODE")
            {
                BCAliQrcodePayResult result = new BCAliQrcodePayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.url = responseData["url"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            if (channel == "UN_WEB")
            {
                BCUnWebPayResult result = new BCUnWebPayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.html = responseData["html"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            if (channel == "JD_WAP" || channel == "JD_WEB")
            {
                BCJDPayResult result = new BCJDPayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.html = responseData["html"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            if (channel == "KUAIQIAN_WAP" || channel == "KUAIQIAN_WEB")
            {
                BCKuaiQianPayResult result = new BCKuaiQianPayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.html = responseData["html"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            if (channel == "YEE_WAP")
            {
                BCYEEPayResult result = new BCYEEPayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.url = responseData["url"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            if (channel == "YEE_WEB")
            {
                BCYEEPayResult result = new BCYEEPayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.url = responseData["url"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            if (channel == "BD_WEB" || channel == "BD_WAP")
            {
                BCBDPayResult result = new BCBDPayResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    result.id = responseData["id"].ToString();
                    result.url = responseData["url"].ToString();
                }
                else
                {
                    result.errDetail = responseData["err_detail"].ToString();
                }
                return result;
            }
            return new BCPayResult();
        }

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="channel">渠道类型
        ///     根据不同场景选择不同的支付方式
        ///     必填
        ///     可以通过enum BCPay.PayChannel获取
        ///     channel的参数值含义：
        ///     WX_APP:       微信手机APP支付
        ///     WX_NATIVE:    微信公众号二维码支付
        ///     WX_JSAPI:     微信公众号支付
        ///     ALI_APP:      支付宝APP支付
        ///     ALI_WEB:      支付宝网页支付 
        ///     ALI_QRCODE:   支付宝内嵌二维码支付
        ///     UN_APP:       银联APP支付
        ///     UN_WEB:       银联网页支付
        ///     JD_WAP:       京东wap支付
        ///     JD_WEB:       京东web支付
        ///     YEE_WAP:      易宝wap支付 
        ///     YEE_WEB:      易宝web支付
        ///     KUAIQIAN_WAP: 快钱wap支付
        ///     KUAIQIAN_WEB: 快钱web支付
        ///     BD_WEB:       百度web支付
        ///     BD_WAP:       百度wap支付
        /// </param>
        /// <param name="totalFee">订单总金额
        ///     只能为整数，单位为分
        ///     必填
        /// </param>
        /// <param name="billNo">商户订单号
        ///     32个字符内，数字和/或字母组合，确保在商户系统中唯一（即所有渠道所有订单号不同）
        ///     必填
        /// </param>
        /// <param name="title">订单标题
        ///     32个字节内，最长支持16个汉字
        ///     必填
        /// </param>
        /// <param name="optional">附加数据
        ///     用户自定义的参数，将会在webhook通知中原样返回，该字段主要用于商户携带订单的自定义数据
        ///     {"key1":"value1","key2":"value2",...}
        ///     可空
        /// </param>
        /// <param name="returnUrl">同步返回页面
        ///     支付渠道处理完请求后,当前页面自动跳转到商户网站里指定页面的http路径。
        ///     当channel 参数为 ALI_WEB 或 ALI_QRCODE 或 UN_WEB时为必填
        /// </param>
        /// <param name="billTimeout">订单失效时间
        ///     必须为非零正整数，单位为秒，建议最短失效时间间隔必须大于300秒
        ///     可空
        ///     京东系列支付不支持该参数，填空
        /// </param>
        /// <param name="openId">用户相对于微信公众号的唯一id
        ///     例如'0950c062-5e41-44e3-8f52-f89d8cf2b6eb'
        ///     微信公众号支付(WX_JSAPI)的必填参数
        /// </param>
        /// <param name="showURL">商品展示地址
        ///     以http://开头,例如'http://beecloud.cn'
        ///     支付宝网页支付(ALI_WEB)的选填参数
        /// </param>
        /// <param name="qrPayMode">二维码类型
        ///     支付宝内嵌二维码支付(ALI_QRCODE)的选填参数
        ///     二维码类型含义
        ///     0： 订单码-简约前置模式,对应 iframe 宽度不能小于 600px, 高度不能小于 300px
        ///     1： 订单码-前置模式,对应 iframe 宽度不能小于 300px, 高度不能小于 600px
        ///     3： 订单码-迷你前置模式,对应 iframe 宽度不能小于 75px, 高度不能小于 75px
        /// </param>
        /// <returns>
        ///     BCPayResult， 根据不同的支付渠道有各自对应的result类型
        /// </returns>
        public static BCPayResult BCPayByChannel(string channel, int totalFee, string billNo, string title, Dictionary<string, string> optional, string returnUrl, int? billTimeout, string openId, string showURL, string qrPayMode)
        {
            Random random = new Random();
            string payUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billURL;

            string paraString = preparePayParameters(channel, totalFee, billNo, title, optional, returnUrl, billTimeout, openId, showURL, qrPayMode);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(payUrl, paraString, BCCache.Instance.networkTimeout);

                string respString = BCPrivateUtil.GetResponseString(response);

                return handlePayResult(respString, channel);
            }
            catch (Exception e)
            {
                BCPayResult result = new BCPayResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }
        }
        #endregion

        #region 退款
        //准备退款参数
        public static string prepareRefundParameters(string channel, string refundNo, string billNo, int refundFee, Dictionary<string, string> optional, bool needApproval)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignatureByMasterSecret(BCCache.Instance.appId, BCCache.Instance.masterSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["refund_no"] = refundNo;
            data["bill_no"] = billNo;
            data["refund_fee"] = refundFee;
            if (optional != null && optional.Count > 0)
            {
                data["optional"] = new JsonData();
                foreach (string key in optional.Keys)
                {
                    data["optional"][key] = optional[key];
                }
            }
            data["need_approval"] = needApproval;
            string paraString = data.ToJson();
            return paraString;
        }

        //处理退款回调
        public static BCRefundResult handleRefundResult(string respString, string channel)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            BCRefundResult result = new BCRefundResult();
            result.resultCode = int.Parse(responseData["result_code"].ToString());
            result.resultMsg = responseData["result_msg"].ToString();
            if (responseData["result_code"].ToString() == "0")
            {
                result.id = responseData["id"].ToString();
                if (channel.Contains("ALI"))
                {
                    result.url = responseData["url"].ToString();
                }
            }
            else
            {
                result.errDetail = responseData["err_detail"].ToString();
            }
            return result;
        }

        /// <summary>
        /// (预)退款
        /// </summary>
        /// <param name="channel">渠道类型   
        ///     选填
        ///     可以通过enum BCPay.RefundChannel获取
        ///     ALI:      支付宝
        ///     WX:       微信
        ///     UN:       银联
        ///     JD:       京东
        ///     YEE:      易宝
        ///     KUAIQIAN: 快钱
        ///     BD:       百度
        ///     注意：不传channel也能退款的前提是保证所有渠道所有订单号不同，如果出现两个订单号重复，会报错提示传入channel进行区分
        /// </param>
        /// <param name="refundNo">商户退款单号
        ///     格式为:退款日期(8位) + 流水号(3~24 位)。不可重复，且退款日期必须是当天日期。流水号可以接受数字或英文字符，建议使用数字，但不可接受“000”。
        ///     必填
        ///     例如：201506101035040000001
        /// </param>
        /// <param name="billNo">商户订单号
        ///     32个字符内，数字和/或字母组合，确保在商户系统中唯一
        ///     DIRECT_REFUND和PRE_REFUND时必填
        /// </param>
        /// <param name="refundFee">退款金额
        ///     只能为整数，单位为分
        ///     DIRECT_REFUND和PRE_REFUND时必填
        /// </param>
        /// <param name="optional">附加数据
        ///     用户自定义的参数，将会在webhook通知中原样返回，该字段主要用于商户携带订单的自定义数据
        ///     选填
        ///     {"key1":"value1","key2":"value2",...}
        /// </param>
        /// <param name="needApproval">是否为预退款
        ///     预退款needApproval值传true,直接退款传false
        ///     如果needApproval值传true，开发者需要调用审核退款接口或者直接去BeeCloud控制台的预退款界面审核退款方能最终退款
        /// </param>
        /// <returns>
        ///     BCRefundResult
        /// </returns>
        public static BCRefundResult BCRefundByChannel(string channel, string refundNo, string billNo, int refundFee, Dictionary<string, string> optional, bool needApproval)
        {
            Random random = new Random();
            string refundUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundURL;
            string paraString = prepareRefundParameters(channel, refundNo, billNo, refundFee, optional, needApproval);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(refundUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleRefundResult(respString, channel);
                
            }
            catch(Exception e)
            {
                BCRefundResult result = new BCRefundResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }            
        }
        #endregion

        #region 退款审核
        //准备退款审核参数
        public static string prepareApproveRefundParameters(string channel, List<string> ids, bool agree, string denyReason)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignatureByMasterSecret(BCCache.Instance.appId, BCCache.Instance.masterSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["ids"] = JsonMapper.ToObject(JsonMapper.ToJson(ids));
            data["agree"] = agree;
            data["denyReason"] = denyReason;

            string paraString = data.ToJson();
            return paraString;
        }

        //处理退款审核回调
        public static BCApproveRefundResult handleApproveRefundResult(string respString, string channel)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            BCApproveRefundResult result = new BCApproveRefundResult();
            result.resultCode = int.Parse(responseData["result_code"].ToString());
            result.resultMsg = responseData["result_msg"].ToString();
            if (responseData["result_code"].ToString() == "0")
            {
                if (channel.Contains("ALI"))
                {
                    result.url = responseData["url"].ToString();
                }
                result.status = JsonMapper.ToObject<Dictionary<string, string>>(responseData["result_map"].ToString());
            }
            else
            {
                result.errDetail = responseData["err_detail"].ToString();
            }
            return result;
        }

        /// <summary>
        ///  预退款(批量)审核
        /// </summary>
        /// <param name="channel">渠道类型
        ///     根据不同渠道选不同的值
        ///     必填
        ///     可以通过enum BCPay.RefundChannel获取
        ///     ALI:      支付宝
        ///     WX:       微信
        ///     UN:       银联
        ///     JD:       京东
        ///     YEE:      易宝
        ///     KUAIQIAN: 快钱
        ///     BD:       百度
        /// </param>
        /// <param name="ids">退款记录id列表
        ///     批量审核的退款记录的唯一标识符集合
        ///     必填
        /// </param>
        /// <param name="agree">同意或者驳回
        ///     批量驳回传false，批量同意传true
        ///     必填
        /// </param>
        /// <param name="denyReason">驳回理由
        ///     可空
        /// </param>
        /// <returns>
        ///     参考BCApproveRefundResult
        /// </returns>
        public static BCApproveRefundResult BCApproveRefund(string channel, List<string> ids, bool agree, string denyReason)
        {
            Random random = new Random();
            string approveRefundUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundURL;

            string paraString = prepareApproveRefundParameters(channel, ids, agree, denyReason);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePutHttpResponse(approveRefundUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleApproveRefundResult(respString, channel);
            }
            catch (Exception e)
            {
                BCApproveRefundResult result = new BCApproveRefundResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            } 
        }
        #endregion

        #region 查询
        ///准备订单查询参数
        public static string preparePayQueryByConditionParameters(string channel, string billNo, long? startTime, long? endTime, bool? spayResult, bool? needDetail, int? skip, int? limit)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["bill_no"] = billNo;
            data["start_time"] = startTime;
            data["end_time"] = endTime;
            data["skip"] = skip;
            data["spay_result"] = spayResult;
            data["need_detail"] = needDetail;
            data["limit"] = limit;

            string paraString = data.ToJson();
            return paraString;
        }

        //处理订单条件查询回调
        public static BCPayQueryByConditionResult handlePayQueryByConditionResult(string respString, bool? needDetail)
        {
            JsonData responseData = JsonMapper.ToObject(respString);

            BCPayQueryByConditionResult result = new BCPayQueryByConditionResult();

            result.resultCode = int.Parse(responseData["result_code"].ToString());
            result.resultMsg = responseData["result_msg"].ToString();
            if (result.resultCode == 0)
            {
                result.count = int.Parse(responseData["count"].ToString());
                if (responseData["bills"].IsArray)
                {
                    List<BCBill> bills = new List<BCBill>();
                    foreach (JsonData billData in responseData["bills"])
                    {
                        BCBill bill = new BCBill();
                        bill.id = billData["id"].ToString();
                        bill.title = billData["title"].ToString();
                        bill.totalFee = int.Parse(billData["total_fee"].ToString());
                        bill.createdTime = BCUtil.GetDateTime((long)billData["create_time"]);
                        bill.billNo = billData["bill_no"].ToString();
                        bill.result = (bool)billData["spay_result"];
                        bill.channel = billData["channel"].ToString();
                        bill.tradeNo = billData["trade_no"].ToString();
                        bill.subChannel = billData["sub_channel"].ToString();
                        bill.optional = billData["optional"].ToString();
                        if (needDetail == true)
                        {
                            bill.messageDetail = billData["message_detail"].ToString();
                        }
                        bill.revertResult = (bool)billData["revert_result"];
                        bill.refundResult = (bool)billData["refund_result"];
                        bills.Add(bill);
                    }
                    result.bills = bills;
                }
            }
            else
            {
                result.errDetail = responseData["err_detail"].ToString();
            }

            return result;
        }

        //处理订单Id查询回调
        public static BCPayQueryByIdResult handlePayQueryByIdResult(string respString)
        {
            JsonData responseData = JsonMapper.ToObject(respString);

            BCPayQueryByIdResult result = new BCPayQueryByIdResult();

            result.resultCode = int.Parse(responseData["result_code"].ToString());
            result.resultMsg = responseData["result_msg"].ToString();
            if (result.resultCode == 0)
            {
                JsonData billData = responseData["pay"];
                BCBill bill = new BCBill();
                bill.id = billData["id"].ToString();
                bill.title = billData["title"].ToString();
                bill.totalFee = int.Parse(billData["total_fee"].ToString());
                bill.createdTime = BCUtil.GetDateTime((long)billData["create_time"]);
                bill.billNo = billData["bill_no"].ToString();
                bill.result = (bool)billData["spay_result"];
                bill.channel = billData["channel"].ToString();
                bill.tradeNo = billData["trade_no"].ToString();
                bill.subChannel = billData["sub_channel"].ToString();
                bill.optional = billData["optional"].ToString();
                bill.messageDetail = billData["message_detail"].ToString();
                bill.revertResult = (bool)billData["revert_result"];
                bill.refundResult = (bool)billData["refund_result"];

                result.bill = bill;
            }
            else
            {
                result.errDetail = responseData["err_detail"].ToString();
            }

            return result;
        }

        //准备订单/退款id查询参数
        public static string prepareQueryByIdParameters(string id)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;

            string paraString = data.ToJson();
            return paraString;
        }

        //准备退款查询参数
        public static string prepareRefundQueryByConditionParameters(string channel, string billNo, string refundNo, long? startTime, long? endTime, bool? needApproval, bool? needDetail, int? skip, int? limit)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["bill_no"] = billNo;
            data["refund_no"] = refundNo;
            data["start_time"] = startTime;
            data["end_time"] = endTime;
            data["need_approval"] = needApproval;
            data["need_detail"] = needDetail;
            data["skip"] = skip;
            data["limit"] = limit;

            string paraString = data.ToJson();
            return paraString;
        }

        //处理退款条件查询回调
        public static BCRefundQueryByConditionResult handleRefundQueryByConditionResult(string respString, bool? needDetail)
        {
            JsonData responseData = JsonMapper.ToObject(respString);

            BCRefundQueryByConditionResult result = new BCRefundQueryByConditionResult();

            result.resultCode = int.Parse(responseData["result_code"].ToString());
            result.resultMsg = responseData["result_msg"].ToString();
            if (result.resultCode == 0)
            {
                result.count = int.Parse(responseData["count"].ToString());
                if (responseData["refunds"].IsArray)
                {
                    List<BCRefund> refunds = new List<BCRefund>();
                    foreach (JsonData refundData in responseData["refunds"])
                    {
                        BCRefund refund = new BCRefund();
                        refund.id = refundData["id"].ToString();
                        refund.title = refundData["title"].ToString();
                        refund.billNo = refundData["bill_no"].ToString();
                        refund.refundNo = refundData["refund_no"].ToString();
                        refund.totalFee = int.Parse(refundData["total_fee"].ToString());
                        refund.refundFee = int.Parse(refundData["refund_fee"].ToString());
                        refund.channel = refundData["channel"].ToString();
                        refund.subChannel = refundData["sub_channel"].ToString();
                        refund.finish = (bool)refundData["finish"];
                        refund.result = (bool)refundData["result"];
                        refund.optional = refundData["optional"].ToString();
                        if (needDetail == true)
                        {
                            refund.messageDetail = refundData["message_detail"].ToString();
                        }
                        refund.createdTime = BCUtil.GetDateTime((long)refundData["create_time"]);
                        refunds.Add(refund);
                    }
                    result.refunds = refunds;
                }
            }
            else
            {
                result.errDetail = responseData["err_detail"].ToString();
            }

            return result;
        }

        //处理退款Id查询回调
        public static BCRefundQueryByIdResult handleRefundQueryByIdResult(string respString)
        {
            JsonData responseData = JsonMapper.ToObject(respString);

            BCRefundQueryByIdResult result = new BCRefundQueryByIdResult();

            result.resultCode = int.Parse(responseData["result_code"].ToString());
            result.resultMsg = responseData["result_msg"].ToString();
            if (result.resultCode == 0)
            {
                BCRefund refund = new BCRefund();
                JsonData refundData = responseData["refund"];
                refund.id = refundData["id"].ToString();
                refund.title = refundData["title"].ToString();
                refund.billNo = refundData["bill_no"].ToString();
                refund.refundNo = refundData["refund_no"].ToString();
                refund.totalFee = int.Parse(refundData["total_fee"].ToString());
                refund.refundFee = int.Parse(refundData["refund_fee"].ToString());
                refund.channel = refundData["channel"].ToString();
                refund.subChannel = refundData["sub_channel"].ToString();
                refund.finish = (bool)refundData["finish"];
                refund.result = (bool)refundData["result"];
                refund.optional = refundData["optional"].ToString();
                refund.messageDetail = refundData["message_detail"].ToString();
                refund.createdTime = BCUtil.GetDateTime((long)refundData["create_time"]);
                result.refund = refund;
            }
            else
            {
                result.errDetail = responseData["err_detail"].ToString();
            }

            return result;
        }

        //准备退款状态查询参数
        public static string prepareRefundStatusQueryParameters(string channel, string refundNo)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["refund_no"] = refundNo;

            string paraString = data.ToJson();
            return paraString;
        }

        //处理退狂状态查询回调
        public static BCRefundStatusQueryResult handleRefundStatusQueryResult(string respString)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            BCRefundStatusQueryResult result = new BCRefundStatusQueryResult();
            result.resultCode = int.Parse(responseData["result_code"].ToString());
            result.resultMsg = responseData["result_msg"].ToString();
            if (responseData["result_code"].ToString() == "0")
            {
                result.refundStatus = responseData["refund_status"].ToString();
            }
            else
            {
                result.errDetail = responseData["err_detail"].ToString();
            }
            return result;
        }

        /// <summary>
        /// 支付订单查询
        /// </summary>
        /// <param name="channel">渠道类型
        ///     选填
        ///     可以通过enum BCPay.QueryChannel获取
        ///     channel的参数值含义：
        ///     WX_APP:       微信手机APP支付
        ///     WX_NATIVE:    微信公众号二维码支付
        ///     WX_JSAPI:     微信公众号支付
        ///     ALI_APP:      支付宝APP支付
        ///     ALI_WEB:      支付宝网页支付 
        ///     ALI_QRCODE:   支付宝内嵌二维码支付
        ///     UN_APP:       银联APP支付
        ///     UN_WEB:       银联网页支付
        ///     JD_WAP:       京东wap支付
        ///     JD_WEB:       京东web支付
        ///     YEE_WAP:      易宝wap支付 
        ///     YEE_WEB:      易宝web支付
        ///     KUAIQIAN_WAP: 快钱wap支付
        ///     KUAIQIAN_WEB: 快钱web支付
        ///     注意：不传channel也能查询的前提是保证所有渠道所有订单号不同，如果出现两个订单号重复，会报错提示传入channel进行区分
        /// </param>
        /// <param name="billNo">商户订单号
        /// </param>
        /// <param name="startTime">起始时间
        ///     毫秒时间戳, 13位, 可以使用BCUtil.GetTimeStamp()方法获取
        ///     选填
        /// </param>
        /// <param name="endTime">结束时间
        ///     毫秒时间戳, 13位, 可以使用BCUtil.GetTimeStamp()方法获取
        ///     选填
        /// </param>
        /// <param name="spayResult">订单状态
        ///     订单是否成功，null为全部返回，true只返回成功订单，false只返回失败订单
        ///     选填
        /// </param>
        /// <param name="needDetail">是否需要返回渠道详细信息
        ///     决定是否需要返回渠道的回调信息，true为需要
        ///     选填
        /// </param>
        /// <param name="skip">查询起始位置
        ///     默认为0。设置为10表示忽略满足条件的前10条数据
        ///     选填
        /// </param>
        /// <param name="limit">查询的条数
        ///     默认为10，最大为50。设置为10表示只返回满足条件的10条数据
        ///     选填
        /// </param>
        /// <returns></returns>
        public static BCPayQueryByConditionResult BCPayQueryByCondition(string channel, string billNo, long? startTime, long? endTime, bool? spayResult, bool? needDetail, int? skip, int? limit)
        {
            Random random = new Random();
            string payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billsURL;

            string paraString = preparePayQueryByConditionParameters(channel, billNo, startTime, endTime, spayResult, needDetail, skip, limit);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handlePayQueryByConditionResult(respString, needDetail);
            }
            catch(Exception e)
            {
                BCPayQueryByConditionResult result = new BCPayQueryByConditionResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 支付订单查询(指定ID)
        /// </summary>
        /// <param name="id">订单id</param>
        /// <returns></returns>
        public static BCPayQueryByIdResult BCPayQueryById(string id)
        {
            Random random = new Random();
            string payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.billURL + "/" + id;

            string paraString = prepareQueryByIdParameters(id);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handlePayQueryByIdResult(respString);
            }
            catch (Exception e)
            {
                BCPayQueryByIdResult result = new BCPayQueryByIdResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 退款订单查询
        /// </summary>
        /// <param name="channel">渠道类型
        ///     根据不同场景选择不同的支付方式
        ///     必填
        ///     可以通过enum BCPay.QueryChannel获取
        ///     channel的参数值含义：
        ///     WX_APP:       微信手机APP支付
        ///     WX_NATIVE:    微信公众号二维码支付
        ///     WX_JSAPI:     微信公众号支付
        ///     ALI_APP:      支付宝APP支付
        ///     ALI_WEB:      支付宝网页支付 
        ///     ALI_QRCODE:   支付宝内嵌二维码支付
        ///     UN_APP:       银联APP支付
        ///     UN_WEB:       银联网页支付
        ///     JD_WAP:       京东wap支付
        ///     JD_WEB:       京东web支付
        ///     YEE_WAP:      易宝wap支付 
        ///     YEE_WEB:      易宝web支付
        ///     KUAIQIAN_WAP: 快钱wap支付
        ///     KUAIQIAN_WEB: 快钱web支付
        ///     注意：不传channel也能查询的前提是保证所有渠道所有订单号不同，如果出现两个订单号重复，会报错提示传入channel进行区分
        /// </param>
        /// <param name="billNo">商户订单号
        /// </param>
        /// <param name="refundNo">商户退款单号
        /// </param>
        /// <param name="startTime">起始时间
        ///     毫秒时间戳, 13位, 可以使用BCUtil.GetTimeStamp()方法获取
        ///     选填</param>
        /// <param name="endTime">结束时间
        ///     毫秒时间戳, 13位, 可以使用BCUtil.GetTimeStamp()方法获取
        ///     选填
        /// </param>
        /// <param name="needApproval">需要审核     
        ///     标识退款记录是否为预退款
        ///     选填
        /// </param>
        /// <param name="needDetail">是否需要返回渠道详细信息
        ///     决定是否需要返回渠道的回调信息，true为需要
        ///     选填
        /// </param>
        /// <param name="skip">查询起始位置
        ///     默认为0。设置为10表示忽略满足条件的前10条数据
        ///     选填
        /// </param>
        /// <param name="limit">查询的条数
        ///     默认为10，最大为50。设置为10表示只返回满足条件的10条数据
        ///     选填
        /// </param>
        /// <returns>
        ///     BCRefundQuerytResult
        /// </returns>
        public static BCRefundQueryByConditionResult BCRefundQueryByCondition(string channel, string billNo, string refundNo, long? startTime, long? endTime, bool? needApproval, bool? needDetail, int? skip, int? limit)
        {
            Random random = new Random();
            string payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundsURL;

            string paraString = prepareRefundQueryByConditionParameters(channel, billNo, refundNo, startTime, endTime, needApproval, needDetail, skip, limit);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleRefundQueryByConditionResult(respString, needDetail);
            }
            catch (Exception e)
            {
                BCRefundQueryByConditionResult result = new BCRefundQueryByConditionResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 退款订单查询(指定ID)
        /// </summary>
        /// <param name="id">退款记录的唯一标识，可用于查询单笔记录</param>
        /// <returns></returns>
        public static BCRefundQueryByIdResult BCRefundQueryById(string id)
        {
            Random random = new Random();
            string payQueryUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundURL + "/" + id;

            string paraString = prepareQueryByIdParameters(id);

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleRefundQueryByIdResult(respString);
            }
            catch (Exception e)
            {
                BCRefundQueryByIdResult result = new BCRefundQueryByIdResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        ///退款状态查询"
        /// </summary>
        /// <param name="channel">渠道类型
        ///     只有WX、YEE、KUAIQIAN、BD需要
        /// </param>
        /// <param name="refundNo">商户退款单号
        /// </param>
        /// <returns>
        ///     BCRefundStatusQueryResult
        /// </returns>
        public static BCRefundStatusQueryResult BCRefundStatusQuery(string channel, string refundNo)
        {
            Random random = new Random();
            string refundStatusUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.refundStatusURL;

            string paraString = prepareRefundStatusQueryParameters(channel, refundNo);
            
            string url = refundStatusUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
            try
            {
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleRefundStatusQueryResult(respString);
            }
            catch (Exception e)
            {
                BCRefundStatusQueryResult result = new BCRefundStatusQueryResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }
        }
        #endregion

        #region 打款
        //处理单笔单款参数
        public static string prepareTransferParameters(string channel, string transferNo, int totalFee, string desc, string channelUserId, string channelUserName, BCRedPackInfo info, string account_name)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignatureByMasterSecret(BCCache.Instance.appId, BCCache.Instance.masterSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["transfer_no"] = transferNo;
            data["total_fee"] = totalFee;
            data["desc"] = desc;
            data["channel_user_id"] = channelUserId;
            data["channel_user_name"] = channelUserName;
            data["account_name"] = account_name;
            if (info != null)
            {
                data["redpack_info"] = new JsonData();
                data["redpack_info"]["send_name"] = info.sendName;
                data["redpack_info"]["wishing"] = info.wishing;
                data["redpack_info"]["act_name"] = info.actName;
            }

            string paraString = data.ToJson();
            return paraString;
        }

        //准备批量打款参数
        public static string prepareTransfersParameters(string channel, string batchNo, string accountName, List<BCTransferData> transferData)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignatureByMasterSecret(BCCache.Instance.appId, BCCache.Instance.masterSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["batch_no"] = batchNo;
            data["account_name"] = accountName;
            JsonData list = new JsonData();
            foreach (var transfer in transferData)
            {
                JsonData d = new JsonData();
                d["transfer_id"] = transfer.transferId;
                d["receiver_account"] = transfer.receiverAccount;
                d["receiver_name"] = transfer.receiverName;
                d["transfer_fee"] = transfer.transferFee;
                d["transfer_note"] = transfer.transferNote;
                list.Add(d);
            }
            data["transfer_data"] = list;
            string paraString = data.ToJson();
            return paraString;
        }

        //处理(批量)打款回调
        public static BCTransferResult handleTransfersResult(string respString, string channel)
        {
            JsonData responseData = JsonMapper.ToObject(respString);
            BCTransferResult result = new BCTransferResult();
            result.resultCode = int.Parse(responseData["result_code"].ToString());
            result.resultMsg = responseData["result_msg"].ToString();
            if (responseData["result_code"].ToString() == "0")
            {
                if (channel.Contains("ALI"))
                {
                    result.url = responseData["url"].ToString();
                }
            }
            return result;
        }

        /// <summary>
        /// 批量打款
        /// </summary>
        /// <param name="channel">渠道
        ///     必填
        ///     现在只支持支付宝（TransferChannel.ALI_TRANSFER）</param>
        /// <param name="batchNo">批量付款批号
        ///     必填
        ///     此次批量付款的唯一标示，11-32位数字字母组合
        /// </param>
        /// <param name="accountName">付款方的支付宝账户名
        ///     必填
        /// </param>
        /// <param name="transferData">付款的详细数据
        ///     必填
        ///     每一个Map对应一笔付款的详细数据, list size 小于等于 1000。
        ///     具体参BCTransferData类
        /// </param>
        /// <returns></returns>
        public static BCTransferResult BCTransfers(string channel, string batchNo, string accountName, List<BCTransferData> transferData)
        {
            Random random = new Random();
            string transferUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.transfersURL;

            string paraString = prepareTransfersParameters(channel, batchNo, accountName, transferData);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(transferUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleTransfersResult(respString, channel);
            }
            catch (Exception e)
            {
                BCTransferResult result = new BCTransferResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 打款
        /// </summary>
        /// <param name="channel">渠道类型
        ///     WX_REDPACK 微信红包, 
        ///     WX_TRANSFER 微信企业打款, 
        ///     ALI_TRANSFER 支付宝企业打款
        /// </param>
        /// <param name="transferNo">打款单号
        ///     支付宝为11-32位数字字母组合， 微信为10位数字
        /// </param>
        /// <param name="totalFee">打款金额
        ///     此次打款的金额,单位分,正整数(微信红包1.00-200元，微信打款>=1元)
        /// </param>
        /// <param name="desc">打款说明
        ///     此次打款的说明
        /// </param>
        /// <param name="channelUserId">用户id
        ///     支付渠道方内收款人的标示, 微信为openid, 支付宝为支付宝账户
        /// </param>
        /// <param name="channelUserName">用户名
        ///     支付渠道内收款人账户名， 支付宝必填
        /// </param>
        /// <param name="info">红包信息
        ///     查看BCRedPackInfo
        /// </param>
        /// <param name="account_name">打款方账号名称
        ///     打款方账号名全称，支付宝必填
        /// </param>
        /// <returns></returns>
        public static BCTransferResult BCTransfer(string channel, string transferNo, int totalFee, string desc, string channelUserId, string channelUserName, BCRedPackInfo info, string account_name)
        {
            Random random = new Random();
            string transferUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.transferURL;

            string paraString = prepareTransferParameters(channel, transferNo, totalFee, desc, channelUserId, channelUserName, info, account_name);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(transferUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleTransfersResult(respString, channel);
            }
            catch (Exception e)
            {
                BCTransferResult result = new BCTransferResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }
        }
        #endregion

        #region 境外支付
        //准备境外支付参数
        public static string prepareInternationalPayParameters(string channel, int totalFee, string billNo, string title, string currency, BCCreditCardInfo info, string creditCardId, string returnUrl)
        {
            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["total_fee"] = totalFee;
            data["bill_no"] = billNo;
            data["title"] = title;
            data["currency"] = currency;
            if (info != null)
            {
                data["credit_card_info"] = JsonMapper.ToObject(JsonMapper.ToJson(info));
            }
            if (creditCardId != null)
            {
                data["credit_card_id"] = creditCardId;
            }
            if (returnUrl != null)
            {
                data["return_url"] = returnUrl;
            }

            string paraString = data.ToJson();
            return paraString;
        }

        //处理境外支付回调
        public static BCPayResult handleInternationalPayResult(string respString, string channel)
        {
            JsonData responseData = JsonMapper.ToObject(respString);

            BCPayPalResult result = new BCPayPalResult();
            result.resultCode = int.Parse(responseData["result_code"].ToString());
            result.resultMsg = responseData["result_msg"].ToString();
            if (responseData["result_code"].ToString() == "0")
            {
                if (channel == "PAYPAL_PAYPAL")
                {
                    result.url = responseData["url"].ToString();
                }
                if (channel == "PAYPAL_CREDITCARD")
                {
                    result.creditCardId = responseData["credit_card_id"].ToString();
                }
            }
            else
            {
                result.errDetail = responseData["err_detail"].ToString();
            }
            return result;
        }

        /// <summary>
        /// 境外支付
        /// </summary>
        /// <param name="channel">渠道类型
        ///     enum InternationalPay提供了三个境外支付渠道类型，分别是：
        ///     PAYPAL_PAYPAL ： 跳转到paypal使用paypal内支付
        ///     PAYPAL_CREDITCARD ： 直接使用信用卡支付（paypal渠道）
        ///     PAYPAL_SAVED_CREDITCARD ： 使用存储的行用卡id支付（信用卡信息存储在PAYPAL)
        /// </param>
        /// <param name="totalFee">订单总金额
        ///     只能为整数，单位为分
        ///     必填
        /// </param>
        /// <param name="billNo">商户订单号
        ///     32个字符内，数字和/或字母组合，确保在商户系统中唯一（即所有渠道所有订单号不同）
        ///     必填
        /// </param>
        /// <param name="title">订单标题
        ///     32个字节内，最长支持16个汉字
        ///     必填
        /// </param>
        /// <param name="currency">三位货币种类代码
        ///     必填
        ///     类型如下：
        ///         Australian dollar	AUD
        ///         Brazilian real**	BRL
        ///         Canadian dollar	    CAD
        ///         Czech koruna	    CZK
        ///         Danish krone	    DKK
        ///         Euro	            EUR
        ///         Hong Kong dollar	HKD
        ///         Hungarian forint	HUF
        ///         Israeli new shekel	ILS
        ///         Japanese yen	    JPY
        ///         Malaysian ringgit	MYR
        ///         Mexican peso	    MXN
        ///         New Taiwan dollar	TWD
        ///         New Zealand dollar	NZD
        ///         Norwegian krone	    NOK
        ///         Philippine peso	    PHP
        ///         Polish złoty	    PLN
        ///         Pound sterling	    GBP
        ///         Singapore dollar	SGD
        ///         Swedish krona	    SEK
        ///         Swiss franc	        CHF
        ///         Thai baht	        THB
        ///         Turkish lira	    TRY
        ///         United States dollar	USD
        /// </param>
        /// <param name="info">信用卡信息
        ///     具体查看BCCreditCardInfo类
        ///     当channel 为PAYPAL_CREDITCARD必填
        /// </param>
        /// <param name="creditCardId">
        ///     当使用PAYPAL_CREDITCARD支付完成后会返回一个credit_card_id，商家可以存储这个id方便下次通过这个id发起支付无需再输入卡面信息
        /// </param>
        /// <param name="returnUrl">同步返回页面
        ///     支付渠道处理完请求后,当前页面自动跳转到商户网站里指定页面的http路径。
        ///     当channel参数为PAYPAL_PAYPAL时为必填
        /// </param>
        /// <returns></returns>
        public static BCPayResult BCInternationalPay(string channel, int totalFee, string billNo, string title, string currency, BCCreditCardInfo info,  string creditCardId, string returnUrl)
        {
            Random random = new Random();
            string payUrl = BCPrivateUtil.getHost() + BCConstants.version + BCConstants.internationalURL;

            string paraString = prepareInternationalPayParameters(channel, totalFee, billNo, title, currency, info, creditCardId, returnUrl);

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(payUrl, paraString, BCCache.Instance.networkTimeout);
                string respString = BCPrivateUtil.GetResponseString(response);
                return handleInternationalPayResult(respString, channel);                
            }
            catch (Exception e)
            {
                BCPayResult result = new BCPayResult();
                result.resultCode = -1;
                result.resultMsg = e.Message;
                return result;
            }
        }
        #endregion
    }
}
