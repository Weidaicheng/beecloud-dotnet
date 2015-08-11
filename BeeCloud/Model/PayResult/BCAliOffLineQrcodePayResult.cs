using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCAliOffLineQrcodePayResult : BCPayResult
    {
        //二维码码串,可以用二维码生成工具根据该码串值生成对应的二维码
        public string qrCode { get; set; }
    }
}
