using System;
using System.Collections.Generic;

namespace ApiMedsi.Models
{
    public partial class Patient
    {
        public int IdPatient { get; set; }
        public string FirstNamePatient { get; set; } = null!;
        public string SecondNamePatient { get; set; } = null!;
        public string? MiddleNamePatient { get; set; }
        public string PassportSeries { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;
        public string Snils { get; set; } = null!;
        public string InsurancePolicy { get; set; } = null!;
        public string LoginPatient { get; set; } = null!;
        public string PasswordPatient { get; set; } = null!;

    }
}
