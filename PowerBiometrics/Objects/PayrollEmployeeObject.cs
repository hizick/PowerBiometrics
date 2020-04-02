using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBiometrics.DomainModels
{
    public class PayrollEmployeeObject
    {
        public Employees payrollEmp { get; set; }
        public string apiToken { get; set; }
    }
}
