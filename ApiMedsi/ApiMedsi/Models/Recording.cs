using System;
using System.Collections.Generic;

namespace ApiMedsi.Models
{
    public partial class Recording
    {
        public int IdRecording { get; set; }
        public DateTime RecordDate { get; set; }
        public TimeSpan RecordTime { get; set; }
        public string Direction { get; set; } = null!;
        public int StatusId { get; set; }
        public int PatientId { get; set; }
        public int EmployeeId { get; set; }

    }
}
