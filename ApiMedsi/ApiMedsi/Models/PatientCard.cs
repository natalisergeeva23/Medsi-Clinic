using System;
using System.Collections.Generic;

namespace ApiMedsi.Models
{
    public partial class PatientCard
    {
        public int IdPatientCard { get; set; }
        public string CardNumberPatient { get; set; } = null!;
        public DateTime DateOfCreation { get; set; }
        public string NameDiseases { get; set; } = null!;
        public int PatientId { get; set; }
        public int EmployeeId { get; set; }

    }
}
