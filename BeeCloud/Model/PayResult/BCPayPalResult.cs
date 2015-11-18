
namespace BeeCloud.Model
{
    public class BCPayPalResult : BCPayResult
    {
        /// <summary>
        /// 当channel 为PAYPAL_PAYPAL时返回，跳转支付的url
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// 当channel为PAYPAL_CREDITCARD时返回， 信用卡id
        /// </summary>
        public string creditCardId { get; set; }
    }
}
