using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FortinetAutoLoginIIITD
{
    public partial class DevOps : Form
    {
        public DevOps()
        {
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.checkURL = txt_CheckURL.Text;
            Properties.Settings.Default.fortinetURL = txt_FortinetURL.Text;
            Properties.Settings.Default.flagFastNetCheck = chk_FastInternetCheck.Checked;
            Properties.Settings.Default.Save();
        }

        private void DevOps_Load(object sender, EventArgs e)
        {
            txt_CheckURL.Text = Properties.Settings.Default.checkURL;
            txt_FortinetURL.Text = Properties.Settings.Default.fortinetURL;
            chk_FastInternetCheck.Checked = Properties.Settings.Default.flagFastNetCheck;
        }
    }
}
