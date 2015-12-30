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

            byte[] byts = new byte[Request.InputStream.Length];
            Request.InputStream.Read(byts, 0, byts.Length);
            string req = System.Text.Encoding.Default.GetString(byts);
            req = Server.UrlDecode(req);

            JsonData requestData = JsonMapper.ToObject(req);

            string sign = requestData["sign"].ToString();
            long timestamp = long.Parse(requestData["timestamp"].ToString());
            string channelType = requestData["channel_type"].ToString();
            string transactionType = requestData["transaction_type"].ToString();
            string tradeSuccess = requestData["trade_success"].ToString();

            //检查timestamp是否在可信时间段内，阻止在该时间段外重复发送请求
            TimeSpan ts = DateTime.Now - BCUtil.GetDateTime(timestamp);
            
            //验签， 确保来自BeeCloud
            string mySign = BCUtil.GetSign(requestData["timestamp"].ToString());
            if (ts.TotalSeconds < 300 && mySign == sign)
            {
                // 此处需要验证购买的产品与订单金额是否匹配:
                // 验证购买的产品与订单金额是否匹配的目的在于防止黑客反编译了iOS或者Android app的代码，
                // 将本来比如100元的订单金额改成了1分钱，开发者应该识别这种情况，避免误以为用户已经足额支付。
                // Webhook传入的消息里面应该以某种形式包含此次购买的商品信息，比如title或者optional里面的某个参数说明此次购买的产品是一部iPhone手机，
                // 开发者需要在客户服务端去查询自己内部的数据库看看iPhone的金额是否与该Webhook的订单金额一致，仅有一致的情况下，才继续走正常的业务逻辑。
                // 如果发现不一致的情况，排除程序bug外，需要去查明原因，防止不法分子对你的app进行二次打包，对你的客户的利益构成潜在威胁。
                // 如果发现这样的情况，请及时与我们联系，我们会与客户一起与这些不法分子做斗争。而且即使有这样极端的情况发生，
                // 只要按照前述要求做了购买的产品与订单金额的匹配性验证，在你的后端服务器不被入侵的前提下，你就不会有任何经济损失。

                JsonData messageDetail = requestData["message_detail"];
                if (channelType == "ALI")
                {
                    string bc_appid = messageDetail["bc_appid"].ToString();
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
