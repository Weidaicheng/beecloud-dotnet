
namespace BeeCloud
{
    internal sealed class BCCache
    {
        private BCCache() { }

        //public static readonly BCCache Instance = new BCCache();
        public static BCCache Instance
        {
            get
            {
                //todo:从数据源（比如数据库）中读取配置
                return new BCCache();
            }
        }

        public string appId { get; set; }
        public string appSecret { get; set; }
        public string masterSecret { get; set; }
        public string testSecret { get; set; }

        private bool _testMode = false;
        public bool testMode
        {
            get
            {
                return _testMode;
            }
            set
            {
                _testMode = value;
            }
        }

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
