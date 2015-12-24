
namespace BeeCloud
{
    internal sealed class BCCache
    {
        private BCCache() { }

        public static readonly BCCache Instance = new BCCache();

        public string appId { get; set; }
        public string appSecret { get; set; }
        public string masterSecret { get; set; }

        private int _newworkTimeout = 30000;
        public int networkTimeout
        {
            get
            {
                return _newworkTimeout;
            }
            set
            {
                _newworkTimeout = value;
            }
        }

    }
}
