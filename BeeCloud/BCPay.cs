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

        public static long GetTimeStamp(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerMillisecond;
            return unixTimestamp;
        }

        private static DateTime GetDateTime(long timestamp)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddMilliseconds(timestamp);
            return time;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="channel"></param>
        /// <param name="totalFee"></param>
        /// <param name="billNo"></param>
        /// <param name="title"></param>
        /// <param name="optional"></param>
        /// <param name="returnUrl"></param>
        /// <param name="openId"></param>
        /// <param name="showURL"></param>
        /// <param name="qrPayMode"></param>
        /// <returns></returns>
        public static BCPayResult BCPayByChannel(long timestamp, string channel, int totalFee, string billNo, string title, Dictionary<string,string> optional, string returnUrl,string openId, string showURL, string qrPayMode)
        {
            string payUrl = "http://192.168.1.103:8080/1/rest/pay";//BCCache.Instance.bestHost + BCConstants.version + BCConstants.rest_pay;

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
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
                HttpWebResponse response = BCUtil.CreatePostHttpResponse(payUrl, paraString);

                string respString = BCUtil.GetResponseString(response);

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
                result.resultCode = 100;
                result.resultMsg = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="channel"></param>
        /// <param name="type"></param>
        /// <param name="refundNo"></param>
        /// <param name="billNo"></param>
        /// <param name="refundFee"></param>
        /// <param name="agree"></param>
        /// <param name="optional"></param>
        /// <returns></returns>
        public static BCRefundResult BCRefundByChannel(long timestamp, string channel, string type, string refundNo, string billNo, string refundFee, bool agree, Dictionary<string, string> optional)
        {
            string refundUrl = "http://58.211.191.123:8080/1/rest/refund";//BCCache.Instance.bestHost + BCConstants.version + BCConstants.rest_pay;

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
            data["timestamp"] = timestamp;
            data["channel"] = channel;
            data["type"] = type;
            data["refund_no"] = refundNo;
            data["bill_no"] = billNo;
            data["refund_fee"] = refundFee;
            data["agree"] = agree;
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
                HttpWebResponse response = BCUtil.CreatePostHttpResponse(refundUrl, paraString);
                string respString = BCUtil.GetResponseString(response);
                JsonData responseData = JsonMapper.ToObject(respString);
                BCRefundResult result = new BCRefundResult();
                result.resultCode = int.Parse(responseData["result_code"].ToString());
                result.resultMsg = responseData["result_msg"].ToString();
                if (responseData["result_code"].ToString() == "0")
                {
                    if (channel.Contains("ALI") && (type == "CONFIRM_REFUND" || type == "DIRECT_REFUND"))
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
                result.resultCode = 100;
                result.resultMsg = e.Message;
                return result;
            }            
        }

        public static BCPayQueryResult BCPayQueryByCondition(long timestamp, string channel, string billNo, long? startTime, long? endTime, int? skip, int? limit)
        {
            string payQueryUrl = "http://192.168.1.103:8080/1/rest/pay/query";//BCCache.Instance.bestHost + BCConstants.version + BCConstants.rest_pay;

            JsonData data = new JsonData();
            data["app_id"] = BCCache.Instance.appId;
            data["app_sign"] = BCUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, timestamp.ToString());
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
                HttpWebResponse response = BCUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout, null, null);
                string respString = BCUtil.GetResponseString(response);
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
                            bill.createdTime = GetDateTime((long)billData["created_time"]);
                            bill.billNo = billData["bill_no"].ToString();
                            bill.result = (bool)billData["spay_result"];
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
                result.resultCode = 100;
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
            para.appSign = BCUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, BCPay.GetTimeStamp(DateTime.Now).ToString());
            para.mch_billno = mch_billno;
            para.re_openid = re_openid;
            para.total_amount = total_amount;
            para.nick_name = nick_name;
            para.send_name = send_name;
            para.wishing = wishing;
            para.remark = remark;
            para.act_name = act_name;


            string paraString = BCUtil.ObjectToJson(para);
            string url = wx_redpack_url + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
            HttpWebResponse response = BCUtil.CreateGetHttpResponse(url, 0, null, null);
            string respString = BCUtil.GetResponseString(response);

            object result = new RedPackResult();
            result = BCUtil.JsonToObject(respString, result);

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
            para.appSign = BCUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, BCPay.GetTimeStamp(DateTime.Now).ToString());
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

            string paraString = BCUtil.ObjectToJson(para);
            string url = wx_redpack_extra_url + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
            HttpWebResponse response = BCUtil.CreateGetHttpResponse(url, 0, null, null);
            string respString = BCUtil.GetResponseString(response);

            object result = new RedPackResult();
            result = BCUtil.JsonToObject(respString, result);

            return result as RedPackResult;
        }

        public static MchPayResult BCMchPay(string partner_trade_no, string openid, string check_name, string re_user_name, int amount, string desc) 
        {
            string mchPayUrl = BCCache.Instance.bestHost + BCConstants.version + BCConstants.wx_mch_pay_url;
            //mchPayUrl = "http://192.168.1.103:8080/1/pay/wxmp/mchPay";
            MchPayPara para = new MchPayPara();
            para.appId = BCCache.Instance.appId;
            para.appSign = BCUtil.getAppSignature(BCCache.Instance.appId, BCCache.Instance.appSecret, BCPay.GetTimeStamp(DateTime.Now).ToString());
            para.partner_trade_no = partner_trade_no;
            para.openid = openid;
            para.check_name = check_name;
            para.re_user_name = re_user_name;
            para.amount = amount;
            para.desc = desc;

            string paraString = BCUtil.ObjectToJson(para);
            string url = mchPayUrl + "?para=" + HttpUtility.UrlEncode(paraString, Encoding.UTF8);
            try
            {
                HttpWebResponse response = BCUtil.CreateGetHttpResponse(url, 0, null, null);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    string respString = BCUtil.GetResponseString(response);
                    object result = new MchPayResult();
                    result = BCUtil.JsonToObject(respString, result);
                    return result as MchPayResult;
                }
                else
                {
                    BCUtil.checkBestHostForFail();
                    MchPayResult result = new MchPayResult();
                    result.resultCode = -1;
                    result.errMsg = "服务器错误";
                    return result;
                }
            }
            catch
            {
                BCUtil.checkBestHostForFail();
                MchPayResult result = new MchPayResult();
                result.resultCode = -1;
                result.errMsg = "服务器错误";
                return result;
            }
            
        }
    }
}
