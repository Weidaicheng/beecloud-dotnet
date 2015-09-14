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
            BeeCloud.BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c");

            byte[] byts = new byte[Request.InputStream.Length];
            Request.InputStream.Read(byts, 0, byts.Length);
            string req = System.Text.Encoding.Default.GetString(byts);
            req = Server.UrlDecode(req);

            JsonData requestData = JsonMapper.ToObject(req);

            string sign = requestData["sign"].ToString();
            long timestamp = long.Parse(requestData["timestamp"].ToString());
            string channelType = requestData["channelType"].ToString();
            string transactionType = requestData["transactionType"].ToString();
            string tradeSuccess = requestData["tradeSuccess"].ToString();

            //检查timestamp是否在可信时间段内，阻止重放
            TimeSpan ts = DateTime.Now - BCUtil.GetDateTime(timestamp);
            
            //验签， 确保来自BeeCloud
            string mySign = BCUtil.GetSign(requestData["timestamp"].ToString());
            if (ts.TotalSeconds < 300 && mySign == sign)
            {
                JsonData messageDetail = requestData["messageDetail"];
                if (channelType == "AlI")
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