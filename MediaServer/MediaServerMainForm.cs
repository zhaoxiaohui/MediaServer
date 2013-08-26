using Declarations;
using Implementation;
using MediaServer.OperationListen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaServer
{
    public partial class MedisServerMainForm : Form
    {
        #region 全局静态变量
        private static string outputStore = @"#transcode<vcodec=h264,vb=0,scale=0,acodec=mpga,ab=128,channels=2,samplerate=44100>" +
                                       @":std<access=file,mux=ps,dst={0}//{1}.mp4>";
        private static string outputBroadcast = @"#transcode<vcodec=h264,vb=0,scale=0,acodec=mpga,ab=128,channels=2,samplerate=44100>" +
                                        @":rtp<sdp=rtsp://:8554/{0}>";
        private static string[] options;
        #endregion
        #region 初始化操作类
        //状态
        private MediaServerStatus msStatus = new MediaServerStatus();
        //设置
        private MediaServerSettings msSettings = new MediaServerSettings();
        //设备列表
        private BindingList<Device> devices = new BindingList<Device>();
        //磁盘列表
        private List<HardDiskPartition> disks;
        #endregion

        #region 格式化需要
        [DllImport("shell32.dll")]
        private static extern int SHFormatDrive(IntPtr hWnd, int drive, long fmtID, int Options);
        public const long SHFMT_ID_DEFAULT = 0xFFFF;
        #endregion

        #region VLC C#库
        private IMediaPlayerFactory m_factory;
        private IMediaPlayerFactory m_factory_broadcast;
        private IMediaPlayerFactory m_factory_vod;
        private Dictionary<String, String> vods;
        #endregion

        #region 时间控件
        System.Timers.Timer mediaTimer;
        #endregion

        #region 操作监听
        OperationServer osListener;
        #endregion

        #region 全局判断变量
        bool m_isLoadVod = false;
        #endregion

        #region 文件夹监听 用户vod添加
        FileSystemWatcher fileWatcher = null;
        #endregion

        public MedisServerMainForm()
        {
            InitializeComponent();
            //加载盘符信息
            LoadDisks();
            //初始化VLC运行库
            InitializeVLC();
            //初始化控件
            InitializeBtns();
            //初始化默认值
            InitializeSetup();
            //初始化fileWatcher
            InitializeFileWatcher();

            
            //m_factory.
           // m_factory.VideoLanManager.AddVod("10.27.0.143_554_1_201308170801.mp4", "D:\\10.27.0.143_554_1_201308170801.mp4",options);
            //bool k = m_factory.VideoLanManager.IsMediaSeekable("10.27.0.143_554_1_201308170801.mp4");
            //m_factory.VideoLanManager.Play("10.27.0.143_554_1_201308170801.mp4");
        }
        private void InitializeVLC()
        {
            string[] args = new string[] 
             {
                "-I", 
                "dumy",  
		        "--ignore-config", 
                "--no-osd",
                "--disable-screensaver",
                "--ffmpeg-hw",
		        "--plugin-path=./plugins",
                "--rtsp-host=:554"
             };
            m_factory = new MediaPlayerFactory();
            m_factory_broadcast = new MediaPlayerFactory();
            m_factory_vod = new MediaPlayerFactory(args);
            vods = new Dictionary<string, string>();
            m_factory.VideoLanManager.Events.MediaInstancePlaying += Events_MediaInstancePlaying;
            m_factory.VideoLanManager.Events.MediaInstanceError += Events_MediaInstanceError;
            m_factory.VideoLanManager.Events.MediaInstanceStopped += Events_MediaInstanceStopped;
            m_factory_broadcast.VideoLanManager.Events.MediaInstancePlaying += Events_MediaInstancePlaying;
            m_factory_broadcast.VideoLanManager.Events.MediaInstanceError += Events_MediaInstanceError;
            m_factory_broadcast.VideoLanManager.Events.MediaInstanceStopped += Events_MediaInstanceStopped;
            m_factory_vod.VideoLanManager.Events.MediaInstancePlaying += Events_MediaInstancePlaying;
            m_factory_vod.VideoLanManager.Events.MediaInstanceError += Events_MediaInstanceError;
            m_factory_vod.VideoLanManager.Events.MediaInstanceStopped += Events_MediaInstanceStopped;
        }
        private void InitializeFileWatcher()
        {
            //初始化文件夹变化

            fileWatcher = new FileSystemWatcher();
            fileWatcher.Path = msSettings.Directory;
            fileWatcher.Filter = "*.mp4";
            fileWatcher.EnableRaisingEvents = false;
            fileWatcher.NotifyFilter = NotifyFilters.Attributes | NotifyFilters.CreationTime | NotifyFilters.DirectoryName | NotifyFilters.FileName | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite | NotifyFilters.Security | NotifyFilters.Size;
            fileWatcher.IncludeSubdirectories = false;
            fileWatcher.Created += new FileSystemEventHandler(fileWatcherEvents);
        }
        void Events_MediaInstanceStopped(object sender, Declarations.VLM.VlmEvent e)
        {
            UISync.Execute(() => labStatus.Text = "组播停止");
        }

        void Events_MediaInstanceError(object sender, Declarations.VLM.VlmEvent e)
        {

            UISync.Execute(() => labStatus.Text = "组播失败 ");
            showMessage("组播出错，请检查URL各个设备时候能正常访问");
            UISync.Execute(() => btnStop.PerformClick());
            //showMessage(e.ToString());
            /** if (msStatus.ServerStatus == STATUS.Running) msStatus.ServerStatus = STATUS.Stop;
             UISync.Execute(() => btnStart.Enabled = true);
             UISync.Execute(() => btnStop.Enabled = false);
             UISync.Execute(() => groupAdd.Enabled = true);
             UISync.Execute(() => groupSetup.Enabled = true);*/
        }


        void Events_MediaInstancePlaying(object sender, Declarations.VLM.VlmEvent e)
        {
            UISync.Execute(() => labStatus.Text = "组播完成");
        }
        private void LoadDisks()
        {
            try
            {
                disks = GetDiskListInfo();
                if (disks != null && disks.Count > 0)
                {
                    this.lstDisks.Items.Clear();
                    foreach (HardDiskPartition disk in disks)
                    {
                        this.lstDisks.Items.Add(string.Format("{0}  总空间：{1} GB,剩余:{2} GB", disk.PartitionName, ManagerDoubleValue(disk.SumSpace, 1), ManagerDoubleValue(disk.FreeSpace, 1)));
                    }
                    this.lstDisks.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 处理Double值，精确到小数点后几位
        /// </summary>
        /// <param name="_value">值</param>
        /// <param name="Length">精确到小数点后几位</param>
        /// <returns>返回值</returns>
        private double ManagerDoubleValue(double _value, int Length)
        {
            if (Length < 0)
            {
                Length = 0;
            }
            return System.Math.Round(_value, Length);
        }
        /// <summary>
        /// 获取硬盘上所有的盘符空间信息列表
        /// </summary>
        /// <returns></returns>
        private List<HardDiskPartition> GetDiskListInfo()
        {
            List<HardDiskPartition> list = null;
            //指定分区的容量信息
            try
            {
                SelectQuery selectQuery = new SelectQuery("select * from win32_logicaldisk");

                ManagementObjectSearcher searcher = new ManagementObjectSearcher(selectQuery);

                ManagementObjectCollection diskcollection = searcher.Get();
                if (diskcollection != null && diskcollection.Count > 0)
                {
                    list = new List<HardDiskPartition>();
                    HardDiskPartition harddisk = null;
                    foreach (ManagementObject disk in searcher.Get())
                    {
                        int nType = Convert.ToInt32(disk["DriveType"]);

                        harddisk = new HardDiskPartition();
                        harddisk.FreeSpace = Convert.ToDouble(disk["FreeSpace"]) / (1024 * 1024 * 1024);
                        harddisk.SumSpace = Convert.ToDouble(disk["Size"]) / (1024 * 1024 * 1024);
                        harddisk.PartitionName = disk["DeviceID"].ToString();
                        list.Add(harddisk);
                    }
                }
            }
            catch (Exception)
            {

            }
            return list;
        }

        private void InitializeBtns()
        {

            this.btnStop.Enabled = false;

            //初始化 列表
            this.lstDevics.DataSource = devices;
            this.lstDevics.DisplayMember = "ShortUrl";

        }
        private void InitializeSetup()
        {
            msStatus.ServerStatus = STATUS.Stop;
            labServerStatus.Text = "已停止";
            comTimeMinute.SelectedIndex = 2;
            msSettings.IsCycle = radDeleteYes.Checked;
            msSettings.Directory = "";
            msSettings.Duration = readTime();

            options = new string[1];
            options[0] = ":rtsp-caching=300";
            /**options[1] = ":ttl=12";
            options[2] = ":rtp-caching=300";
            */
            UISync.Init(this);

            this.toolDeleteAll.Click += toolDeleteAll_Click;
            this.toolDeleteSel.Click += toolDeleteSel_Click;

            mediaTimer = new System.Timers.Timer();
            mediaTimer.AutoReset = true;
            mediaTimer.Elapsed += mediaTimer_Elapsed;
            mediaTimer.Enabled = false;

            //初始化操作监听类
            osListener = new OperationServer(100, 2000, "");
            //osListener.Start(8556);
            //LinkNumTimer.Start();
            //checkDiskEnough()
        }
        /// <summary>
        /// 时钟TICK函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void mediaTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //如果是第一次进入
            if (msStatus.ServerStatus == STATUS.Stop)
            {
                this.msStatus.ServerStatus = STATUS.Running;
                mediaTimer.Interval = msSettings.Duration * 1000;
            }
            //检查当前盘符时候够用
            while (!checkDiskEnough())
            {
                if (msSettings.IsCycle)
                {
                    //删除以前的文件
                    deleteFiles();
                }
                else
                {//停止服务 
                    UISync.Execute(() => this.btnStop.PerformClick());
                    return;
                }
            }
            //首先停止所有
            foreach (Device device in devices)
            {
                m_factory.VideoLanManager.Stop(device.ShortUrl);
            }

            //首先设置每个流的output
            foreach (Device device in devices)
            {
                m_factory.VideoLanManager.SetOutput(device.ShortUrl, string.Format(outputStore, msSettings.Directory, device.ShortUrl + "_" + DateTime.Now.ToString("yyyyMMddHHmmss")).Replace("<", "{").Replace(">", "}"));
            }
            //UISync.Execute(() => labServerStatus.Text = "运行中" + DateTime.Now.ToLongTimeString());
            //if (msStatus.ServerStatus == STATUS.Stop) {
            //重新启动
            foreach (Device device in devices)
            {
                m_factory.VideoLanManager.Play(device.ShortUrl);
            }
            UISync.Execute(() => labServerStatus.Text = "运行中" + DateTime.Now.ToLongTimeString());

        }
        /// <summary>
        /// 程序初始时加载vod资源
        /// </summary>
        void loadVod()
        {
            m_isLoadVod = true;
            List<FileInfo> mp4Files = FileOperation.getFile(msSettings.Directory + "/", ".mp4");
            foreach (FileInfo file in mp4Files) {
                m_factory_vod.VideoLanManager.AddVod(file.Name, file.FullName, options);
            }
        }
        /// <summary>
        /// 检查盘符空间是否够用，如果不够则根据设置选择停止，或是覆盖掉以前的文件
        /// </summary>
        /// <returns></returns>
        private bool checkDiskEnough()
        {
            DriveInfo drive = new DriveInfo(msSettings.Directory);
            Double freespace = 1.0 * drive.AvailableFreeSpace / (1024 * 1024 * 1024);
            // string n = DateTime.Now.ToString("yyyy_MM_HH_mm_ss_ffff");
            if (freespace < 3.0) return false;
            else return true;
        }
        /// <summary>
        /// 按照时间排序 删除第一个文件
        /// </summary>
        private void deleteFiles()
        {
            List<FileInfo> files = FileOperation.getFile(msSettings.Directory + "/", ".mp4");
            if (files.Count > 0)
            {
                files.Sort(delegate(FileInfo file1, FileInfo file2) { return file1.CreationTime.CompareTo(file2.CreationTime); });
                this.m_factory_vod.VideoLanManager.DeleteMedia(files[0].Name);
                files[0].Delete();
                
            }
        }
        private int readTime()
        {
            int hour = int.Parse(comTimeHour.Text == "" ? "0" : comTimeHour.Text);
            int minute = int.Parse(comTimeMinute.Text == "" ? "0" : comTimeMinute.Text);
            return hour * 60 * 60 + minute * 60;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose(true);
        }
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (msStatus.ServerStatus == STATUS.Running)
            {
                stopService();
            }
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (msStatus.ServerStatus == STATUS.Running)
            {
                showMessage("服务已经启动");
            }
            else
            {
                if (msSettings.Directory == "")
                {
                    showMessage("请选择一个磁盘");
                }
                else if (msSettings.Duration == 0)
                {
                    showMessage("请设置存储文件时间间隔");
                }
                else
                {
                    if (this.devices.Count == 0)
                    {
                        showMessage("还没有添加设备，请先添加之后再操作");
                    }
                    else
                    {

                        this.btnStart.Enabled = false;
                        this.groupAdd.Enabled = false;
                        this.groupSetup.Enabled = false;
                        this.btnStop.Enabled = true;
                        startService();
                    }
                    //showMessage(msSettings.printInfo());
                }

            }
        }

        private void radDeleteNo_CheckedChanged(object sender, EventArgs e)
        {
            msSettings.IsCycle = radDeleteYes.Checked;
        }

        private void comTimeMinute_SelectedIndexChanged(object sender, EventArgs e)
        {
            msSettings.Duration = readTime();
        }

        private void comTimeHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            msSettings.Duration = readTime();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (msStatus.ServerStatus == STATUS.Running)
            {
                stopService();

                this.btnStart.Enabled = true;
                this.groupAdd.Enabled = true;
                this.groupSetup.Enabled = true;
                this.btnStop.Enabled = false;
            }
        }
        #region 服务启动关闭
        /// <summary>
        /// 停止服务
        /// </summary>
        private void stopService()
        {
            //停止文件夹监听
            fileWatcher.EnableRaisingEvents = false;
            vodTimer.Stop();
            //停止操作监听
            osListener.Stop();
            LinkNumTimer.Stop();

            foreach (Device device in devices)
            {
                this.m_factory.VideoLanManager.Stop(device.ShortUrl);
                this.m_factory_broadcast.VideoLanManager.Stop(device.ShortUrl);
            }
            mediaTimer.Stop();
            this.msStatus.ServerStatus = STATUS.Stop;
            labServerStatus.Text = "已停止";
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        private void startService()
        {
            //开启文件夹监听
            //如果还没有加载之前的文件 加载之


            //if (!m_isLoadVod) loadVod();
            fileWatcher.Path = msSettings.Directory + "/";
            fileWatcher.EnableRaisingEvents = true;
            vodTimer.Interval = 10000;
            vodTimer.Start();
            //开启操作监听
            osListener.MediaDir = msSettings.Directory + "/";
            osListener.Start(8556);
            LinkNumTimer.Start();//开始监听人数
            //设置时间
            mediaTimer.Interval = 100;
            mediaTimer.Start();
            foreach (Device device in devices)
            {
                this.m_factory_broadcast.VideoLanManager.Play(device.ShortUrl);
            }

        }
        /// <summary>
        /// 事件监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void fileWatcherEvents(object sender, FileSystemEventArgs e)
        {
            
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                vods.Add(e.Name, msSettings.Directory + "\\" + e.Name);
                //m_factory_vod.VideoLanManager.AddVod(e.Name, msSettings.Directory+"\\"+e.Name, options);
            }
        }
        #endregion
        private void btnAddDevice_Click(object sender, EventArgs e)
        {
            if (checkIPStr(this.txtInputSrc.Text))
            {
                if (comChannel.Text == "")
                {
                    showMessage("请选择通道");
                }
                else
                {
                    Device device = new Device(txtInputSrc.Text, comChannel.Text);
                    if (!checkExists(device))
                    {
                        this.devices.Add(device);
                        this.m_factory.VideoLanManager.AddBroadcast(device.ShortUrl, device.LongUrl, "", options);
                        this.m_factory_broadcast.VideoLanManager.AddBroadcast(device.ShortUrl, device.LongUrl, string.Format(outputBroadcast, device.ShortUrl.Replace("_", "/")).Replace("<", "{").Replace(">", "}"), options);
                    }
                }
            }
            else
            {
                showMessage("输入地址不对，请重新输入");
            }
        }
        /// <summary>
        /// 检查设备时候已经存在
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        private bool checkExists(Device dev)
        {
            foreach (Device device in devices)
            {
                if (device.ShortUrl == dev.ShortUrl)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 检查是否符合IP:PORT结构
        /// </summary>
        /// <param name="ipstr"></param>
        /// <returns></returns>
        private bool checkIPStr(string ipstr)
        {
            string num = "(25[0-5]|2[0-4]\\d|[0-1]\\d{2}|[1-9]?\\d)";
            return Regex.IsMatch(ipstr, ("^" + num + "\\." + num + "\\." + num + "\\." + num + ":[1-9]\\d*$"));
        }

        /// <summary>
        /// 输出信息
        /// </summary>
        /// <param name="message"></param>
        private void showMessage(string message)
        {
            MessageBox.Show(message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void lstDisks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lstDisks.SelectedIndex != -1)
            {
                this.btnClear.Visible = true;
                if (this.disks[this.lstDisks.SelectedIndex].FreeSpace < 3.0)
                {
                    this.lstDisks.SelectedIndex = -1;
                    showMessage("空间不足，请选择其他盘符，或者格式化此盘符");

                }
                else
                {
                    this.msSettings.Directory = this.disks[this.lstDisks.SelectedIndex].PartitionName;
                    fileWatcher.Path = msSettings.Directory;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要格式化此盘？", "确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    SHFormatDrive(this.Handle, this.lstDisks.SelectedIndex, SHFMT_ID_DEFAULT, 0);
                    showMessage("格式化完成");
                    //重新加载磁盘信息
                    LoadDisks();
                }
                catch
                {
                    showMessage("格式化失败");
                }
            }
        }
        #region 最小化托盘
        private void MedisServerMainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                MinimizeToTray();
        }
        private void MinimizeToTray()
        {
            Hide();
            Minimize_NotifyIcon.Visible = true;
        }

        private void Minimize_NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                RestoreFromTray();
        }
        private void RestoreFromTray()
        {
            Minimize_NotifyIcon.Visible = false;
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Minimize_NotifyIcon_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.msStatus.ServerStatus == STATUS.Running)
            {
                Minimize_NotifyIcon.Text = "服务正在运行...";
            }
        }
        #endregion
        #region 列表右键菜单
        private void lstDevics_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lstDevics.SelectedItems.Count > 0 && msStatus.ServerStatus == STATUS.Stop)
            {
                this.lstContextMenu.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        void toolDeleteSel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除吗？", "确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                ListBox.SelectedIndexCollection sels = this.lstDevics.SelectedIndices;
                if (sels.Count > 0)
                {
                    foreach (int ind in sels)
                    {
                        this.m_factory.VideoLanManager.DeleteMedia(devices[ind].ShortUrl);
                        this.m_factory_broadcast.VideoLanManager.DeleteMedia(devices[ind].ShortUrl);
                        this.devices.RemoveAt(ind);
                    }
                }
            }
        }

        void toolDeleteAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除吗？", "确认", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.devices.Clear();
                foreach (Device device in devices)
                {
                    this.m_factory.VideoLanManager.DeleteMedia(device.ShortUrl);
                    this.m_factory_broadcast.VideoLanManager.DeleteMedia(device.ShortUrl);
                }
            }
        }
        #endregion

        private void btnHelp_Click(object sender, EventArgs e)
        {
            MediaServerHelp help = new MediaServerHelp();
            help.Show();
        }

        private void LinkNumTimer_Tick(object sender, EventArgs e)
        {
            //if (msStatus.ServerStatus == STATUS.Running) {
                UISync.Execute(() => labLinkNum.Text = osListener.NumberOfConnections.ToString());
            //}
        }

        private void vodTimer_Tick(object sender, EventArgs e)
        {
            if (vods.Count > 0) { 
                foreach(KeyValuePair<string, string> item in vods){
                    this.m_factory_vod.VideoLanManager.AddVod(item.Key, item.Value, options);
                    vods.Remove(item.Key);
                }
            }
        }
    }
}
