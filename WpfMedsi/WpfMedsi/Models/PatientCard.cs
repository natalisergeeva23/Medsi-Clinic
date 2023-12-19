using System;
using System.Collections.Generic;

namespace WpfMedsi.Models
{
    public partial class PatientCard
    {
        public int IdPatientCard { get; set; }
        public string CardNumberPatient { get; set; } 
        public DateTime DateOfCreation { get; set; }
        public string NameDiseases { get; set; } 
        public int PatientId { get; set; }
        public int EmployeeId { get; set; }

    }
}
