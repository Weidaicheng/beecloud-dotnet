using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace BeeCloud
{
    public class BCUtil
    {
        public static long GetTimeStamp(DateTime date)
        {
            long unixTimestamp = date.ToUniversalTime().Ticks - new DateTime(1970, 1, 1, 0,0,0, DateTimeKind.Utc).Ticks;
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

        public static string GetSign(string timestamp)
        {
            string input = BCCache.Instance.appId + BCCache.Instance.appSecret + timestamp;
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5").ToLower();
            return sign;
        }

        public static string GetPaySignForJSAPI(string appid, string titile, string amount, string outTradeNo, string secret)
        {
            string input = appid + titile + amount + outTradeNo + secret;
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5").ToLower();
            return sign;
        }
    }
}