using PowerBiometrics.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBiometrics.Objects
{
    public class AttendanceObject
    {
        public List<Attendance> AttendanceHeader { get; set; }
        public string apiToken { get; set; }
    }
}
