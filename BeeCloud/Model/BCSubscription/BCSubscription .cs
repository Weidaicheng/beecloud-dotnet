using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model.BCSubscription
{
    public class BCSubscription
    {
        public string ID { get; set; }
        public string buyerID { get; set; }
        public string planID { get; set; }
        public string cardID { get; set; }
        public string bankName { get; set; }
        public string cardNo { get; set; }
        public string IDName { get; set; }
        public string IDNo { get; set; }
        public string mobile { get; set; }
        public double amount { get; set; }
        public string couponID { get; set; }
        public long trialEnd { get; set; }
        public Dictionary<string, string> optional { get; set; }

        public bool valid { get; set; }
        public string status { get; set; }
        public string last4 { get; set; }
        public bool cancelAtPeriodEnd { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public BCSubscription() { }

        /// <summary>
        /// 已有cardID时的初始化方法
        /// </summary>
        /// <param name="_buyerID">购买者ID</param>
        /// <param name="_planID">订阅计划ID</param>
        /// <param name="_cardID">用户卡ID</param>
        public BCSubscription(string _buyerID, string _planID, string _cardID)
        {
            buyerID = _buyerID;
            planID = _planID;
            cardID = _cardID;
        }

        /// <summary>
        /// 只知道用户卡号等信息时的初始化方法
        /// </summary>
        /// <param name="_buyerID">购买者ID</param>
        /// <param name="_planID">订阅计划ID</param>
        /// <param name="_bankName">银行名称，可以通过getCommonBanks/getBanks方法获取</param>
        /// <param name="_cardNo">卡号</param>
        /// <param name="_IDName">卡持有人的姓名</param>
        /// <param name="_IDNo">卡持有人的身份证号</param>
        /// <param name="_mobile">持卡人预留银行卡的手机号</param>
        public BCSubscription(string _buyerID, string _planID, string _bankName, string _cardNo, string _IDName, string _IDNo, string _mobile)
        {
            buyerID = _buyerID;
            planID = _planID;
            bankName = _bankName;
            cardNo = _cardNo;
            IDName = _IDName;
            IDNo = _IDNo;
            mobile = _mobile;
        }
    }
}
