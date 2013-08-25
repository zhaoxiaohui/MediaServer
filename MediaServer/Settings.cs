using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaServer
{
    class MediaServerSettings
    {
        private int _duration = 0;
        private string _directory;
        private bool _isCycle = true;
        private string _ipport = ":5544";
        /// <summary>
        /// 时间间隔（秒）
        /// </summary>
        public int Duration {
            get { return _duration; }
            set { _duration = value; }
        }

        /// <summary>
        /// 存储路径
        /// </summary>
        public string Directory {
            get { return _directory; }
            set { _directory = value; }
        }
        /// <summary>
        /// 是否循环删除
        /// </summary>
        public bool IsCycle {
            get { return _isCycle; }
            set { _isCycle = value; }
        }
        public string IPPort {
            get { return _ipport; }
            set { _ipport = value; }
        }
        /// <summary>
        /// 测试信息
        /// </summary>
        /// <returns></returns>
        public string printInfo() {
            return string.Format("Duration:{0}, Directory:{1}, IsCycle:{2}, Ip:port{3}", Duration, Directory, IsCycle, IPPort);
        }

        
    }
}
