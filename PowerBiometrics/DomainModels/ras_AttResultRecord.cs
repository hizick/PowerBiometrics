using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBiometrics.DomainModels
{
    public class ras_AttResultRecord
    {
        public int ID { get; set; }
        public int UID { get; set; }
        public DateTime AttDate { get; set; }
        public int AttID { get; set; }
        public string TimeString { get; set; }
        public string Remark { get; set; }
        public decimal A01 { get; set; }
        public decimal B01 { get; set; }
        public decimal B02 { get; set; }
        public decimal B03 { get; set; }
        public decimal C01 { get; set; }
        public decimal D01 { get; set; }
        public decimal D02 { get; set; }
        public decimal D03 { get; set; }
        public decimal D04 { get; set; }
        public decimal D05 { get; set; }
        public decimal D06 { get; set; }
        public decimal D07 { get; set; }
        public decimal D08 { get; set; }
        public decimal D09 { get; set; }
        public decimal D10 { get; set; }
        public decimal E01 { get; set; }   
        public decimal F01 { get; set; }  
        public decimal G01 { get; set; }   
        public decimal G02 { get; set; } 
        public decimal G03 { get; set; } 
    }
}
