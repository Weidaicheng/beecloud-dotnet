using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCJDPayResult : BCPayResult
    {
        //京东跳转form，是一段HTML代码，自动提交
        public string html { get; set; }
    }
}
