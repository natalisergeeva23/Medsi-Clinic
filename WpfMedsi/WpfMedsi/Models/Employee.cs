using System;
using System.Collections.Generic;

namespace WpfMedsi.Models
{
    public partial class Employee
    {

        public int IdEmployee { get; set; }
        public string FirstNameEmp { get; set; } 
        public string SecondNameEmp { get; set; } 
        public string MiddleNameEmp { get; set; }
        public string LoginEmp { get; set; } 
        public string PasswordEmp { get; set; } 
        public string LoginSaltEmp { get; set; }
        public string PasswordSaltEmp { get; set; }
        public int PositionId { get; set; }
        public int RoleId { get; set; }
        public string Salt { get; set; }

    }
}
