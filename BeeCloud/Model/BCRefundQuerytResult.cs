using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCRefundQuerytResult
    {
        public int resultCode { get; set; }
        public string resultMsg { get; set; }
        public string errDetail { get; set; }
        public int count { get; set; }
        public List<BCRefund> refunds { get; set; }
    }
}
