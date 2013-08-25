using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaServer
{
    public partial class MediaServerHelp : Form
    {
        public MediaServerHelp()
        {
            InitializeComponent();
        }

        private void MediaServerHelp_Load(object sender, EventArgs e)
        {
            this.labAuthor.Text = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), true)).Company;
            this.labVersion.Text = Application.ProductVersion;
            this.labCopyright.Text = ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCopyrightAttribute), true)).Copyright;
            String help = "";
            using (StreamReader sr = new StreamReader(@"help.txt", UnicodeEncoding.GetEncoding("GB2312")))
            { 
                help = sr.ReadToEnd();
            }
            this.txtHelp.Text = help;
            this.txtHelp.Select(0, 0);

        }
    }
}
