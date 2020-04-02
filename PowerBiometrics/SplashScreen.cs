using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PowerBiometrics;

namespace PowerBiometrics
{
    public partial class SplashScreen : Form
    {
        Login login = new Login();
        int timeLeft;
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            //pgbSplashScreen.PerformStep();
            timeLeft = 14;
            timerSplash.Start();
            
            lblCopyright.Text = "Copyright Powersoft " + DateTime.Now.Year.ToString();
            //this.timerSplash.Start();

            //login.ShowDialog();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                pgbSplashScreen.PerformStep();
                timeLeft = timeLeft - 1;
            }
            else
            {
                timerSplash.Stop();

                while (pgbSplashScreen.Value < 100)
                {
                    pgbSplashScreen.PerformStep();
                }

                //new MDIPowerSync().Show();
                new Login().Show();
                this.Hide();
            }
        }
    }
}
