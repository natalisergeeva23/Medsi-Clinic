using System;
using System.Collections.Generic;

namespace ApiMedsi.Models
{
    public partial class Synopsis
    {
        public int IdSynopsis { get; set; }
        public string NumberOfDays { get; set; } = null!;
        public int PatientCardId { get; set; }

    }
}
