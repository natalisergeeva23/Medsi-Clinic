using System;
using System.Collections.Generic;

namespace WpfMedsi.Models
{
    public partial class Patient
    {
        public int IdPatient { get; set; }
        public string FirstNamePatient { get; set; } 
        public string SecondNamePatient { get; set; } 
        public string MiddleNamePatient { get; set; }
        public string PassportSeries { get; set; } 
        public string PassportNumber { get; set; } 
        public string Snils { get; set; } 
        public string InsurancePolicy { get; set; } 
        public string LoginPatient { get; set; } 
        public string PasswordPatient { get; set; } 

    }
}
