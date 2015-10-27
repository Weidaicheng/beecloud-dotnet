using System.Net;
using System.Text;
using System.Web;
using System.Collections.Generic;
using LitJson;
using BeeCloud.Model;
using System;

namespace BeeCloud
{
    public static class BCPay
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
            KUAIQIAN_WEB
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
            PAYPAL
        };

        public enum RefundChannel
        {
            WX,
            ALI,
            UN,
            JD,
            YEE,
            KUAIQIAN
        };

        public enum RefundStatusChannel
        {
            WX,
            YEE,
            KUAIQIAN
        };

        public enum TransferChannel
        {
            ALI,
            WX
        };

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
        public static BCPayResult BCPayByChannel(string channel, int totalFee, string billNo, string title, Dictionary<string,string> optional, string returnUrl,string openId, string showURL, string qrPayMode)
        {
            Random random = new Random();
            string payUrl = BCPrivateUtil.mLocalDefaultHosts[random.Next(0, 4)] + BCConstants.version + BCConstants.billURL;

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

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(payUrl, paraString);

                string respString = BCPrivateUtil.GetResponseString(response);

                JsonData responseData = JsonMapper.ToObject(respString);

                if (channel == "WX_NATIVE")
                {
                    BCWxNativePayResult result = new BCWxNativePayResult();
                    result.resultCode = int.Parse(responseData["result_code"].ToString());
                    result.resultMsg = responseData["result_msg"].ToString();
                    if (responseData["result_code"].ToString() == "0")
                    {
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
            catch (Exception e)
            {
                BCPayResult result = new BCPayResult();
                result.resultCode = 99;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 退款
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
        /// <returns>
        ///     BCRefundResult
        /// </returns>
        public static BCRefundResult BCRefundByChannel(string channel, string refundNo, string billNo, int refundFee, Dictionary<string, string> optional)
        {
            Random random = new Random();
            string refundUrl = BCPrivateUtil.mLocalDefaultHosts[random.Next(0, 4)] + BCConstants.version + BCConstants.refundURL;

            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
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
            string paraString = data.ToJson();

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(refundUrl, paraString);
                string respString = BCPrivateUtil.GetResponseString(response);
                JsonData responseData = JsonMapper.ToObject(respString);
                BCRefundResult result = new BCRefundResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
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
            catch(Exception e)
            {
                BCRefundResult result = new BCRefundResult();
                result.resultCode = 99;
                result.resultMsg = e.Message;
                return result;
            }            
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
        /// <param name="skip">查询起始位置
        ///     默认为0。设置为10表示忽略满足条件的前10条数据
        ///     选填
        /// </param>
        /// <param name="limit">查询的条数
        ///     默认为10，最大为50。设置为10表示只返回满足条件的10条数据
        ///     选填
        /// </param>
        /// <returns></returns>
        public static BCPayQueryResult BCPayQueryByCondition(string channel, string billNo, long? startTime, long? endTime, int? skip, int? limit)
        {
            Random random = new Random();
            string payQueryUrl = BCPrivateUtil.mLocalDefaultHosts[random.Next(0, 4)] + BCConstants.version + BCConstants.billsURL;

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
            data["limit"] = limit;

            string paraString = data.ToJson();

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout, null, null);
                string respString = BCPrivateUtil.GetResponseString(response);
                JsonData responseData = JsonMapper.ToObject(respString);

                BCPayQueryResult result = new BCPayQueryResult();

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
                            bill.title = billData["title"].ToString();
                            bill.totalFee = int.Parse(billData["total_fee"].ToString());
                            bill.createdTime = BCUtil.GetDateTime((long)billData["created_time"]);
                            bill.billNo = billData["bill_no"].ToString();
                            bill.result = (bool)billData["spay_result"];
                            bill.channel = billData["channel"].ToString();
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
            catch(Exception e)
            {
                BCPayQueryResult result = new BCPayQueryResult();
                result.resultCode = 99;
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
        ///     选填</param>
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
        public static BCRefundQuerytResult BCRefundQueryByCondition(string channel, string billNo, string refundNo, long? startTime, long? endTime, int? skip, int? limit)
        {
            Random random = new Random();
            string payQueryUrl = BCPrivateUtil.mLocalDefaultHosts[random.Next(0, 4)] + BCConstants.version + BCConstants.refundsURL;

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
            data["skip"] = skip;
            data["limit"] = limit;

            string paraString = data.ToJson();

            try
            {
                string url = payQueryUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout, null, null);
                string respString = BCPrivateUtil.GetResponseString(response);
                JsonData responseData = JsonMapper.ToObject(respString);

                BCRefundQuerytResult result = new BCRefundQuerytResult();

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
                            refund.title = refundData["title"].ToString();
                            refund.billNo = refundData["bill_no"].ToString();
                            refund.refundNo = refundData["refund_no"].ToString();
                            refund.totalFee = int.Parse(refundData["total_fee"].ToString());
                            refund.refundFee = int.Parse(refundData["refund_fee"].ToString());
                            refund.channel = refundData["channel"].ToString();
                            refund.finish = (bool)refundData["finish"];
                            refund.result = (bool)refundData["result"];
                            refund.createdTime = BCUtil.GetDateTime((long)refundData["created_time"]);
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
            catch (Exception e)
            {
                BCRefundQuerytResult result = new BCRefundQuerytResult();
                result.resultCode = 99;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        ///退款状态查询"(只支持微信)
        /// </summary>
        /// <param name="channel">渠道类型
        ///     暂时只能填WX
        /// </param>
        /// <param name="refundNo">商户退款单号
        /// </param>
        /// <returns>
        ///     BCRefundStatusQueryResult
        /// </returns>
        public static BCRefundStatusQueryResult BCRefundStatusQuery(string channel, string refundNo)
        {
            Random random = new Random();
            string refundStatusUrl = BCPrivateUtil.mLocalDefaultHosts[random.Next(0, 4)] + BCConstants.version + BCConstants.refundStatusURL;

            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["refund_no"] = refundNo;

            string paraString = data.ToJson();
            string url = refundStatusUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
            try
            {
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout, null, null);
                string respString = BCPrivateUtil.GetResponseString(response);
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
            catch (Exception e)
            {
                BCRefundStatusQueryResult result = new BCRefundStatusQueryResult();
                result.resultCode = 99;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 批量打款
        /// </summary>
        /// <param name="channel">渠道
        ///     必填
        ///     现在只支持支付宝（TransferChannel.ALI）</param>
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
        public static BCTransferResult BCTransfer(string channel, string batchNo, string accountName, List<BCTransferData> transferData)
        {
            Random random = new Random();
            string transferUrl = BCPrivateUtil.mLocalDefaultHosts[random.Next(0, 4)] + BCConstants.version + BCConstants.transfersURL;

            long timestamp = BCUtil.GetTimeStamp(DateTime.Now);

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
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

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(transferUrl, paraString);
                string respString = BCPrivateUtil.GetResponseString(response);
                JsonData responseData = JsonMapper.ToObject(respString);
                BCTransferResult result = new BCTransferResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    if (channel == "ALI")
                    {
                        result.url = responseData["url"].ToString();
                    }
                }
                return result;
            }
            catch (Exception e)
            {
                BCTransferResult result = new BCTransferResult();
                result.resultCode = 99;
                result.resultMsg = e.Message;
                return result;
            }
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
            string payUrl = BCPrivateUtil.mLocalDefaultHosts[random.Next(0, 4)] + BCConstants.version + BCConstants.internationalURL;

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

            try
            {
                HttpWebResponse response = BCPrivateUtil.CreatePostHttpResponse(payUrl, paraString);

                string respString = BCPrivateUtil.GetResponseString(response);

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
            catch (Exception e)
            {
                BCPayResult result = new BCPayResult();
                result.resultCode = 99;
                result.resultMsg = e.Message;
                return result;
            }
        }
    }
}
