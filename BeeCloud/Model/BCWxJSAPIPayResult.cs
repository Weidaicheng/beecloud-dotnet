
namespace BeeCloud.Model
{
    public class BCWxJSAPIPayResult:BCPayResult
    {
        public string appId { get; set; }
        public string package { get; set; }
        public string noncestr { get; set; }
        public string timestamp { get; set; }
        public string paySign { get; set; }
        public string signType { get; set; }
    }
}
