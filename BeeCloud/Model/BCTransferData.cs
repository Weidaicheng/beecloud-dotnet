using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCTransferData
    {
        //以下所有字段在创建时都是必填的
 
        //付款流水号，32位以内数字字母
        public string transferId { get; set; }
        //收款方支付宝账号
        public string receiverAccount { get; set; }
        //收款方支付宝账户名
        public string receiverName { get; set; }
        //付款金额，单位为分
        public int transferFee { get; set; }
        //付款备注
        public string transferNote { get; set; }
    }
}
