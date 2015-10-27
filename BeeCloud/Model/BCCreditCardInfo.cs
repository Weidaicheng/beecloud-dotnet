using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public enum CardType
    {
        visa,
        mastercard,
        discover,
        amex
    };

    public class BCCreditCardInfo
    {
        //由于json序列化后服务端需要蛇形命名，所以这里用蛇形命名
        //卡号
        public string card_number { get; set; }
        //过期时间中的月
        public int expire_month { get; set; }
        //过期时间中的年
        public int expire_year { get; set; }
        //信用卡的三位cvv码
        public int cvv { get; set; }
        //用户名字
        public string first_name { get; set; }
        //用户的姓
        public string last_name { get; set; }
        //卡类别 visa/mastercard/discover/amex
        public string card_type { get; set; }
    }
}
