using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaServer
{
    enum STATUS { 
        Running,
        Stop,
        Error
    }
    class MediaServerStatus
    {
        private STATUS _status;

        /// <summary>
        /// 服务器状态
        /// </summary>
        public STATUS ServerStatus {
            get { return _status; }
            set { _status = value; }
        }
    }
}
