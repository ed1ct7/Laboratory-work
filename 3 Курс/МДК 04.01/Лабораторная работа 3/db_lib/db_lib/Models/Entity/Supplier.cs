using System;
using System.Collections.Generic;
namespace db_lib.Models.Entity
{
    public partial class Supplier
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null;

        public string Address { get; set; } = null;

        public string Phone { get; set; } = null;

        public int? GoodsId1 { get; set; }

        public int? GoodsId2 { get; set; }

        public int? GoodsId3 { get; set; }

        public virtual Good GoodsId1Navigation { get; set; }

        public virtual Good GoodsId2Navigation { get; set; }

        public virtual Good GoodsId3Navigation { get; set; }
    }
}