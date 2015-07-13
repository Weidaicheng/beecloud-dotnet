using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BeeCloud
{
    public class BCUtil
    {
        public static long GetTimeStamp(DateTime date)
        {
            long unixTimestamp = date.Ticks - new DateTime(1970, 1, 1).Ticks;
            unixTimestamp /= TimeSpan.TicksPerMillisecond;
            return unixTimestamp;
        }

        public static DateTime GetDateTime(long timestamp)
        {
            DateTime time = DateTime.MinValue;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            time = startTime.AddMilliseconds(timestamp);
            return time;
        }

        public static string GetUUID()
        {
            Guid g = Guid.NewGuid();
            string uuid = g.ToString().Replace("-", "");
            return uuid;
        }
    }
}