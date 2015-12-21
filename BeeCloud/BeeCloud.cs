using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Linq;


namespace BeeCloud
{

    public static class BeeCloud
    {
        /// <summary>
        /// 注册APP，masterSecret为需要退款/打款功能时注册
        /// </summary>
        public static void registerApp(string appID, string appSecret, string masterSecret) 
        {
            BCCache.Instance.appId = appID;
            BCCache.Instance.appSecret = appSecret;
            BCCache.Instance.masterSecret = masterSecret;
        }

        public static void setNetworkTimeout(int timeout)
        {
            BCCache.Instance.networkTimeout = timeout;
        }

        
    }
}
