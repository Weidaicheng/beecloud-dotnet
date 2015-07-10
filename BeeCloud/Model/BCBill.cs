using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCBill
    {
        public string billNo { get; set; }
        public int totalFee { get; set; }
        public string title { get; set; }
        public bool result { get; set; }
        public DateTime createdTime { get; set; }
    }
}
