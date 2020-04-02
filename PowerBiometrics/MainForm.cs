using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using PowerBiometrics.DomainModels;
using Newtonsoft.Json;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using PowerBiometrics.Objects;
using System.Globalization;

namespace PowerBiometrics
{
    public partial class MainForm : Form
    {
        Login LoginPage = new Login();
        string EmployeeID = Utility.EmployeeID;
        string baseUrl = Utility.APIBaseUrl;
        string EmployeePassword = Utility.EmployeePassword;
        string Token = Utility.Token;
        static HttpClient client = new HttpClient();
        static string dateFormat = "yyyy/MM/dd hh:mm:ss";
        DateTime dateNow = dateConverter((DateTime.Now).ToString(dateFormat));
        bool isSyncStillOn = Utility.isSyncStillOn;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lblCopyright.Text = "Copyright Powersoft " + DateTime.Now.Year.ToString();
            lblUser.Text = "Welcome, " + EmployeeID.ToString();

        }

        private async void btnSynchronize_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.MarqueeAnimationSpeed = 20;
            btnSynchronize.Enabled = false;
            isSyncStillOn = true;

            await (Task.Run(async () =>
            {
                await PopulateHeaderTable();
            }));
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.MarqueeAnimationSpeed = 0;
            btnSynchronize.Enabled = true;
            isSyncStillOn = false;
        }

        private async Task<List<AttendanceDetail>> getAttendanceDetail(DateTime AttendanceDate)
        {
            DataTable getData = new DataTable();
            List<AttendanceDetail> attendanceDetail = new List<AttendanceDetail>();
            string attendanceDate = DataAccess.DateStringConverter(AttendanceDate);

            try
            {
                getData = DataAccess.GetAttendanceDetail("ras_AttRecord", "Synchronized", "Clock", attendanceDate, "*");

                foreach (DataRow dr in getData.Rows)
                {
                    string attDate = Convert.ToDateTime(dr["Clock"]).ToString(dateFormat);
                    AttendanceDetail attendanceDetailObj = new AttendanceDetail();

                    DateTime dtAttendanceDate = dateConverter(attDate);
                    dtAttendanceDate = dtAttendanceDate.Date;

                    attendanceDetailObj.AttendanceDate = dtAttendanceDate;
                    attendanceDetailObj.EmployeeId = dr["DIN"].ToString();
                    //DateTime timeIn = Convert.ToDateTime(dr["Clock"].ToString());
                    attendanceDetailObj.TimeIn = dateConverter(attDate);
                    attendanceDetail.Add(attendanceDetailObj);

                }
            }
            catch (Exception ex)
            {

            }

            return await Task.FromResult(attendanceDetail);
        }
        private static DateTime dateConverter(string dateInString)
        {
            DateTime dd = Convert.ToDateTime(dateInString, CultureInfo.InvariantCulture);
            return dd;
        }
        private async Task PopulateHeaderTable()
        {
            DataTable getData = new DataTable();
            string statusMessage = "";

            List<Attendance> attendanceList = new List<Attendance>();
            try
            {
                getData = DataAccess.GetAttendanceHeader("ras_AttRecord", "Clock", "Synchronized");
                if (getData != null)
                {
                    if (getData.Rows.Count > 0)
                    {
                        foreach (DataRow dr in getData.Rows)
                        {
                            Attendance attendanceObj = new Attendance();
                            string attDate = Convert.ToDateTime(dr["Clock"]).ToString(dateFormat);
                            attendanceObj.AttendanceDate = dateConverter(attDate).Date;
                            attendanceObj.EnteredBy = EmployeeID;
                            attendanceObj.EnteredDate = dateNow;
                            attendanceObj.apiToken = Token;
                            attendanceObj.attendanceDetail = await getAttendanceDetail(dateConverter(attDate));
                            attendanceList.Add(attendanceObj);
                        }

                        var att = JsonConvert.SerializeObject(attendanceList);
                        string url = baseUrl + "api/PowerBiometrics";
                        var response = client.PostAsync(url, new StringContent(att, Encoding.UTF8, "application/json"));

                        response.Wait();
                        var result = response.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            Uri attendanceUrl = result.Headers.Location;
                            statusMessage = result.Content.ReadAsStringAsync().Result;
                            showMessageResult(statusMessage);
                        } 
                    }
                    else
                    {
                        MessageBox.Show("Record already synchronised");
                        return;
                    }                  
                }
                else
                {
                    MessageBox.Show("Record does not exist.");
                    return;
                }
                
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
                if (errorMessage == "One or more errors occurred.")
                {
                    MessageBox.Show(errorMessage + " Is API running?");
                    Application.Exit();
                }
                else if (errorMessage == "Invalid URI: Invalid port specified.")
                {
                    MessageBox.Show("API url is incorrect.");
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show(errorMessage);
                    Application.Exit();
                }
            }
        }

        public void showMessageResult(string msg)
        {
            DateTime syncDate = dateNow;
            if (msg == "success")
            {
                DataAccess.UpdateSyncDetail("ras_AttRecord", "SynchBy", EmployeeID, "Synchronized", "1", "SynchDate", syncDate);
                MessageBox.Show("The sync is completed successfully!!");
                return;
            }
            else if(msg == "failed")
            {
                MessageBox.Show("Sync Failed. Please see the Admin");
                return;
            }
            else
            {
                MessageBox.Show(msg);
                return;
            }
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            //progressBar.Increment(10);
        }

        //for logout
        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (isSyncStillOn)
            {
                MessageBox.Show("You cannot log out until Synchronization is complete.");
            }
            else
            {
                EmployeeID = string.Empty;
                EmployeePassword = string.Empty;
                LoginPage.Show();
                this.Hide();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to close this page?", "Close Page", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (isSyncStillOn)
                {
                    e.Cancel = true;
                    MessageBox.Show("You cannot close this page when synchronization is in progress.");
                }
                else
                {
                    Application.ExitThread();
                    e.Cancel = false;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
