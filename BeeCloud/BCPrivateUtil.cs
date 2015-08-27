using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Web.Security;
using System.Collections.Generic;
using System.Linq;

namespace BeeCloud
{
    internal class BCPrivateUtil
    {
        private delegate void getBestHostDelegate();
        
        public static List<string> mLocalDefaultHosts = new List<string>(){
            "https://apisz.beecloud.cn",   //深圳
            "https://apihz.beecloud.cn",    //杭州
            "https://apiqd.beecloud.cn",    //青岛
            "https://apibj.beecloud.cn"     //北京
            //"http://58.211.191.123:8080",
            //"http://58.211.191.123:8080",
            //"http://58.211.191.123:8080",
            //"http://58.211.191.123:8080"
            };

        /// <summary>
        /// 生成AppSign
        /// </summary>
        public static string getAppSignature(string appId, string appSerect, string timestamp)
        {
            string input = appId + timestamp + appSerect;
            string sign = FormsAuthentication.HashPasswordForStoringInConfigFile(input, "MD5").ToLower();
            return sign;
        }

        /// <summary>
        /// 从一个对象信息生成Json串
        /// </summary>
        public static string ObjectToJson(object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            return Encoding.UTF8.GetString(dataBytes);
        }

        /// <summary>
        /// 从一个Json串生成对象信息
        /// </summary>
        public static object JsonToObject(string jsonString, object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }

        /// <summary>
        /// 创建GET方式的HTTP请求 
        /// </summary>
        public static HttpWebResponse CreateGetHttpResponse(string url, int timeout, string userAgent, CookieCollection cookies)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";

            //设置代理UserAgent和超时
            //request.UserAgent = userAgent;
            //request.Timeout = timeout;
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            return request.GetResponse() as HttpWebResponse;
        }

        public static HttpWebResponse CreatePostHttpResponse(String url, String payload)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";

            // Encode the data
            byte[] encodedBytes = Encoding.UTF8.GetBytes(payload);
            request.ContentLength = encodedBytes.Length;
            request.ContentType = "application/json";

            // Write encoded data into request stream
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(encodedBytes, 0, encodedBytes.Length);
            requestStream.Close();

            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                
                return reader.ReadToEnd();

            }
            
        }

        public static Dictionary<string, int> bestHostDic = new Dictionary<string, int>();

        /// <summary>
        /// 获取当前最佳的BeeCloud服务器地址
        /// </summary>
        public static void getBestHost()
        {
            bestHostDic.Clear();
            foreach (string host in mLocalDefaultHosts)
            {
                bestHostDic.Add(host, getLtt(host));
            }
            bestHostDic = (from entry in bestHostDic orderby entry.Value ascending select entry).ToDictionary(pair => pair.Key, pair => pair.Value);
            BCCache.Instance.bestHost = bestHostDic.First().Key;
        }

        private static int getLtt(string host)
        {
            string url = host + "/status";
            DateTime startTime = DateTime.Now;
            try
            {
                HttpWebResponse response = BCPrivateUtil.CreateGetHttpResponse(url, BCCache.Instance.networkTimeout, null, null);
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    return (DateTime.Now - startTime).Milliseconds;
                }
                else
                {
                    return BCCache.Instance.networkTimeout;
                }
            }
            catch (Exception e)
            {
                return BCCache.Instance.networkTimeout;
            }

        }

        public static void startMeasurement()
        {
            getBestHostDelegate task = BCPrivateUtil.getBestHost;
            IAsyncResult asyncResult = task.BeginInvoke(getBestHostCallback, task);
        }

        public static void checkBestHostForFail()
        {
            BCCache.Instance.bestHost = BCPrivateUtil.bestHostDic.ElementAtOrDefault(1).Key;
            startMeasurement();
        }

        private static void getBestHostCallback(IAsyncResult ar)
        {
            getBestHostDelegate dlgt = (getBestHostDelegate)ar.AsyncState;
            dlgt.EndInvoke(ar);
        }
    }
}
