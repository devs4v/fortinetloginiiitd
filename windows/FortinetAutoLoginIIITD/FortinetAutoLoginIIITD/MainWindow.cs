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
            using (StreamWriter writer = new StreamWriter("debug.txt", false))
            {
                writer.WriteLine("New Log Cleared : " + Environment.NewLine);
            }
            flagLoginStatus = false;
            connectionStatus = 10;
        }

        private void saveSettings()
        {
            Properties.Settings.Default.Username = txt_Username.Text;
            Properties.Settings.Default.Password = txt_Password.Text;
            Properties.Settings.Default.AutoLogin = chk_DontAskAgain.Checked;
            Properties.Settings.Default.Save();
        }

        private void loadSettings()
        {
            chk_DontAskAgain.Checked = Properties.Settings.Default.AutoLogin;
            txt_Username.Text = Properties.Settings.Default.Username;
            txt_Password.Text = Properties.Settings.Default.Password;
            checkURL = new Uri(Properties.Settings.Default.checkURL);
            fortinetURL = new Uri(Properties.Settings.Default.fortinetURL);
            flagFastNetCheck = Properties.Settings.Default.flagFastNetCheck;
        }

        private void minimizeToTray()
        {
            notification.Visible = true;
            notification.ShowBalloonTip(500);
            this.Hide();
            log("Window to tray");
        }

        private void restoreFromTray()
        {
            this.Show();
            notification.Visible = false;
            log("Window Visible");
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
            loadSettings();
            log("Loaded Settings.");
            if (chk_DontAskAgain.Checked)
            {
                minimizeToTray();
            }
            backgroundWorker();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            saveSettings();
            if (!flagLoginStatus)
            {
                log("User attempted to log in.");
                login();
                if (chk_DontAskAgain.Checked)
                {
                    minimizeToTray();
                }
            }
            else
            {
                log("User attempted to log out.");
                logout();
            }
        }

        public bool IsConnectedToInternet() //faster connectivity check (buggy results)
        {
            bool pingable = false;
            Ping pinger = new Ping();
            try
            {
                PingReply reply = pinger.Send("www.google.com");
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            { }
            log("Ping returned " + pingable.ToString());
            return pingable;
        }

        private int checkConnectivity() //detailed connectivity check
        {
            if (flagFastNetCheck)
                if (IsConnectedToInternet()) return 1;

            string data = openURL(checkURL.OriginalString);// + "search?q=" + randomString(5));
            if (data == "-1")
            {
                return -1;
            }
            if (data.IndexOf("Bing</title>") > 0)
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
        {
            string tempCheckURL = checkURL.OriginalString;// +"search?q=" + randomString(5);
            string page = openURL(tempCheckURL);
            string page_4Tredir = HttpUtility.UrlEncode(tempCheckURL);
            int index = page.IndexOf("magic") + 14;
            if (index < 0)
            {
                lbl_Status.Text = "Cannot login into the firewall";
                flagLoginStatus = false;
            }
            magicLogin = page.Substring(index, 16);
            log("Magic Login : " + magicLogin);
            string postData = "4Tredir=" + page_4Tredir + "&magic=" + HttpUtility.UrlEncode(magicLogin)
                    + "&username=" + HttpUtility.UrlEncode(username) + "&password=" + HttpUtility.UrlEncode(password);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fortinetURL);
            request.Method = "POST";
            try
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream(), Encoding.UTF8))
                {
                    writer.Write(postData);
                }
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                page = reader.ReadToEnd();
            }
            catch 
            {
                lbl_Status.Text = "Unable to login.";
                flagLoginStatus = false;
                return;
            }
            if (page.IndexOf("<h4>The username or password you entered is incorrect.</h4>") > 0)
            {
                lbl_Status.Text = "Incorrect Username or Password!";
                flagLoginStatus = false;
                return;
            }
            string keepAlive = fortinetURL.OriginalString + "keepalive?";
            index = page.IndexOf(keepAlive);
            keepAliveURL = new System.Uri(page.Substring(index, keepAlive.Length + 16));
            magicLogout = page.Substring(index + keepAlive.Length, 16);
            log("Magic Logout : " + magicLogout);
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
                        timer.Interval = 1000 * 10; //10 Sec
                    }
                    else
                    {
                        lbl_Status.Text = "Logged in @ IIITD!";
                        progressBar.Value = 100;
                        minuteCount++;
                        if (minuteCount > 180)
                        {
                            minuteCount = 0;
                            openURL(keepAliveURL.OriginalString);
                        }
                        timer.Interval = 1000 * 10; //10 Sec
                    }
                    break;
                case -1:
                    flagLoginStatus = false;
                    lbl_Status.Text = "Check Connecitivity to Internet";
                    progressBar.Value = 33;
                    timer.Interval = 1000 * 10; //10 Sec
                    break;
                case -2:
                    flagLoginStatus = false;
                    lbl_Status.Text = "Unknown Login Detected.";
                    progressBar.Value = 33;
                    timer.Interval = 1000 * 10; //10 Sec
                    break;
                case 0:
                    if (!flagLoginStatus)
                    {
                        progressBar.Value = 33;
                        lbl_Status.Text = "Login to proceed.";
                        timer.Interval = 1000 * 10; //10 Sec
                    }
                    else
                    {
                        progressBar.Value = 33;
                        lbl_Status.Text = "Performing Automatic Login.";
                        performLogin(txt_Username.Text, txt_Password.Text);
                        timer.Interval = 1000 * 10; //10 Sec
                    }
                    break;
            }
            return connectionStatus;
        }

        private void onTick(object sender, EventArgs e)
        {
            lbl_Status.Text = "Checking things again.";
            log("Timer Tick (Background) = " + backgroundWorker().ToString());
        }

        private void chk_DontAskAgain_CheckedChanged(object sender, EventArgs e)
        {
            saveSettings();
        }

        private bool IgnoreCertificateErrorHandler(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
          System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private string openURL(string urlAddress)
        {
            log("Open URL " + urlAddress);
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
                response.Close();
            }
            catch (Exception e)
            {
                log(e.Message);
                return "-1";
            }
            return webPage;
        }

        [DllImport("dnsapi.dll", EntryPoint = "DnsFlushResolverCache")]
        private static extern UInt32 DnsFlushResolverCache();
        public static void flushDNSCache()
        {
            UInt32 result = DnsFlushResolverCache();
        }

        private void btn_Log_Click(object sender, EventArgs e)
        {
            LogWindow logWindow = new LogWindow();
            logWindow.Show();
        }

        private void btn_Dev_Click(object sender, EventArgs e)
        {
            DevOps devOps = new DevOps();
            devOps.Show();
        }

        private void log(string text)
        {
            using (StreamWriter writer = new StreamWriter("debug.txt", true))
            {
                writer.WriteLine(text + "\n");
            }
        }

        private string randomString(int size)
        {
            Random _rng = new Random();
            const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] buffer = new char[size];
            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }
    }
}
