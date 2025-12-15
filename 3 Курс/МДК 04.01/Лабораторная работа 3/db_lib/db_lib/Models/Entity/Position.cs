using System;
using System.Collections.Generic;
namespace db_lib.Models.Entity
{
    public partial class Position
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null;

        public decimal Salary { get; set; }

        public string Responsibilities { get; set; }

        public string Requirements { get; set; }

        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
