using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeeCloud.Model.BCSubscription
{
    public class BCPlan
    {
        public string ID { get; set; }
        public int fee { get; set; }
        public string interval { get; set; }
        public string name { get; set; }
        public string currency { get; set; }
        public int? intervalCount { get; set; }
        public int? trialDays { get; set; }
        public bool valid { get; set; }
        public Dictionary<string, string> optional { get; set; }

        public BCPlan() { }

        public BCPlan(int _fee, string _interval, string _name)
        {
            fee = _fee;
            interval = _interval;
            name = _name;
        }
    }
}
