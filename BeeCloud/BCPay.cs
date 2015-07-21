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
            UN_WEB
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
            UN_APP,
            UN_WEB
        }

        public enum RefundChannel
        {
            WX,
            ALI,
            UN
        };

        /// <summary>
        /// 支付
        /// </summary>
        /// <param name="timestamp">签名生成时间
        ///     时间戳，毫秒数，13位， 可以使用BCUtil.GetTimeStamp()方法获取
        ///     必填
        /// </param>
        /// <param name="channel">渠道类型
        ///     根据不同场景选择不同的支付方式
        ///     必填
        ///     可以通过enum BCPay.PayChannel获取
        ///     channel的参数值含义：
        ///     WX_APP: 微信手机APP支付
        ///     WX_NATIVE: 微信公众号二维码支付
        ///     WX_JSAPI: 微信公众号支付
        ///     ALI_APP: 支付宝APP支付
        ///     ALI_WEB: 支付宝网页支付 ALI_QRCODE: 支付宝内嵌二维码支付
        ///     UN_APP: 银联APP支付
        ///     UN_WEB: 银联网页支付
        /// </param>
        /// <param name="totalFee">订单总金额
        ///     只能为整数，单位为分
        ///     必填
        /// </param>
        /// <param name="billNo">商户订单号
        ///     32个字符内，数字和/或字母组合，确保在商户系统中唯一
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
            string payUrl = "http://58.211.191.123:8080/1/rest/bill";//BCCache.Instance.bestHost + BCConstants.version + BCConstants.billURL;

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
                        result.noncestr = responseData["noncestr"].ToString();
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
                if (channel == "ALI_WEB")
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
                return new BCPayResult();
            }
            catch (Exception e)
            {
                BCWxNativePayResult result = new BCWxNativePayResult();
                result.resultCode = 99;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="timestamp">签名生成时间
        ///     时间戳，毫秒数，13位， 可以使用BCUtil.GetTimeStamp()方法获取
        ///     必填
        /// </param>
        /// <param name="channel">渠道类型   
        ///     根据不同场景选择不同的支付方式
        ///     必填
        ///     可以通过enum BCPay.RefundChannel获取
        ///     ALI:支付宝
        ///     WX:微信
        ///     UN:银联
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
            string refundUrl = "http://58.211.191.123:8080/1/rest/refund";//BCCache.Instance.bestHost + BCConstants.version + BCConstants.refundURL;

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
        /// <param name="timestamp">签名生成时间
        ///     时间戳，毫秒数，13位， 可以使用BCUtil.GetTimeStamp()方法获取
        ///     必填
        /// </param>
        /// <param name="channel">渠道类型
        ///     根据不同场景选择不同的支付方式
        ///     必填
        ///     可以通过enum BCPay.QueryChannel获取
        ///     channel的参数值含义：
        ///     WX: 微信所有类型支付
        ///     WX_APP: 微信手机APP支付
        ///     WX_NATIVE: 微信公众号二维码支付
        ///     WX_JSAPI: 微信公众号支付
        ///     ALI: 支付宝所有类型支付
        ///     ALI_APP: 支付宝APP支付
        ///     ALI_WEB: 支付宝网页支付 
        ///     ALI_QRCODE: 支付宝内嵌二维码支付
        ///     UN: 银联所有类型支付
        ///     UN_APP: 银联APP支付
        ///     UN_WEB: 银联网页支付
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
            string payQueryUrl = "http://58.211.191.123:8080/1/rest/bills";//BCCache.Instance.bestHost + BCConstants.version + BCConstants.billsURL;

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
        /// <param name="timestamp">签名生成时间
        ///     时间戳，毫秒数，13位， 可以使用BCUtil.GetTimeStamp()方法获取
        ///     必填
        /// </param>
        /// <param name="channel">渠道类型
        ///     根据不同场景选择不同的支付方式
        ///     必填
        ///     可以通过enum BCPay.QueryChannel获取
        ///     channel的参数值含义：
        ///     WX: 微信所有类型支付
        ///     WX_APP: 微信手机APP支付
        ///     WX_NATIVE: 微信公众号二维码支付
        ///     WX_JSAPI: 微信公众号支付
        ///     ALI: 支付宝所有类型支付
        ///     ALI_APP: 支付宝APP支付
        ///     ALI_WEB: 支付宝网页支付 
        ///     ALI_QRCODE: 支付宝内嵌二维码支付
        ///     UN: 银联所有类型支付
        ///     UN_APP: 银联APP支付
        ///     UN_WEB: 银联网页支付</param>
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
            string payQueryUrl = "http://58.211.191.123:8080/1/rest/refunds";//BCCache.Instance.bestHost + BCConstants.version + BCConstants.refundsURL;

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
        /// <param name="timestamp">签名生成时间
        ///     时间戳，毫秒数，13位， 可以使用BCUtil.GetTimeStamp()方法获取
        ///     必填
        /// </param>
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
            string refundStatusUrl = "http://58.211.191.123:8080/1/rest/refund/status";//BCCache.Instance.bestHost + BCConstants.version + BCConstants.refundStatusURL;

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
        /// 微信红包支付（定额红包）
        /// </summary>
        /// <param name="mch_billno">红包id， 格式： mch_id+yyyyMMdd+10位数字， 共28位， 10位数字每天不能重复。</param>
        /// <param name="re_openid">红包接收者的openid</param>
        /// <param name="total_amount">固定红包金额,单位为分, 取值100~20000之间</param>
        /// <param name="nick_name">商家昵称</param>
        /// <param name="send_name">红包发送方名称</param>
        /// <param name="wishing">红包祝福语</param>
        /// <param name="act_name">活动名称</param>
        /// <param name="remark">备注</param>
        /* <returns>resultCode	int	    BeeCloud返回码，0为正常，其他为错误
                    errMsg	    String	BeeCloud错误信息，正常为空串，详细错误信息参见下表
                    sendStatus	boolean	红包是否已发送， true为已发送
                    sendMsg	    String	红包发送的错误信息，包括：‘该用户已达到发送红包上限: count_per_user’，‘该用户随机未成功， 概率为: probability’
                    return_code	String	微信红包状态，SUCCESS代表发送成功或者该mch_billno对应的红包之前已发送成功
                    result_code	String	本次发送是否成功，与return_code都是SUCCESS代表本次红包发送成功（之前没有发过）
                    return_msg	String	红包发送的消息返还，例如"发送成功"，"失败原因"等
           </returns>*/
        public static RedPackResult BCRedPack(string mch_billno, string re_openid, int total_amount, string nick_name, string send_name, string wishing, string act_name, string remark)
        {
            string wx_redpack_url = BCCache.Instance.bestHost + BCConstants.version + BCConstants.wx_red_url;

            RedPackPara para = new RedPackPara();
            para.appId = BCCache.Instance.appId;
            para.appSign = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, BCUtil.GetTimeStamp(DateTime.Now).ToString());
            para.mch_billno = mch_billno;
            para.re_openid = re_openid;
            para.total_amount = total_amount;
            para.nick_name = nick_name;
            para.send_name = send_name;
            para.wishing = wishing;
            para.remark = remark;
            para.act_name = act_name;


            string paraString = BCPrivateUtil.ObjectToJson(para);
            string url = wx_redpack_url + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
            HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, 0, null, null);
            string respString = BCPrivateUtil.GetResponseString(response);

            object result = new RedPackResult();
            result = BCPrivateUtil.JsonToObject(respString, result);

            return result as RedPackResult;
        }

        /// <summary>
        /// 微信红包支付（支持随机红包）
        /// </summary>
        /// <param name="mch_billno">红包id， 格式： mch_id+yyyyMMdd+10位数字， 共28位， 10位数字每天不能重复。</param>
        /// <param name="re_openid">红包接收者的openid</param>
        /// <param name="total_amount">固定红包金额,单位为分, 取值100~20000之间</param>
        /// <param name="nick_name">商家昵称</param>
        /// <param name="send_name">红包发送方名称</param>
        /// <param name="wishing">红包祝福语</param>
        /// <param name="act_name">活动名称</param>
        /// <param name="remark">备注</param>
        /// <param name="countPerUser">每个用户在一定时间范围内所能获取的红包个数, 与period配合生效	默认值1</param>
        /// <param name="minA">随机红包下限, 单位为分，取值100~20000，小于或等于max</param>
        /// <param name="maxA">随机红包上限, 大于或等于min, 当total_amount不传， 且max, min都存在时，随机红包生效</param>
        /// <param name="probability">代表用户有多大的可能抢到该红包，0.0-1.0之间	默认值1.0，表示一定能抢到红包</param>
        /// <param name="period">与count_per_user配合生效，代表过去一定时间范围内每个用户所能获取的最大红包数，单位为毫秒，比如3600000代表过去一个小时内，每个用户最多只能获得count_per_user个红包	默认值当前时间戳，表示无时间限制</param>
        /* <returns>resultCode	int	    BeeCloud返回码，0为正常，其他为错误
                    errMsg	    String	BeeCloud错误信息，正常为空串，详细错误信息参见下表
                    sendStatus	boolean	红包是否已发送， true为已发送
                    sendMsg	    String	红包发送的错误信息，包括：‘该用户已达到发送红包上限: count_per_user’，‘该用户随机未成功， 概率为: probability’
                    return_code	String	微信红包状态，SUCCESS代表发送成功或者该mch_billno对应的红包之前已发送成功
                    result_code	String	本次发送是否成功，与return_code都是SUCCESS代表本次红包发送成功（之前没有发过）
                    return_msg	String	红包发送的消息返还，例如"发送成功"，"失败原因"等
           </returns>*/
        public static RedPackResult BCRedPackExtra(string mch_billno, string re_openid, int? total_amount, string nick_name, string send_name, string wishing, string act_name, string remark, int? countPerUser, int? minA, int? maxA, double? probability, long? period)
        {
            string wx_redpack_extra_url = BCCache.Instance.bestHost + BCConstants.version + BCConstants.wx_red_extra_url;

            RedPackExtraPara para = new RedPackExtraPara();
            para.appId = BCCache.Instance.appId;
            para.appSign = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, BCUtil.GetTimeStamp(DateTime.Now).ToString());
            para.mch_billno = mch_billno;
            para.re_openid = re_openid;
            para.total_amount = total_amount;
            para.nick_name = nick_name;
            para.send_name = send_name;
            para.wishing = wishing;
            para.remark = remark;
            para.act_name = act_name;
            para.count_per_user = countPerUser;
            para.min = minA;
            para.max = maxA;
            para.probability = probability;
            para.period = period;

            string paraString = BCPrivateUtil.ObjectToJson(para);
            string url = wx_redpack_extra_url + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
            HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, 0, null, null);
            string respString = BCPrivateUtil.GetResponseString(response);

            object result = new RedPackResult();
            result = BCPrivateUtil.JsonToObject(respString, result);

            return result as RedPackResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partner_trade_no"></param>
        /// <param name="openid"></param>
        /// <param name="check_name"></param>
        /// <param name="re_user_name"></param>
        /// <param name="amount"></param>
        /// <param name="desc"></param>
        /// <returns></returns>
        public static MchPayResult BCMchPay(string partner_trade_no, string openid, string check_name, string re_user_name, int amount, string desc) 
        {
            string mchPayUrl = BCCache.Instance.bestHost + BCConstants.version + BCConstants.wx_mch_pay_url;
            //mchPayUrl = "http://192.168.1.103:8080/1/pay/wxmp/mchPay";
            MchPayPara para = new MchPayPara();
            para.appId = BCCache.Instance.appId;
            para.appSign = BCPrivateUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, BCUtil.GetTimeStamp(DateTime.Now).ToString());
            para.partner_trade_no = partner_trade_no;
            para.openid = openid;
            para.check_name = check_name;
            para.re_user_name = re_user_name;
            para.amount = amount;
            para.desc = desc;

            string paraString = BCPrivateUtil.ObjectToJson(para);
            string url = mchPayUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
            try
            {
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, 0, null, null);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    string respString = BCPrivateUtil.GetResponseString(response);
                    object result = new MchPayResult();
                    result = BCPrivateUtil.JsonToObject(respString, result);
                    return result as MchPayResult;
                }
                else
                {
                    BCPrivateUtil.checkBestHostForFail();
                    MchPayResult result = new MchPayResult();
                    result.resultCode = -1;
                    result.errMsg = "服务器错误";
                    return result;
                }
            }
            catch
            {
                BCPrivateUtil.checkBestHostForFail();
                MchPayResult result = new MchPayResult();
                result.resultCode = -1;
                result.errMsg = "服务器错误";
                return result;
            }
            
        }
    }
}
