
namespace BeeCloud.Model
{
    public class BCWxJSAPIPayResult : BCPayResult
    {
        //微信应用APPID
        public string appId { get; set; }
        //微信支付打包参数
        public string package { get; set; }
        //随机字符串
        public string noncestr { get; set; }
        //当前毫秒时间戳，13位
        public string timestamp { get; set; }
        //签名
        public string paySign { get; set; }
        //签名类型，固定为MD5
        public string signType { get; set; }
    }
}
