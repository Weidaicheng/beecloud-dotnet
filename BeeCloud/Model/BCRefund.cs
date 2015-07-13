using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCRefund
    {
        public string billNo { get; set; }
        public string title { get; set; }
        public string refundNo { get; set; }
        public int totalFee { get; set; }
        public int refundFee { get; set; }
        public bool finish { get; set; }
        public bool result { get; set; }
        public DateTime createdTime { get; set; }
    }
}
