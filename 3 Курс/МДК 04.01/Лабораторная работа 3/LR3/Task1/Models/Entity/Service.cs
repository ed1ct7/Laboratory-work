using System;
using System.Collections.Generic;

namespace Task1.Models.Entity
{
    public partial class Service
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}