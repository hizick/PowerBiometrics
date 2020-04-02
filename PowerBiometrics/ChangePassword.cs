using Newtonsoft.Json;
using PowerBiometrics.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace PowerBiometrics
{
    public partial class ChangePassword : Form
    {
        string CompanyID = Utility.CompanyID;
        string DivisionID = Utility.DivisionID;
        string DepartmentID = Utility.DepartmentID;
        string Username = Utility.Username;
        string Password = Utility.Password;
        static HttpClient client = new HttpClient();
        //string username = "";
        //string password = "";
        string oldPassword = "";
        string newPassword = "";

        string vcmbxCompany;
        string vtxtUsername;
        string vtxtOldPwd;
        string vtxtNewPwd;
        string vtxtCnfmPwd;
        DateTime changeDate = DateTime.Now;

        Login LoginPage = new Login();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void ChangePassword_Load(object sender, EventArgs e)
        {
            loadCompany();
        }
        private void btnChangePwd_Click(object sender, EventArgs e)
        {
            bool status = ValidateUser();
            string convertedPwrd = "";
            string oldconvertedPwrd = "";

            if (status)
            {
                vcmbxCompany = cmbCompanyChpwrd.Text.ToString();
                vtxtUsername = txtUsernameCh.Text.ToString();
                vtxtOldPwd = txtOldPwd.Text.ToString();
                vtxtNewPwd = txtNewPwd.Text.ToString();
                vtxtCnfmPwd = txtCnfmPwd.Text.ToString();
                convertedPwrd = DataAccess.doConvertPwd(vtxtCnfmPwd);
                oldconvertedPwrd = DataAccess.doConvertPwd(vtxtOldPwd);

                try
                {
                    //DataAccess.UpdateData("PayrollEmployees", CompanyID, DivisionID, DepartmentID, "EmployeeID", username, "EmployeePassword", convertedPwrd, "EmployeePasswordDate", changeDate, "EmployeePasswordOld", dconvertedPwrd);
                    PayrollEmployees payrollEmployeesObj = new PayrollEmployees()
                    {
                        CompanyId = vcmbxCompany,
                        EmployeeId = vtxtUsername,
                        EmployeePassword = convertedPwrd,
                        EmployeePasswordOld = oldconvertedPwrd,
                        EmployeePasswordDate = changeDate,
                    };

                    var emp = JsonConvert.SerializeObject(payrollEmployeesObj);

                    var response = client.PutAsync("http://localhost:3295/api/ChangePassword",
                                            new StringContent(emp, Encoding.UTF8, "application/json"));

                    response.Wait();
                    var result = response.Result;
                }
                catch (Exception ex)
                {

                }
                //DialogResult result = MessageBox.Show("Password Successfully changed", "Password Change", MessageBoxButtons.OK);
                //if (result == DialogResult.OK)
                //{
                //    this.Hide();
                //    LoginPage.ShowDialog();
                //}
            }
        }

        //loads the company dropdown
        public void loadCompany()
        {
            try
            {

                var response = client.PostAsync("http://localhost:3295/api/Company", null);

                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    Uri employeeUrl = result.Headers.Location;
                    var statusMessage = result.Content.ReadAsStringAsync().Result;
                    List<Companies> companyList = JsonConvert.DeserializeObject<List<Companies>>(statusMessage);
                    if (companyList.Count > 1)
                    {
                        cmbCompanyChpwrd.DataSource = companyList;
                        cmbCompanyChpwrd.DisplayMember = "CompanyID";
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        //validates user input
        public bool ValidateUser()
        {
            bool status = false;
            bool validUser;
            bool comparePwrd;

            vcmbxCompany = cmbCompanyChpwrd.Text.ToString();
            vtxtUsername = txtUsernameCh.Text.ToString();
            vtxtOldPwd = txtOldPwd.Text.ToString();
            vtxtNewPwd = txtNewPwd.Text.ToString();
            vtxtCnfmPwd = txtCnfmPwd.Text.ToString();

            bool bcmbxCompany = string.IsNullOrEmpty(cmbCompanyChpwrd.Text);
            bool btxbUsername = string.IsNullOrEmpty(vtxtUsername);
            bool btxbOldPwd = string.IsNullOrEmpty(vtxtOldPwd);
            bool btxbNewPwd = string.IsNullOrEmpty(vtxtNewPwd);
            bool btxbCnfmPwd = string.IsNullOrEmpty(vtxtCnfmPwd);
            validUser = ValidateUserLogin(vcmbxCompany, vtxtUsername, vtxtOldPwd);
            //validPassword = ValidatePassword(vcmbxCompany, vtxtUsername, vtxtOldPwd);
            comparePwrd = comparePassword(vtxtCnfmPwd, vtxtNewPwd);

            if (bcmbxCompany == true)
            {
                MessageBox.Show("Please select Company");
                status = false;
            }
            else if ((btxbUsername || btxbOldPwd || btxbNewPwd || btxbCnfmPwd) == true)
            {
                MessageBox.Show("Please fill required fields");
                status = false;
            }
            else if (vtxtNewPwd.Length < 6)
            {
                MessageBox.Show("Password cannot be less than 6 characters");
                status = false;
            }
            else if (validUser == false)
            {
                MessageBox.Show("invalid user or password");
                status = false;
            }
            //else if (validPassword == false)
            //{
            //    MessageBox.Show("The password does not match the username entered");
            //    status = false;
            //}
            else if (comparePwrd == false)
            {
                MessageBox.Show("The passwords entered do not match");
                status = false;
            }
            else
            {
                //username = txtUsernameCh.Text.ToString();
                //password = txtOldPwd.Text.ToString();
                //oldPassword = txtOldPwd.Text.ToString();
                //newPassword = txtNewPwd.Text.ToString();
                status = true;
            }
            return status;

        }

        //checks if the user exists on the db
        public bool ValidateUserLogin(string vCompany, string vUsername, string vOldPwd)
        {
            bool vValidUser = false;
            string vconvertedPwrd = DataAccess.doConvertPwd(vOldPwd);

            PayrollEmployees payrollEmployeesObj = new PayrollEmployees()
            {
                CompanyId = vCompany,
                EmployeeId = vUsername,
                EmployeePassword = vconvertedPwrd
            };

            var emp = JsonConvert.SerializeObject(payrollEmployeesObj);

            var response = client.PostAsync("http://localhost:3295/api/ChangePassword",
                                    new StringContent(emp, Encoding.UTF8, "application/json"));

            response.Wait();
            var result = response.Result;
            if (result.IsSuccessStatusCode)
            {
                Uri employeeUrl = result.Headers.Location;
                var statusMessage = result.Content.ReadAsStringAsync().Result;
                List<PayrollEmployees> employeeList = JsonConvert.DeserializeObject<List<PayrollEmployees>>(statusMessage);
                if (employeeList.Count > 0)
                {
                    vValidUser = true; ;
                }  
            }
            return vValidUser;
        }

        //checks if the password exists for the user
        //public bool ValidatePassword(string vCompany, string vUsername, string vOldPwd)
        //{
        //    bool vValid = false;
        //    DataTable dtr = new DataTable();
        //    string vconvertedPwrd = "";
        //        vconvertedPwrd = DataAccess.doConvertPwd(vOldPwd);
                
        //    string query = "Select EmployeeID From PayrollEmployees " + "Where CompanyID ='" + vCompany + 
        //                    "' AND EmployeeID ='" + vUsername + "' AND EmployeePassword ='" + vconvertedPwrd + "'";
        //    dtr = DataAccess.ReturnDataTable(query);
        //    if (dtr.Rows.Count > 0)
        //    {
        //        vValid = true;
        //    }
        //    return vValid;
        //}

        public bool comparePassword(string vCfmPwd, string vNewPwd)
        {
            bool comparePwd = false;
            string vconvertedPwrd = "";
            string bconvertedPwrd = "";
            vconvertedPwrd = DataAccess.doConvertPwd(vCfmPwd);
            bconvertedPwrd = DataAccess.doConvertPwd(vNewPwd);
            if (vconvertedPwrd == bconvertedPwrd)
            {
                comparePwd = true;
            }
            return comparePwd;
        }

        private void btnBackLogin_Click(object sender, EventArgs e)
        {
            this.Hide();

            LoginPage.ShowDialog();
        }

    }
}
