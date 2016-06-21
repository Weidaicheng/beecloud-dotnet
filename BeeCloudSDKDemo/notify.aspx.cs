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

            string sign = requestData["sign"].ToString();//签名
            long timestamp = long.Parse(requestData["timestamp"].ToString());//时间戳
            string channelType = requestData["channel_type"].ToString();//渠道
            string transactionType = requestData["transaction_type"].ToString();//支付还是退款还是打款
            string tradeSuccess = requestData["trade_success"].ToString();//是否成功
            int transactionFee = int.Parse(requestData["transaction_fee"].ToString());//金额
            //如需取用message_detail的内容
            //JsonData messageDetail = requestData["message_detail"];

            //检查timestamp是否在可信时间段内，阻止在该时间段外重复发送请求
            TimeSpan ts = DateTime.Now - BCUtil.GetDateTime(timestamp);
            
            //验签， 确保来自BeeCloud
            //根据当前模式使用验签方法
            //生产环境：
            string mySign = BCUtil.GetSign(requestData["timestamp"].ToString());
            //测试环境：  string mySign = BCUtil.GetSignByTestMode(requestData["timestamp"].ToString());
            if (ts.TotalSeconds < 300 && mySign == sign)
            {
                //在处理自己的业务逻辑前，要做以下几步
                // 1. 过滤重复的webhook，如果该webhook的订单之前已经处理过，则忽略新的webhook（渠道有一定几率重复发送相同的webhook）
                // 2. 验证订单金额，客户需要验证Webhook中的 transaction_fee （实际的交易金额）与客户内部系统中的相应订单的金额匹配。

                
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
                //当验签成功后务必返回success字样，通知server获取成功。
                Response.Write("success");
            }
        }
    }
}
