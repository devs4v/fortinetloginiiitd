namespace FortinetAutoLoginIIITD
{
    partial class DevOps
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DevOps));
            this.lbl_CheckURL = new System.Windows.Forms.Label();
            this.lbl_FortinetURL = new System.Windows.Forms.Label();
            this.txt_CheckURL = new System.Windows.Forms.TextBox();
            this.txt_FortinetURL = new System.Windows.Forms.TextBox();
            this.chk_FastInternetCheck = new System.Windows.Forms.CheckBox();
            this.btn_Save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_CheckURL
            // 
            this.lbl_CheckURL.AutoSize = true;
            this.lbl_CheckURL.Location = new System.Drawing.Point(9, 13);
            this.lbl_CheckURL.Name = "lbl_CheckURL";
            this.lbl_CheckURL.Size = new System.Drawing.Size(60, 13);
            this.lbl_CheckURL.TabIndex = 0;
            this.lbl_CheckURL.Text = "CheckURL";
            // 
            // lbl_FortinetURL
            // 
            this.lbl_FortinetURL.AutoSize = true;
            this.lbl_FortinetURL.Location = new System.Drawing.Point(9, 40);
            this.lbl_FortinetURL.Name = "lbl_FortinetURL";
            this.lbl_FortinetURL.Size = new System.Drawing.Size(67, 13);
            this.lbl_FortinetURL.TabIndex = 1;
            this.lbl_FortinetURL.Text = "Fortinet URL";
            // 
            // txt_CheckURL
            // 
            this.txt_CheckURL.Location = new System.Drawing.Point(82, 10);
            this.txt_CheckURL.Name = "txt_CheckURL";
            this.txt_CheckURL.Size = new System.Drawing.Size(218, 20);
            this.txt_CheckURL.TabIndex = 3;
            // 
            // txt_FortinetURL
            // 
            this.txt_FortinetURL.Location = new System.Drawing.Point(82, 37);
            this.txt_FortinetURL.Name = "txt_FortinetURL";
            this.txt_FortinetURL.Size = new System.Drawing.Size(218, 20);
            this.txt_FortinetURL.TabIndex = 4;
            // 
            // chk_FastInternetCheck
            // 
            this.chk_FastInternetCheck.AutoSize = true;
            this.chk_FastInternetCheck.Location = new System.Drawing.Point(12, 68);
            this.chk_FastInternetCheck.Name = "chk_FastInternetCheck";
            this.chk_FastInternetCheck.Size = new System.Drawing.Size(207, 17);
            this.chk_FastInternetCheck.TabIndex = 5;
            this.chk_FastInternetCheck.Text = "Fast Internet Check (Warning!! Buggy)";
            this.chk_FastInternetCheck.UseVisualStyleBackColor = true;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(225, 64);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 6;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // DevOps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(316, 99);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.chk_FastInternetCheck);
            this.Controls.Add(this.txt_FortinetURL);
            this.Controls.Add(this.txt_CheckURL);
            this.Controls.Add(this.lbl_FortinetURL);
            this.Controls.Add(this.lbl_CheckURL);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DevOps";
            this.Text = "Developer Options";
            this.Load += new System.EventHandler(this.DevOps_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_CheckURL;
        private System.Windows.Forms.Label lbl_FortinetURL;
        private System.Windows.Forms.TextBox txt_CheckURL;
        private System.Windows.Forms.TextBox txt_FortinetURL;
        private System.Windows.Forms.CheckBox chk_FastInternetCheck;
        private System.Windows.Forms.Button btn_Save;
    }
}