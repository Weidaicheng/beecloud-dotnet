using BeeCloud;
using LitJson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BeeCloudSDKDemo
{
    public partial class notify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BeeCloud.BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c", "e14ae2db-608c-4f8b-b863-c8c18953eef2", "4bfdd244-574d-4bf3-b034-0c751ed34fee");
            BeeCloud.BeeCloud.setTestMode(false);

            byte[] byts = new byte[Request.InputStream.Length];
            Request.InputStream.Read(byts, 0, byts.Length);
            string req = System.Text.Encoding.UTF8.GetString(byts);
            req = Server.UrlDecode(req);

            JsonData requestData = JsonMapper.ToObject(req);

            string signature = requestData["signature"].ToString();//签名
            long timestamp = long.Parse(requestData["timestamp"].ToString());//时间戳
            string transactionID = requestData["transaction_id"].ToString();//交易单号
            string channelType = requestData["channel_type"].ToString();//渠道
            string subChannelType = requestData["sub_channel_type"].ToString();//子渠道
            string transactionType = requestData["transaction_type"].ToString();//支付还是退款还是打款或者订阅
            string tradeSuccess = requestData["trade_success"].ToString();//是否成功
            int transactionFee = int.Parse(requestData["transaction_fee"].ToString());//金额
            //如需取用message_detail的内容
            //JsonData messageDetail = requestData["message_detail"];

            //检查timestamp是否在可信时间段内，阻止在该时间段外重复发送请求
            TimeSpan ts = DateTime.Now - BCUtil.GetDateTime(timestamp);
            
            //验签， 确保来自BeeCloud
            //根据当前模式使用验签方法
            //生产环境：
            string mySign = BCUtil.GetSign(transactionID, transactionType, channelType, transactionFee);
            //测试环境：  string mySign = BCUtil.GetSignByTestMode(requestData["timestamp"].ToString());
            if (ts.TotalSeconds < 300 && mySign == signature)
            {
                //在处理自己的业务逻辑前，要做以下几步
                // 1. 过滤重复的webhook，如果该webhook的订单之前已经处理过，则忽略新的webhook（渠道有一定几率重复发送相同的webhook）
                // 2. 验证订单金额，客户需要验证Webhook中的 transaction_fee （实际的交易金额）与客户内部系统中的相应订单的金额匹配。

                if (transactionType == "PAY")//收款成功的webhook
                {

                    if (channelType == "ALI")
                    {
                        //......
                    }
                    if (channelType == "UN")
                    {
                        //
                    }
                    if (channelType == "WX")
                    {
                        //
                    }
                    if (channelType == "BC")
                    {
                        //subChannelType是BC_SUBSCRIPTION时候，说明是比可订阅支付
                        if (subChannelType == "BC_SUBSCRIPTION")
                        {
                            //订阅扣款成功的webhook样例
                            //{
                            //    "sign": "b366eda3e40aa396c1445ede5f418223", 
                            //    "timestamp": 1469674005187, 
                            //    "transaction_id": "238b156b-563b-495e-b649-3b2c671595e8", 
                            //    "retryCounter": 0, 
                            //    "notifyUrl": "https://notify.beecloud.cn/2/pay/callback/subscription/webhook", 
                            //    "transaction_fee": 150, 
                            //    "sub_channel_type": "BC_SUBSCRIPTION", 
                            //    "transaction_type": "PAY", 
                            //    "channel_type": "BC", 
                            //    "notify_url": "https://notify.beecloud.cn/2/pay/callback/subscription/webhook", 
                            //    "toSign": "95d87fff-989c-4426-812c-21408644cf888aaad136-b899-4793-9564-0ebc72ae86f2", 
                            //    "message_detail": {
                            //        "subscription_id": "e0ecd9cc-0fe4-4a1d-bd2f-c36e7d430c50", 
                            //        "id_no": "230826198601240832", 
                            //        "card_no": "***************0486", 
                            //        "err_msg": "", 
                            //        "mobile": "15555511114", 
                            //        "bank_name": "中国银行", 
                            //        "buyer_id": "this_is_a_buyer_id", 
                            //        "card_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", 
                            //        "id_name": "xxx", 
                            //        "plan_id": "1a73bf15-e227-4d75-8b0f-894629149945"
                            //    }, 
                            //    "trade_success": true
                            //}
                        }
                    }
                }
                else if (transactionType == "SUBSCRIPTION")//订阅成功的webhook
                {
                    if (channelType == "SUBSCRIPTION")
                    {
                        //subChannelType是BC_SUBSCRIPTION时候，说明是比可订阅支付
                        if (subChannelType == "BC_SUBSCRIPTION")
                        {
                            //订阅成功的webhook样例
                            //{
                            //    "transaction_id": "78500a22-2da1-4302-b55e-7cf2cc01791c", 
                            //    "sign": "b366eda3e40aa396c1445ede5f418223", 
                            //    "timestamp": 1469674005187, 
                            //    "retryCounter": 0, 
                            //    "notifyUrl": "https://notify.beecloud.cn/2/pay/callback/subscription/webhook", 
                            //    "transaction_fee": 0, 
                            //    "sub_channel_type": "BC_SUBSCRIPTION", 
                            //    "transaction_type": "SUBSCRIPTION", 
                            //    "channel_type": "BC", 
                            //    "notify_url": "https://notify.beecloud.cn/2/pay/callback/subscription/webhook", 
                            //    "toSign": "95d87fff-989c-4426-812c-21408644cf888aaad136-b899-4793-9564-0ebc72ae86f2", 
                            //    "message_detail": {
                            //        "subscription_id": "78500a22-2da1-4302-b55e-7cf2cc01791c", 
                            //        "id_no": "230826198601240832", 
                            //        "card_no": ****************0486", 
                            //        "err_msg": "", 
                            //        "mobile": "15555511114", 
                            //        "bank_name": "中国银行", 
                            //        "buyer_id": "this_is_a_buyer_id", 
                            //        "card_id": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx", 
                            //        "id_name": "高健峰", 
                            //        "plan_id": "1a73bf15-e227-4d75-8b0f-894629149945"
                            //    }, 
                            //    "trade_success": true
                            //}
                            //收到订阅成功的webhook，说明用户的卡已经完成订阅，这时可以去把本地的subscription记录中的status改成success，然后发短信告诉用户他订阅成功了巴拉巴拉
                        }
                    }
                }
                //当验签成功后务必返回success字样，通知server获取成功。
                Response.Write("success");
            }
        }
    }
}
