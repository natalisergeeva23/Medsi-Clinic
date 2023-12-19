using System;
using System.Collections.Generic;

namespace ApiMedsi.Models
{
    public partial class Employee
    {

        public int IdEmployee { get; set; }
        public string FirstNameEmp { get; set; } = null!;
        public string SecondNameEmp { get; set; } = null!;
        public string? MiddleNameEmp { get; set; }
        public string LoginEmp { get; set; } = null!;
        public string PasswordEmp { get; set; } = null!;
        public int PositionId { get; set; }
        public int RoleId { get; set; }
        public string? Salt { get; set; }

    }
}
