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

        private void saveUISettings()
        {
            Properties.Settings.Default.Username = txt_Username.Text;
            Properties.Settings.Default.Password = txt_Password.Text;
            Properties.Settings.Default.AutoLogin = chk_DontAskAgain.Checked;
            Properties.Settings.Default.Save();
        }

        private void loadUISettings()
        {
            chk_DontAskAgain.Checked = Properties.Settings.Default.AutoLogin;
            txt_Username.Text = Properties.Settings.Default.Username;
            txt_Password.Text = Properties.Settings.Default.Password;
        }

        private void minimizeToTray()
        {
            notification.Visible = true;
            notification.ShowBalloonTip(500);
            this.Hide();
        }

        private void restoreFromTray()
        {
            this.Show();
            notification.Visible = false;
        }

        private void login()
        {
            lbl_Status.Text = "Performing Login";
            performLogin(txt_Username.Text, txt_Password.Text);
            int eval = checkConnectivity();
            if (eval == 1)
            {
                btn_Login.Text = "Logout";
                txt_Username.Enabled = false;
                txt_Password.Enabled = false;
                loginStatus = true;
                lbl_Status.Text = "You've successfully logged in.";
                progressBar.Value = 100;
            }
            else
            {
                lbl_Status.Text = "Login Failed. Check your credentials or connection.";
            }
        }

        private void logout()
        {
            lbl_Status.Text = "Performing Logout";
            performLogout();
            btn_Login.Text = "Login";
            txt_Username.Enabled = true;
            txt_Password.Enabled = true;
            loginStatus = false;
        }

        private void btn_Tray_Click(object sender, EventArgs e)
        {
            minimizeToTray();
        }

        private void restoreWindow(object sender, EventArgs e)
        {
            restoreFromTray();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            loadUISettings();
            backgroundWorker();
            if (chk_DontAskAgain.Checked)
            {
                login();
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            saveUISettings();
            performLogin(txt_Username.Text, txt_Password.Text);
            if (!loginStatus)
            {
                login();
                if (chk_DontAskAgain.Checked)
                {
                    minimizeToTray();
                }
            }
            else
            {
                logout();
            }
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

        private void performLogin(string username, string password)
        {

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
                    if (!loginStatus)
                    {
                        lbl_Status.Text = "Connected to direct internet!";
                        progressBar.Value = 100;
                        btn_Login.Enabled = false;
                        timer.Interval = 1000 * 60; //1 Minute
                    }
                    else
                    {
                        lbl_Status.Text = "Logged in @ IIITD!";
                        progressBar.Value = 100;
                        btn_Login.Enabled = false;
                        timer.Interval = 1000 * 60; //1 Minute
                    }
                    break;
                case -1:
                    loginStatus = false;
                    lbl_Status.Text = "Check Connecitivity to Internet";
                    progressBar.Value = 33;
                    btn_Login.Enabled = false;
                    timer.Interval = 1000 * 10; //10 Seconds
                    break;
                case 0:
                    if (!loginStatus)
                    {
                        progressBar.Value = 33;
                        btn_Login.Enabled = true;
                        lbl_Status.Text = "Login to proceed.";
                        timer.Interval = 1000 * 60; //1 Minute
                    }
                    else
                    {
                        progressBar.Value = 33;
                        btn_Login.Enabled = true;
                        lbl_Status.Text = "Performing Automatic Login.";
                        performLogin(txt_Username.Text, txt_Password.Text);
                        timer.Interval = 1000 * 60 * 10; //10 Minutes
                    }
                    break;
            }
            return value;
        }

        private void onTick(object sender, EventArgs e)
        {
            lbl_Status.Text = "Checking things again.";
            backgroundWorker();
        }

        private void chk_DontAskAgain_CheckedChanged(object sender, EventArgs e)
        {
            saveUISettings();
        }
    }
}
