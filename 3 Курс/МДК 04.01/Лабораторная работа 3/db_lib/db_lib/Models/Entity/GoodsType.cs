using System;
using System.Collections.Generic;
namespace db_lib.Models.Entity
{
    public partial class GoodsType
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null;

        public string Description { get; set; }

        public string Peculiarities { get; set; }

        public virtual ICollection<Good> Goods { get; set; } = new List<Good>();
    }
}
