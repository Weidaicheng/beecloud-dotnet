
namespace BeeCloud.Model
{
    public class BCAliQrcodePayResult : BCPayResult
    {
        /// <summary>
        /// 支付宝跳转form，是一段HTML代码，自动提交
        /// </summary>
        public string html { get; set; }
        /// <summary>
        /// 支付宝内嵌二维码地址，是一个URL
        /// </summary>
        public string url { get; set; }
    }
}
