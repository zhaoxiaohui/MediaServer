namespace MediaServer
{
    partial class MedisServerMainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MedisServerMainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labLinkNum = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lstDevics = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labServerStatus = new System.Windows.Forms.Label();
            this.labStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupSetup = new System.Windows.Forms.GroupBox();
            this.lstDisks = new System.Windows.Forms.ListBox();
            this.radDeleteNo = new System.Windows.Forms.RadioButton();
            this.radDeleteYes = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.comTimeMinute = new System.Windows.Forms.ComboBox();
            this.comTimeHour = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupAdd = new System.Windows.Forms.GroupBox();
            this.btnAddDevice = new System.Windows.Forms.Button();
            this.comChannel = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtInputSrc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Minimize_NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.lstContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolDeleteSel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolDeleteAll = new System.Windows.Forms.ToolStripMenuItem();
            this.LinkNumTimer = new System.Windows.Forms.Timer(this.components);
            this.vodTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupSetup.SuspendLayout();
            this.groupAdd.SuspendLayout();
            this.lstContextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labLinkNum);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnHelp);
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.btnStop);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Location = new System.Drawing.Point(1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(684, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作";
            // 
            // labLinkNum
            // 
            this.labLinkNum.Location = new System.Drawing.Point(452, 36);
            this.labLinkNum.Name = "labLinkNum";
            this.labLinkNum.Size = new System.Drawing.Size(100, 23);
            this.labLinkNum.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(334, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "当前连接服务器人数：";
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(206, 20);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 44);
            this.btnHelp.TabIndex = 4;
            this.btnHelp.Text = "帮助";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(603, 20);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 44);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "退出";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(117, 20);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 44);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止服务";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(27, 20);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 44);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "启动服务";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.lstDevics);
            this.groupBox2.Location = new System.Drawing.Point(1, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(180, 369);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "设备列表";
            // 
            // lstDevics
            // 
            this.lstDevics.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstDevics.BackColor = System.Drawing.Color.Gainsboro;
            this.lstDevics.FormattingEnabled = true;
            this.lstDevics.HorizontalScrollbar = true;
            this.lstDevics.ImeMode = System.Windows.Forms.ImeMode.On;
            this.lstDevics.ItemHeight = 12;
            this.lstDevics.Location = new System.Drawing.Point(6, 20);
            this.lstDevics.Name = "lstDevics";
            this.lstDevics.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstDevics.Size = new System.Drawing.Size(161, 340);
            this.lstDevics.TabIndex = 0;
            this.lstDevics.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstDevics_MouseUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.labServerStatus);
            this.groupBox3.Controls.Add(this.labStatus);
            this.groupBox3.Location = new System.Drawing.Point(1, 455);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(678, 42);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "状态";
            // 
            // labServerStatus
            // 
            this.labServerStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labServerStatus.AutoSize = true;
            this.labServerStatus.Location = new System.Drawing.Point(540, 18);
            this.labServerStatus.Name = "labServerStatus";
            this.labServerStatus.Size = new System.Drawing.Size(0, 12);
            this.labServerStatus.TabIndex = 1;
            // 
            // labStatus
            // 
            this.labStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(11, 19);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(0, 12);
            this.labStatus.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "存储文件时间间隔：";
            // 
            // groupSetup
            // 
            this.groupSetup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupSetup.Controls.Add(this.lstDisks);
            this.groupSetup.Controls.Add(this.radDeleteNo);
            this.groupSetup.Controls.Add(this.radDeleteYes);
            this.groupSetup.Controls.Add(this.label5);
            this.groupSetup.Controls.Add(this.btnClear);
            this.groupSetup.Controls.Add(this.label4);
            this.groupSetup.Controls.Add(this.comTimeMinute);
            this.groupSetup.Controls.Add(this.comTimeHour);
            this.groupSetup.Controls.Add(this.label3);
            this.groupSetup.Controls.Add(this.label2);
            this.groupSetup.Controls.Add(this.label1);
            this.groupSetup.Location = new System.Drawing.Point(187, 78);
            this.groupSetup.Name = "groupSetup";
            this.groupSetup.Size = new System.Drawing.Size(498, 221);
            this.groupSetup.TabIndex = 4;
            this.groupSetup.TabStop = false;
            this.groupSetup.Text = "设置";
            // 
            // lstDisks
            // 
            this.lstDisks.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstDisks.FormattingEnabled = true;
            this.lstDisks.ItemHeight = 16;
            this.lstDisks.Location = new System.Drawing.Point(125, 76);
            this.lstDisks.Name = "lstDisks";
            this.lstDisks.Size = new System.Drawing.Size(291, 100);
            this.lstDisks.TabIndex = 19;
            this.lstDisks.SelectedIndexChanged += new System.EventHandler(this.lstDisks_SelectedIndexChanged);
            // 
            // radDeleteNo
            // 
            this.radDeleteNo.AutoSize = true;
            this.radDeleteNo.Location = new System.Drawing.Point(195, 189);
            this.radDeleteNo.Name = "radDeleteNo";
            this.radDeleteNo.Size = new System.Drawing.Size(35, 16);
            this.radDeleteNo.TabIndex = 18;
            this.radDeleteNo.Text = "否";
            this.radDeleteNo.UseVisualStyleBackColor = true;
            this.radDeleteNo.CheckedChanged += new System.EventHandler(this.radDeleteNo_CheckedChanged);
            // 
            // radDeleteYes
            // 
            this.radDeleteYes.AutoSize = true;
            this.radDeleteYes.Checked = true;
            this.radDeleteYes.Location = new System.Drawing.Point(125, 189);
            this.radDeleteYes.Name = "radDeleteYes";
            this.radDeleteYes.Size = new System.Drawing.Size(35, 16);
            this.radDeleteYes.TabIndex = 17;
            this.radDeleteYes.TabStop = true;
            this.radDeleteYes.Text = "是";
            this.radDeleteYes.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 191);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 16;
            this.label5.Text = "是否循环删除：";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(417, 139);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 37);
            this.btnClear.TabIndex = 14;
            this.btnClear.Text = "格式化选中";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "存储文件存放位置：";
            // 
            // comTimeMinute
            // 
            this.comTimeMinute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comTimeMinute.FormattingEnabled = true;
            this.comTimeMinute.Items.AddRange(new object[] {
            "0",
            "1",
            "15",
            "30",
            "45",
            "60"});
            this.comTimeMinute.Location = new System.Drawing.Point(195, 24);
            this.comTimeMinute.Name = "comTimeMinute";
            this.comTimeMinute.Size = new System.Drawing.Size(35, 20);
            this.comTimeMinute.TabIndex = 11;
            this.comTimeMinute.SelectedIndexChanged += new System.EventHandler(this.comTimeMinute_SelectedIndexChanged);
            // 
            // comTimeHour
            // 
            this.comTimeHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comTimeHour.FormattingEnabled = true;
            this.comTimeHour.Items.AddRange(new object[] {
            "0",
            "1"});
            this.comTimeHour.Location = new System.Drawing.Point(125, 24);
            this.comTimeHour.Name = "comTimeHour";
            this.comTimeHour.Size = new System.Drawing.Size(35, 20);
            this.comTimeHour.TabIndex = 10;
            this.comTimeHour.SelectedIndexChanged += new System.EventHandler(this.comTimeHour_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "分";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(172, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "时";
            // 
            // groupAdd
            // 
            this.groupAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupAdd.Controls.Add(this.btnAddDevice);
            this.groupAdd.Controls.Add(this.comChannel);
            this.groupAdd.Controls.Add(this.label7);
            this.groupAdd.Controls.Add(this.txtInputSrc);
            this.groupAdd.Controls.Add(this.label6);
            this.groupAdd.Location = new System.Drawing.Point(187, 305);
            this.groupAdd.Name = "groupAdd";
            this.groupAdd.Size = new System.Drawing.Size(492, 142);
            this.groupAdd.TabIndex = 5;
            this.groupAdd.TabStop = false;
            this.groupAdd.Text = "设备添加";
            // 
            // btnAddDevice
            // 
            this.btnAddDevice.Location = new System.Drawing.Point(347, 101);
            this.btnAddDevice.Name = "btnAddDevice";
            this.btnAddDevice.Size = new System.Drawing.Size(75, 23);
            this.btnAddDevice.TabIndex = 20;
            this.btnAddDevice.Text = "添加";
            this.btnAddDevice.UseVisualStyleBackColor = true;
            this.btnAddDevice.Click += new System.EventHandler(this.btnAddDevice_Click);
            // 
            // comChannel
            // 
            this.comChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comChannel.FormattingEnabled = true;
            this.comChannel.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            "64"});
            this.comChannel.Location = new System.Drawing.Point(107, 77);
            this.comChannel.Name = "comChannel";
            this.comChannel.Size = new System.Drawing.Size(59, 20);
            this.comChannel.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 80);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "输入源通道：";
            // 
            // txtInputSrc
            // 
            this.txtInputSrc.Location = new System.Drawing.Point(107, 34);
            this.txtInputSrc.Name = "txtInputSrc";
            this.txtInputSrc.Size = new System.Drawing.Size(309, 21);
            this.txtInputSrc.TabIndex = 1;
            this.txtInputSrc.Text = "192.168.2.1:554";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "输入源地址:端口：";
            // 
            // Minimize_NotifyIcon
            // 
            this.Minimize_NotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("Minimize_NotifyIcon.Icon")));
            this.Minimize_NotifyIcon.Text = "流媒体服务器";
            this.Minimize_NotifyIcon.Visible = true;
            this.Minimize_NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Minimize_NotifyIcon_MouseClick);
            this.Minimize_NotifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Minimize_NotifyIcon_MouseMove);
            // 
            // lstContextMenu
            // 
            this.lstContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolDeleteSel,
            this.toolDeleteAll});
            this.lstContextMenu.Name = "lstContextMenu";
            this.lstContextMenu.Size = new System.Drawing.Size(125, 48);
            // 
            // toolDeleteSel
            // 
            this.toolDeleteSel.Name = "toolDeleteSel";
            this.toolDeleteSel.Size = new System.Drawing.Size(124, 22);
            this.toolDeleteSel.Text = "删除选中";
            // 
            // toolDeleteAll
            // 
            this.toolDeleteAll.Name = "toolDeleteAll";
            this.toolDeleteAll.Size = new System.Drawing.Size(124, 22);
            this.toolDeleteAll.Text = "删除全部";
            // 
            // LinkNumTimer
            // 
            this.LinkNumTimer.Tick += new System.EventHandler(this.LinkNumTimer_Tick);
            // 
            // vodTimer
            // 
            this.vodTimer.Interval = 1000;
            this.vodTimer.Tick += new System.EventHandler(this.vodTimer_Tick);
            // 
            // MedisServerMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 499);
            this.Controls.Add(this.groupAdd);
            this.Controls.Add(this.groupSetup);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MedisServerMainForm";
            this.Text = "流媒体";
            this.Resize += new System.EventHandler(this.MedisServerMainForm_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupSetup.ResumeLayout(false);
            this.groupSetup.PerformLayout();
            this.groupAdd.ResumeLayout(false);
            this.groupAdd.PerformLayout();
            this.lstContextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lstDevics;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupSetup;
        private System.Windows.Forms.RadioButton radDeleteNo;
        private System.Windows.Forms.RadioButton radDeleteYes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comTimeMinute;
        private System.Windows.Forms.ComboBox comTimeHour;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupAdd;
        private System.Windows.Forms.ComboBox comChannel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtInputSrc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAddDevice;
        private System.Windows.Forms.ListBox lstDisks;
        private System.Windows.Forms.Label labServerStatus;
        private System.Windows.Forms.NotifyIcon Minimize_NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip lstContextMenu;
        private System.Windows.Forms.ToolStripMenuItem toolDeleteSel;
        private System.Windows.Forms.ToolStripMenuItem toolDeleteAll;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label labLinkNum;
        private System.Windows.Forms.Timer LinkNumTimer;
        private System.Windows.Forms.Timer vodTimer;
    }
}

