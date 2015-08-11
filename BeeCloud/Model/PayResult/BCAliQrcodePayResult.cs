
namespace BeeCloud.Model
{
    public class BCAliQrcodePayResult : BCPayResult
    {
        //支付宝跳转form，是一段HTML代码，自动提交
        public string html { get; set; }
        //支付宝内嵌二维码地址，是一个URL
        public string url { get; set; }
    }
}
