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

            Random random = new Random();
            BCCache.Instance.bestHost = BCUtil.mLocalDefaultHosts[random.Next(0,4)];

            BCUtil.getBestHost();
        }

        public static void setNetworkTimeout(int timeout)
        {
            BCCache.Instance.networkTimeout = timeout;
        }

        
    }
}
