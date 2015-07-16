
namespace BeeCloud.Model
{
    public class BCAliWebPayResult:BCPayResult
    {
        //支付宝跳转form，是一段HTML代码，自动提交
        public string html { get; set; }
        //支付宝跳转url，推荐使用html
        public string url { get; set; }
    }
}
