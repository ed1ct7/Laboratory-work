using System;
using System.Collections.Generic;

namespace db_lib.Models.Entity
{
    public partial class Customer
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null;

        public string Address { get; set; } = null;

        public string Phone { get; set; } = null;

        public int? NeedGoods1 { get; set; }

        public int? NeedGoods2 { get; set; }

        public int? NeedGoods3 { get; set; }

        public virtual Good NeedGoods1Navigation { get; set; }

        public virtual Good NeedGoods2Navigation { get; set; }

        public virtual Good NeedGoods3Navigation { get; set; }
    }
}