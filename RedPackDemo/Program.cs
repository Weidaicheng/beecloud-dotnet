using System;
using BeeCloud;
using BeeCloud.Model;

namespace RedPackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string mch_id = "1234275402";
            string date = "20150421";
            string str = "0000000007";
            string mch_billno = mch_id + date + str;

            BeeCloud.BeeCloud.registerApp("c5d1cba1-5e3f-4ba0-941d-9b0a371fe719", "39a7a518-9ac8-4a9e-87bc-7885f33cf18c");
            //RedPackResult data = BCPay.BCRedPack(mch_billno, "o3kKrjmoHicB9nnAK7pcSUBBhXv8", 101, "nick", "nick", "中文", "act", "remark");
            RedPackResult data = BCPay.BCRedPackExtra(mch_billno, "o3kKrjmoHicB9nnAK7pcSUBBhXv8", null, "nick", "nick", "中文", "act", "remark", 60, 200, 400, 0.9, null);
            if (data.resultCode == 0)
            {
                if (data.sendStatus == true)
                {
                    Console.WriteLine("success: " + data.return_msg);
                }
                else
                {
                    Console.WriteLine("Failed: " + data.sendMsg);
                }
            }
            else 
            {
                Console.WriteLine("failed: " + data.errMsg + "\n" + data.return_msg);    
            }
            Console.ReadKey();
        }
    }
}
