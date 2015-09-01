using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCYEEPayResult : BCPayResult
    {
        //易宝支付地址，是一个URL
        public string url { get; set; }
    }
}
