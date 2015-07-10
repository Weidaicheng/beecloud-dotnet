using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BeeCloud;
using BeeCloud.Model;

namespace MchPayDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BeeCloud.BeeCloud.registerApp("c37d661d-7e61-49ea-96a5-68c34e83db3b", "c37d661d-7e61-49ea-96a5-68c34e83db3b");
            MchPayResult data = BCPay.BCMchPay("20150520abcdef019", "o3kKrjlUsMnv__cK5DYZMl0JoAkY", null, null, 1, "白开水");
            if (data.resultCode == 0)
            {
                Console.WriteLine("success");
            }
            else
            {
                Console.WriteLine("failed: " + data.errMsg + "\n");
            }
            Console.ReadKey();
        }
    }
}
