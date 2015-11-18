
namespace BeeCloud
{
    internal static class BCConstants
    {
        public const string version = "/2/";
        public const string wx_red_url = "pay/wxmp/redPack";
        public const string wx_red_extra_url = "pay/wxmp/redPackExtra";
        public const string wx_mch_pay_url = "pay/wxmp/mchPay";

        //new API
        public const string billURL = "rest/bill";
        public const string billsURL = "rest/bills"; 
        public const string refundURL = "rest/refund";
        public const string refundsURL = "rest/refunds";
        public const string refundStatusURL = "rest/refund/status";
        public const string transferURL = "rest/transfer";
        public const string transfersURL = "rest/transfers";
        public const string internationalURL = "rest/international/bill";
    }
}
