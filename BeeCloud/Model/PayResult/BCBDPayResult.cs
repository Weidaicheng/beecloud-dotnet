using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCBDPayResult :  BCPayResult
    {
        /// <summary>
        /// 支付跳转页
        /// </summary>
        public string url { get; set; }
    }
}
