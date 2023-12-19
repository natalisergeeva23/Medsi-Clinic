using System;
using System.Collections.Generic;

namespace ApiMedsi.Models
{
    public partial class Role
    {

        public int IdRole { get; set; }
        public string NameRole { get; set; } = null!;
        public bool IsDeleted { get; set; }

    }
}
