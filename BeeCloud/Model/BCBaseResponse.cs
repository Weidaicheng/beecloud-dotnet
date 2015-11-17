using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model
{
    public class BCBaseResponse
    {
        public int result_code { get; set; }
        public string result_msg { get; set; }
        public string err_detail { get; set; }
    }
}
