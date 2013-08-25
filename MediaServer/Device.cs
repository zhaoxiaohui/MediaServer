using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaServer
{
    class Device : INotifyPropertyChanged
    {
        private string shortUrl;
        private string longUrl;
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }
        public Device(string ipport, string channel) {
            this.shortUrl = ipport + "_" + channel;
            this.longUrl = "rtsp://admin:12345@" + ipport + "/h264/ch" + channel + "/main/av_stream";
        }

        /// <summary>
        /// 短URL ip:port_ch1
        /// </summary>
        public string ShortUrl {
            get { return shortUrl; }
            set {
                shortUrl = value;
                NotifyPropertyChanged("ShortUrl");
            }
        }
        /// <summary>
        /// 长URL rtsp://admin:12345@ip:port/h264/ch1/main/av_stream
        /// </summary>
        public string LongUrl {
            get { return longUrl; }
            set {
                longUrl = value;
                NotifyPropertyChanged("LongUrl");
            }
        }

    }
    /// 
    /// 盘符信息
    /// 
    public class HardDiskPartition
    {
        #region Data
        private string _PartitionName;
        private double _FreeSpace;
        private double _SumSpace;
        #endregion //Data

        #region Properties
        /// 
        /// 空余大小
        /// 
        public double FreeSpace
        {
            get { return _FreeSpace; }
            set { this._FreeSpace = value; }
        }
        /// 
        /// 使用空间
        /// 
        public double UseSpace
        {
            get { return _SumSpace - _FreeSpace; }
        }
        /// 
        /// 总空间
        /// 
        public double SumSpace
        {
            get { return _SumSpace; }
            set { this._SumSpace = value; }
        }
        /// 
        /// 分区名称
        /// 
        public string PartitionName
        {
            get { return _PartitionName; }
            set { this._PartitionName = value; }
        }
        /// 
        /// 是否主分区
        /// 
        public bool IsPrimary
        {
            get
            {
                //判断是否为系统安装分区
                if (System.Environment.GetEnvironmentVariable("windir").Remove(2) == this._PartitionName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        #endregion //Properties
    }
}
