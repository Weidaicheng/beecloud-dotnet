using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Linq;


namespace BeeCloud
{

    public static class BeeCloud
    {
        public static void registerApp(string appID, string appSecret)
        {
            BCCache.Instance.appId = appID;
            BCCache.Instance.appSecret = appSecret;
        }

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
