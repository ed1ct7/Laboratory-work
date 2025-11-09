using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM_Individual.Models.Entities;

namespace ORM_Individual.Models.Repositories
{
    public class OrderRepository
    {
        public int Id { get; set; }

        public string? OrderDate { get; set; }

        public string? CompletionDate { get; set; }

        public int? CustomerId { get; set; }

        public int? Component1Id { get; set; }

        public int? Component2Id { get; set; }

        public int? Component3Id { get; set; }

        public decimal? Prepayment { get; set; }

        public bool? IsPaid { get; set; }

        public bool? IsCompleted { get; set; }

        public decimal? TotalAmount { get; set; }

        public string? TotalWarranty { get; set; }

        public int? Service1Id { get; set; }

        public int? Service2Id { get; set; }

        public int? Service3Id { get; set; }

        public int? EmployeeId { get; set; }
    }
}
