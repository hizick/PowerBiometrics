using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBiometrics.DomainModels
{
    public class ras_AttRecord
    {
        public int ID {get; set;}
        public  int DN { get; set; }
        public  int DIN { get; set; }
        public  DateTime Clock { get; set; }
        public  int VerifyMode { get; set; }
        public  int Action { get; set; }
        public  string AttTypeId { get; set; }
        public  DateTime CollectDate { get; set; }
        public  int JobCode { get; set; }
        public  bool AntiPassback { get; set; }
        public  int LastUpdatedUID { get; set; }
        public  DateTime LastUpdatedDate { get; set; }
        public  string Remark { get; set; }
        public  bool Synchronized { get; set; }
        public  string SynchBy { get; set; }
        public  DateTime SynchDate { get; set; }

    }
}
