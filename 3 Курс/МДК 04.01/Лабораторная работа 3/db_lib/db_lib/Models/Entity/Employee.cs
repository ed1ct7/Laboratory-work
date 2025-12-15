using System;
using System.Collections.Generic;

namespace db_lib.Models.Entity
{
    public partial class Employee
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null;

        public int Age { get; set; }

        public int Gender { get; set; }

        public string Address { get; set; } = null;

        public string Phone { get; set; } = null;

        public string PassportData { get; set; } = null;

        public int PositionId { get; set; }

        public virtual Position Position { get; set; } = null;
    }
}