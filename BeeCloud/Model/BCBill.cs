using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCBill
    {
        /// <summary>
        /// 订单记录的唯一标识，可用于查询单笔记录
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string billNo { get; set; }
        /// <summary>
        /// 订单金额，单位为分
        /// </summary>
        public int totalFee { get; set; }
        /// <summary>
        /// 渠道交易号， 当支付成功时有值
        /// </summary>
        public string tradeNo { get; set; }
        /// <summary>
        /// 渠道类型 WX、ALI、UN、JD、YEE、KUAIQIAN、PAYPAL、BD
        /// </summary>
        public string channel { get; set; }
        /// <summary>
        /// 子渠道类型
        /// </summary>
        public string subChannel { get; set; }
        /// <summary>
        /// 订单标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 订单是否成功
        /// </summary>
        public bool result { get; set; }
        /// <summary>
        /// 附加数据,用户自定义的参数，将会在webhook通知中原样返回，该字段是JSON格式的字符串 "{"key1":"value1","key2":"value2",...}"
        /// </summary>
        public string optional { get; set; }
        /// <summary>
        /// 渠道详细信息， 当need_detail传入true时返回
        /// </summary>
        public string messageDetail { get; set; }
        /// <summary>
        /// 订单是否撤销
        /// </summary>
        public bool revertResult { get; set; }
        /// <summary>
        /// 订单是否已经退款
        /// </summary>
        public bool refundResult { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime createdTime { get; set; }
    }
}
