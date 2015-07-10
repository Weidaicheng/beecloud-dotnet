using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCPayQueryResult
    {
        public int resultCode { get; set; }
        public string resultMsg { get; set; }
        public string errDetail { get; set; }
        public int count { get; set; }
        public List<BCBill> bills { get; set; }
    }
}
