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
using System.Net.Security;
using System.Web;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace FortinetAutoLoginIIITD
{
    public partial class MainWindow : Form
    {
        int connectionStatus;
        int minuteCount;
        bool flagLoginStatus;
        bool flagAutoLogin;
        bool flagFastNetCheck;
        System.Uri checkURL;
        System.Uri responseURI;
        System.Uri fortinetURL;
        System.Uri keepAliveURL;
        string magicLogout;
        string magicLogin;
        public MainWindow()
        {
            InitializeComponent();
            flagLoginStatus = false;
            checkURL = new Uri("http://www.bing.com/");
            fortinetURL = new Uri("https://192.168.1.99:1003/");
            flagFastNetCheck = false;
            connectionStatus = 10;
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
                flagLoginStatus = true;
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
            lbl_Status.Text = "Performing Logout.";
            performLogout();
            btn_Login.Text = "Login";
            txt_Username.Enabled = true;
            txt_Password.Enabled = true;
            flagLoginStatus = false;
            flagFastNetCheck = false;
            openURL(checkURL.OriginalString);
            timer.Interval = 5000;
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
            if (chk_DontAskAgain.Checked)
            {
                minimizeToTray();
            }
            backgroundWorker();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            saveUISettings();
            if (!flagLoginStatus)
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

        public bool IsConnectedToInternet //faster connectivity check (buggy results)
        {
            get
            {
                Uri url = new Uri("google.com");
                string pingurl = string.Format("{0}", url.Host);
                string host = pingurl;
                bool result = false;
                Ping p = new Ping();
                try
                {
                    PingReply reply = p.Send(host, 3000);
                    if (reply.Status == IPStatus.Success)
                        return true;
                }
                catch { }
                return result;
            }
        }

        private int checkConnectivity() //detailed connectivity check
        {
            string data = openURL(checkURL.OriginalString);
            if (data == "-1")
            {
                return -1;
            }
            if (data.IndexOf("<title>Bing</title>") > 0)
            {
                return 1;
            }
            else if (data.IndexOf(("<title>IIIT-Delhi Network</title>")) > 0)
            {
                return 0;
            }
            else
            {
                return -2;
            }
        }

        private void performLogin(string username, string password)
        {   //code to login
            string page = openURL(checkURL.OriginalString);
            string page_4Tredir = HttpUtility.UrlEncode(checkURL.OriginalString);
            int index = page.IndexOf("magic") + 14;
            magicLogin = page.Substring(index, 16);
            string postData = "4Tredir=" + page_4Tredir + "&magic=" + HttpUtility.UrlEncode(magicLogin)
                    + "&username=" + HttpUtility.UrlEncode(username) + "&password=" + HttpUtility.UrlEncode(password);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fortinetURL);
            request.Method = "POST";
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.UTF8))
            {
                writer.Write(postData);
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            page = reader.ReadToEnd();
            string keepAlive = fortinetURL.OriginalString + "keepalive?";
            index = page.IndexOf(keepAlive);
            keepAliveURL = new System.Uri(page.Substring(index, keepAlive.Length + 16));
            magicLogout = page.Substring(index + keepAlive.Length, 16);
            minuteCount = 0;
        }

        private void performLogout()
        {
            flagLoginStatus = false;
            openURL(fortinetURL.OriginalString + "logout?" + magicLogout);
            lbl_Status.Text = "Logged out successfully.";
            flushDNSCache();
        }

        private void btn_About_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }

        private int backgroundWorker()
        {
            progressBar.Value = 0;
            if (!flagFastNetCheck)
            {
                connectionStatus = checkConnectivity();
            }
            switch (connectionStatus)
            {
                case 1:
                    if (!flagLoginStatus)
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
                        btn_Login.Enabled = true;
                        flagFastNetCheck = true;
                        if (!IsConnectedToInternet)
                        {
                            flagFastNetCheck = false;
                        }
                        else
                        {
                            minuteCount++;
                        }
                        if (minuteCount > 30)
                        {
                            minuteCount = 0;
                            openURL(keepAliveURL.OriginalString);
                        }
                        timer.Interval = 1000 * 60; //1 Minute
                    }
                    break;
                case -1:
                    flagLoginStatus = false;
                    lbl_Status.Text = "Check Connecitivity to Internet";
                    progressBar.Value = 33;
                    btn_Login.Enabled = false;
                    timer.Interval = 1000 * 10; //10 Seconds
                    break;
                case -2:
                    flagLoginStatus = false;
                    lbl_Status.Text = "Unknown Login Detected.";
                    progressBar.Value = 33;
                    btn_Login.Enabled = false;
                    timer.Interval = 1000 * 3600; //1 Hour
                    break;
                case 0:
                    if (!flagLoginStatus)
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
            return connectionStatus;
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

        private bool IgnoreCertificateErrorHandler(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
          System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private string openURL(string urlAddress)
        {

            string webPage = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            request.AllowAutoRedirect = true;
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(IgnoreCertificateErrorHandler);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseURI = response.ResponseUri;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;
                    if (response.CharacterSet == null)
                        readStream = new StreamReader(receiveStream);
                    else
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    webPage = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                }
            }
            catch (Exception e)
            {
                return "-1";
            }
            return webPage;
        }

        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        private static extern UInt32 DnsFlushResolverCache();
        public static void flushDNSCache() //This can be named whatever name you want and is the function you will call
        {
            UInt32 result = DnsFlushResolverCache();
        }

    }
}
