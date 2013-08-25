using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaServer.HCNetSDK
{
    class HCNetSDKCMD
    {
        public CHCNetSDK.NET_DVR_DEVICEINFO_V30 m_struDeviceInfo;
        public Int32 m_lUserID = -1;
        private bool m_bInitSDK = false;

        /// <summary>
        /// 初始化
        /// </summary>
        public HCNetSDKCMD()
        {
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                throw new  Exception("NET_DVR_Init error!");
            }
            else
            {
            }
        }
        /// <summary>
        /// 析构函数
        /// </summary>
        ~HCNetSDKCMD()
        {
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout_V30(m_lUserID);
            }
            if (m_bInitSDK == true)
            {
                CHCNetSDK.NET_DVR_Cleanup();
            }
        }
        /// <summary>
        /// 设备登陆
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public Int32 Login(String ip, Int16 port, String username = "admin", String password = "12345")
        {
            if (m_lUserID >= 0) CHCNetSDK.NET_DVR_Logout(m_lUserID);
            m_lUserID = CHCNetSDK.NET_DVR_Login_V30(ip, port, username, password, ref m_struDeviceInfo);
            return m_lUserID;
        }
        /// <summary>
        /// 获得设备的通道号 设备必须先执行Login
        /// </summary>
        /// <returns></returns>
        public Int32 GetChannelNumber() 
        {
            if (m_lUserID >= 0)
            {
                return m_struDeviceInfo.byChanNum;
            }
            else return -1;
        }

        #region 云台控制
        /// <summary>
        /// 向上旋转 按下状态
        /// </summary>
        /// <param name="channel"></param>
        public void PTZ_UP_btnDown(Int32 channel=0) 
        {
            PtzControl(channel, CHCNetSDK.TILT_UP, 0);
        }
        /// <summary>
        /// 向上旋转 停止按下
        /// </summary>
        /// <param name="channel"></param>
        public void PTZ_UP_btnUp(Int32 channel = 0)
        {
            PtzControl(channel, CHCNetSDK.TILT_UP, 1);
        }
        /// <summary>
        /// 向左旋转 按下状态
        /// </summary>
        /// <param name="channel"></param>
        public void PTZ_LEFT_btnDown(Int32 channel = 0)
        {
            PtzControl(channel, CHCNetSDK.PAN_LEFT, 0);
        }
        /// <summary>
        /// 向左旋转 停止按下
        /// </summary>
        /// <param name="channel"></param>
        public void PTZ_LEFT_btnUp(Int32 channel = 0)
        {
            PtzControl(channel, CHCNetSDK.PAN_LEFT, 1);
        }
        /// <summary>
        /// 向右旋转 按下状态
        /// </summary>
        /// <param name="channel"></param>
        public void PTZ_RIGHT_btnDown(Int32 channel = 0) 
        {
            PtzControl(channel, CHCNetSDK.PAN_RIGHT, 0);
        }
        /// <summary>
        /// 向右旋转 停止按下
        /// </summary>
        /// <param name="channel"></param>
        public void PTZ_RIGHT_btnUp(Int32 channel = 0)
        {
            PtzControl(channel, CHCNetSDK.PAN_RIGHT, 1);
        }
        /// <summary>
        /// 向下旋转 按下状态
        /// </summary>
        /// <param name="channel"></param>
        public void PTZ_DOWN_btnDown(Int32 channel = 0)
        {
            PtzControl(channel, CHCNetSDK.TILT_DOWN, 0);
        }
        /// <summary>
        /// 向下旋转 停止按下
        /// </summary>
        /// <param name="channel"></param>
        public void PTZ_DOWN_btnUp(Int32 channel = 0)
        {
            PtzControl(channel, CHCNetSDK.TILT_DOWN, 1);
        }
        #endregion

        ///////////////////////////////////////////////////////////
        ///////////////////////////////private/////////////////////
        ///////////////////////////////////////////////////////////
        private bool PtzControl(Int32 channel, UInt32 dwPtzCommand, UInt32 dwStop)
        {
            if (m_lUserID >= 0)
            {
                if (CHCNetSDK.NET_DVR_PTZControl_Other(m_lUserID, channel, dwPtzCommand, dwStop))
                {

                }
                else
                {
                    return false;
                }
                return true;
            }
            else return false;
        }
    }
}
