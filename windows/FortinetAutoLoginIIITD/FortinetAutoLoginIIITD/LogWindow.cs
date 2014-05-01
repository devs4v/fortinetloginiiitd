using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortinetAutoLoginIIITD
{
    public partial class LogWindow : Form
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        private void LogWindow_Load(object sender, EventArgs e)
        {
            rtb_Log.Text = File.ReadAllText("debug.txt");
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            rtb_Log.Text = "";
            using (StreamWriter writer = new StreamWriter("debug.txt",false))
            {
                writer.WriteLine("New Log Cleared : " + Environment.NewLine);
            }
        }
    }
}
