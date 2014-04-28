using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace FortinetAutoLoginIIITD
{
    public partial class MainWindow : Form
    {
        bool loginStatus;
        public MainWindow()
        {
            InitializeComponent();
            loginStatus = false;
        }

        private void btn_Tray_Click(object sender, EventArgs e)
        {
            notification.Visible = true;
            notification.ShowBalloonTip(500);
            this.Hide();
        }

        private void restoreWindow(object sender, EventArgs e)
        {
            this.Show();
            notification.Visible = false;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            chk_DontAskAgain.Checked = Properties.Settings.Default.AutoLogin;
            txt_Username.Text = Properties.Settings.Default.Username;
            txt_Password.Text = Properties.Settings.Default.Password;
            backgroundWorker();
            if (chk_DontAskAgain.Checked)
            {
                performLogin(txt_Username.Text, txt_Password.Text);
                btn_Login.Text = "Logout";
                loginStatus = true;
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Username = txt_Username.Text;
            Properties.Settings.Default.Password = txt_Password.Text;
            Properties.Settings.Default.AutoLogin = chk_DontAskAgain.Checked;
            Properties.Settings.Default.Save();
            performLogin(txt_Username.Text, txt_Password.Text);
            loginStatus = true;
            if (chk_DontAskAgain.Checked)
            {
                notification.Visible = true;
                notification.ShowBalloonTip(500);
                this.Hide();
            }
            if (loginStatus)
            {
                lbl_Status.Text = "Performing Login";
                performLogin(txt_Username.Text, txt_Password.Text);
                btn_Login.Text = "Logout";
            }
            else
            {
                lbl_Status.Text = "Performing Logout";
                performLogout();
                btn_Login.Text = "Login";
            }
        }

        private void performLogin(string username, string password)
        {

        }

        private int checkConnectivity()
        {
            WebClient client = new WebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            try
            {
                Stream data = client.OpenRead("http://www.wikipedia.org/");
                StreamReader reader = new StreamReader(data);
                string s = reader.ReadToEnd();
                if (s.IndexOf("<title>Wikipedia</title>") > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                return -1;
            }
        }

        private void performLogout()
        {

        }

        private void btn_About_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }

        private int backgroundWorker()
        {
            progressBar.Value = 0;
            int value = checkConnectivity();
            switch (value)
            {
                case 1:
                    if (loginStatus == false)
                    {
                        lbl_Status.Text = "Connected to direct internet!";
                        progressBar.Value = 100;
                        btn_Login.Enabled = false;
                    }
                    else
                    {
                        lbl_Status.Text = "Connected to direct internet!";
                        progressBar.Value = 100;
                        btn_Login.Enabled = false;
                    }
                    break;
                case -1:
                    loginStatus = false;
                    lbl_Status.Text = "Check Connecitivity to Internet";
                    progressBar.Value = 33;
                    btn_Login.Enabled = false;
                    break;
                case 0:
                    progressBar.Value = 33;
                    btn_Login.Enabled = true;
                    lbl_Status.Text = "Login to proceed.";
                    break;
            }
            return value;
        }
    }
}
