using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCPayQueryResult
    {
        //返回码，0为正常
        public int resultCode { get; set; }
        //返回信息， OK为正常
        public string resultMsg { get; set; }
        //具体错误信息
        public string errDetail { get; set; }
        //查询退款结果数量
        public int count { get; set; }
        //订单列表
        public List<BCBill> bills { get; set; }
    }
}
