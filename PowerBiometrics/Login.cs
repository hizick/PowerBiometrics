using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PowerBiometrics.DomainModels;
using PowerBiometrics.Objects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PowerBiometrics
{
    public partial class Login : Form
    {
        public string EmployeeID;
        public string EmployeePassword;
        string Token = Utility.Token;
        string baseUrl = Utility.APIBaseUrl;
        static HttpClient client = new HttpClient();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        public async Task<bool> LoginValidation()
        {
            bool status = false;
            string Tusername = txtUsername.Text;
            string Tpassword = txtPassword.Text;
            bool fieldsNotEmpty = await isFieldEmpty(Tusername, Tpassword, Token, baseUrl);
            if (fieldsNotEmpty)
            {
                Utility.EmployeeID = Tusername;
                Utility.EmployeePassword = Tpassword;
                EmployeeID = Utility.EmployeeID;
                EmployeePassword = Utility.EmployeePassword;
                try
                {
                    Employees payrollEmployeesObj = new Employees()
                    {
                        EmployeeId = EmployeeID,
                        EmployeePassword = EmployeePassword,
                        apiToken = Token
                    };

                    var emp = JsonConvert.SerializeObject(payrollEmployeesObj);
                    string url = baseUrl + "api/company";
                    var response = client.PostAsync(url, new StringContent(emp, Encoding.UTF8, "application/json"));
                    response.Wait();
                    var result = response.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        Uri employeeUrl = result.Headers.Location;
                        var apiResult = result.Content.ReadAsStringAsync().Result;
                        string statusMessage = JsonConvert.DeserializeObject<string>(apiResult);
                        if (statusMessage == "yes")
                        {
                            status = true;
                        }
                        else
                        {
                            MessageBox.Show(statusMessage);
                            return status;
                        }

                    }
                    return status;
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
                        MessageBox.Show(ex.Message);
                        Application.Exit();
                    }
                }
            }

            return await Task.FromResult<bool>(status);

        }
        public async Task<bool> isFieldEmpty(string xusername, string xpassword, string token, string _url)
        {
            bool status = false;

            bool btxbUsername = string.IsNullOrEmpty(xusername);
            bool btxbPassword = string.IsNullOrEmpty(xpassword);

            if ((btxbUsername || btxbPassword) == true)
            {
                MessageBox.Show("Username or Password cannot be empty");
                status = false;
            }
            else if (string.IsNullOrEmpty(token))
            {
                MessageBox.Show("Token cannot be empty");
                status = false;
            }
            else if (string.IsNullOrEmpty(_url))
            {
                MessageBox.Show("Please input api url");
                status = false;
            }
            else
            {
                status = true;
            }
            return await Task.FromResult<bool>(status);
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            //bool status = await LoginValidation();
            //if (status)
            //{
            Utility.isSyncStillOn = false;
                new MainForm().Show();
                this.Hide();
            //}
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }
    }
}
