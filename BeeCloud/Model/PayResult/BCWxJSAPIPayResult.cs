
namespace BeeCloud.Model
{
    public class BCWxJSAPIPayResult : BCPayResult
    {
        /// <summary>
        /// 微信应用APPID
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 微信支付打包参数
        /// </summary>
        public string package { get; set; }
        /// <summary>
        /// 随机字符串
        /// </summary>
        public string noncestr { get; set; }
        /// <summary>
        /// 当前毫秒时间戳，13位
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string paySign { get; set; }
        /// <summary>
        /// 签名类型，固定为MD5
        /// </summary>
        public string signType { get; set; }
    }
}
