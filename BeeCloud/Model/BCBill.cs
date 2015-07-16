using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCBill
    {
        //订单号
        public string billNo { get; set; }
        //订单金额，单位为分
        public int totalFee { get; set; }
        //订单标题
        public string title { get; set; }
        //订单是否成功
        public bool result { get; set; }
        //WX、WX_NATIVE、WX_JSAPI、WX_APP、ALI、ALI_APP、ALI_WEB、ALI_QRCODE、UN、UN_APP、UN_WEB
        public string channel { get; set; }
        //订单创建时间
        public DateTime createdTime { get; set; }
    }
}
