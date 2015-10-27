
namespace BeeCloud.Model
{
    public class BCPayPalResult : BCPayResult
    {
        //当channel 为PAYPAL_PAYPAL时返回，跳转支付的url
        public string url { get; set; }
        //当channel为PAYPAL_CREDITCARD时返回， 信用卡id
        public string creditCardId { get; set; }
    }
}
