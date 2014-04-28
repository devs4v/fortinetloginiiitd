namespace FortinetAutoLoginIIITD
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.txt_Username = new System.Windows.Forms.TextBox();
            this.lbl_Username = new System.Windows.Forms.Label();
            this.lbl_Password = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.btn_Tray = new System.Windows.Forms.Button();
            this.chk_DontAskAgain = new System.Windows.Forms.CheckBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.notification = new System.Windows.Forms.NotifyIcon(this.components);
            this.txt_Password = new System.Windows.Forms.MaskedTextBox();
            this.btn_About = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_Username
            // 
            this.txt_Username.Location = new System.Drawing.Point(78, 15);
            this.txt_Username.Name = "txt_Username";
            this.txt_Username.Size = new System.Drawing.Size(151, 20);
            this.txt_Username.TabIndex = 0;
            // 
            // lbl_Username
            // 
            this.lbl_Username.AutoSize = true;
            this.lbl_Username.Location = new System.Drawing.Point(12, 18);
            this.lbl_Username.Name = "lbl_Username";
            this.lbl_Username.Size = new System.Drawing.Size(60, 13);
            this.lbl_Username.TabIndex = 2;
            this.lbl_Username.Text = "User Name";
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.Location = new System.Drawing.Point(12, 46);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(53, 13);
            this.lbl_Password.TabIndex = 3;
            this.lbl_Password.Text = "Password";
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(248, 13);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "Login";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_Tray
            // 
            this.btn_Tray.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Tray.Location = new System.Drawing.Point(248, 41);
            this.btn_Tray.Name = "btn_Tray";
            this.btn_Tray.Size = new System.Drawing.Size(75, 23);
            this.btn_Tray.TabIndex = 5;
            this.btn_Tray.Text = "Hide to Tray";
            this.btn_Tray.UseVisualStyleBackColor = true;
            this.btn_Tray.Click += new System.EventHandler(this.btn_Tray_Click);
            // 
            // chk_DontAskAgain
            // 
            this.chk_DontAskAgain.AutoSize = true;
            this.chk_DontAskAgain.Location = new System.Drawing.Point(15, 74);
            this.chk_DontAskAgain.Name = "chk_DontAskAgain";
            this.chk_DontAskAgain.Size = new System.Drawing.Size(218, 17);
            this.chk_DontAskAgain.TabIndex = 6;
            this.chk_DontAskAgain.Text = "Automatically login and hide this window.";
            this.chk_DontAskAgain.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(-1, 99);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(335, 13);
            this.progressBar.TabIndex = 7;
            // 
            // lbl_Status
            // 
            this.lbl_Status.AutoSize = true;
            this.lbl_Status.Location = new System.Drawing.Point(2, 115);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(137, 13);
            this.lbl_Status.TabIndex = 8;
            this.lbl_Status.Text = "Status : Starting Application";
            // 
            // notification
            // 
            this.notification.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notification.BalloonTipText = "Auto Login is hiding. Click icon to restore.";
            this.notification.BalloonTipTitle = "Auto Login @ IIITD - Fortinet";
            this.notification.Icon = ((System.Drawing.Icon)(resources.GetObject("notification.Icon")));
            this.notification.Text = "Auto Login is hiding.";
            this.notification.Visible = true;
            this.notification.Click += new System.EventHandler(this.restoreWindow);
            this.notification.DoubleClick += new System.EventHandler(this.restoreWindow);
            // 
            // txt_Password
            // 
            this.txt_Password.Location = new System.Drawing.Point(78, 43);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(151, 20);
            this.txt_Password.TabIndex = 9;
            this.txt_Password.UseSystemPasswordChar = true;
            // 
            // btn_About
            // 
            this.btn_About.Location = new System.Drawing.Point(248, 70);
            this.btn_About.Name = "btn_About";
            this.btn_About.Size = new System.Drawing.Size(75, 23);
            this.btn_About.TabIndex = 10;
            this.btn_About.Text = "About";
            this.btn_About.UseVisualStyleBackColor = true;
            this.btn_About.Click += new System.EventHandler(this.btn_About_Click);
            // 
            // MainWindow
            // 
            this.AcceptButton = this.btn_Login;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(333, 134);
            this.Controls.Add(this.btn_About);
            this.Controls.Add(this.txt_Password);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.chk_DontAskAgain);
            this.Controls.Add(this.btn_Tray);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.lbl_Password);
            this.Controls.Add(this.lbl_Username);
            this.Controls.Add(this.txt_Username);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Auto Login @ IIITD - Fortinet";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Username;
        private System.Windows.Forms.Label lbl_Username;
        private System.Windows.Forms.Label lbl_Password;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_Tray;
        private System.Windows.Forms.CheckBox chk_DontAskAgain;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.NotifyIcon notification;
        private System.Windows.Forms.MaskedTextBox txt_Password;
        private System.Windows.Forms.Button btn_About;
    }
}

