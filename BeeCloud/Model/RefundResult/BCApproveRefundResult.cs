using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCApproveRefundResult
    {
        
        /// <summary>
        ///
        /// result_code	result_msg	            含义
        ///           8	NO_SUCH_BILL	        没有该订单
        ///           9	BILL_UNSUCCESS	        该订单没有支付成功
        ///           10	REFUND_EXCEED_TIME	    已超过可退款时间
        ///           11	ALREADY_REFUNDING	    该订单已有正在处理中的退款
        ///           12	REFUND_AMOUNT_TOO_LARGE	提交的退款金额超出可退额度
        ///           13	NO_SUCH_REFUND	        没有该退款记录
        /// </summary>
        public int resultCode { get; set; }
        /// <summary>
        /// 返回信息，OK为正常
        /// </summary>
        public string resultMsg { get; set; }
        /// <summary>
        /// 具体错误信息
        /// </summary>
        public string errDetail { get; set; }
        /// <summary>
        /// 每条退款请求的状态, 成功显示OK，失败显示错误信息
        /// </summary>
        public Dictionary<string, string> status { get; set; }

        /// <summary>
        /// 当channel为ALI_APP、ALI_WEB、ALI_QRCODE时，以下字段在result_code为0时有返回
        /// 支付宝退款地址，需用户在支付宝平台上手动输入支付密码处理
        /// </summary>
        public string url { get; set; }
    }
}
