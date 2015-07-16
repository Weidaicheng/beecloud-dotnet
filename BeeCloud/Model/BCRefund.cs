using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCRefund
    {
        //订单号
        public string billNo { get; set; }
        //退款号
        public string refundNo { get; set; }
        //订单标题
        public string title { get; set; }
        //订单金额，单位为分
        public int totalFee { get; set; }
        //退款金额，单位为分
        public int refundFee { get; set; }
        //WX、WX_NATIVE、WX_JSAPI、WX_APP、ALI、ALI_APP、ALI_WEB、ALI_QRCODE、UN、UN_APP、UN_WEB
        public string channel { get; set; }
        //退款是否完成
        public bool finish { get; set; }
        //退款是否成功
        public bool result { get; set; }
        //退款创建时间
        public DateTime createdTime { get; set; }
    }
}
