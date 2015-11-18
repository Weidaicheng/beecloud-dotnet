
namespace BeeCloud.Model
{
    public class BCAliWebPayResult : BCPayResult
    {
        /// <summary>
        /// 支付宝跳转form，是一段HTML代码，自动提交
        /// </summary>
        public string html { get; set; }
        /// <summary>
        /// 支付宝跳转url，推荐使用html
        /// </summary>
        public string url { get; set; }
    }
}
