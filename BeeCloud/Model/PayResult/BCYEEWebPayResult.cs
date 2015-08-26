using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCYEEWebPayResult : BCPayResult
    {
        //易宝跳转form，是一段HTML代码，自动提交
        public string html { get; set; }
        //易宝支付地址，是一个URL
        public string url { get; set; }
    }
}
