namespace PowerBiometrics
{
    partial class SplashScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.lblCopyright = new DevExpress.XtraEditors.LabelControl();
            this.timerSplash = new System.Windows.Forms.Timer(this.components);
            this.pgbSplashScreen = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Location = new System.Drawing.Point(178, 72);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(189, 33);
            this.labelControl5.TabIndex = 35;
            this.labelControl5.Text = "Power Biometric";
            // 
            // lblCopyright
            // 
            this.lblCopyright.Location = new System.Drawing.Point(240, 191);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(49, 13);
            this.lblCopyright.TabIndex = 37;
            this.lblCopyright.Text = "Powersoft";
            // 
            // timerSplash
            // 
            this.timerSplash.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pgbSplashScreen
            // 
            this.pgbSplashScreen.Location = new System.Drawing.Point(98, 122);
            this.pgbSplashScreen.Name = "pgbSplashScreen";
            this.pgbSplashScreen.Size = new System.Drawing.Size(367, 37);
            this.pgbSplashScreen.TabIndex = 38;
            // 
            // SplashScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(546, 216);
            this.Controls.Add(this.pgbSplashScreen);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.labelControl5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SplashScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.Load += new System.EventHandler(this.SplashScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl lblCopyright;
        private System.Windows.Forms.Timer timerSplash;
        private System.Windows.Forms.ProgressBar pgbSplashScreen;
    }
}