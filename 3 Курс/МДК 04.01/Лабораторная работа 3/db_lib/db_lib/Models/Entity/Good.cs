using System;
using System.Collections.Generic;
namespace db_lib.Models.Entity
{
    public partial class Good
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public string Manufacturer { get; set; } = null;

        public string FullName { get; set; } = null;

        public string StorageConditions { get; set; }

        public string Package { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public virtual ICollection<Customer> CustomerNeedGoods1Navigations { get; set; } = new List<Customer>();

        public virtual ICollection<Customer> CustomerNeedGoods2Navigations { get; set; } = new List<Customer>();

        public virtual ICollection<Customer> CustomerNeedGoods3Navigations { get; set; } = new List<Customer>();

        public virtual ICollection<Supplier> SupplierGoodsId1Navigations { get; set; } = new List<Supplier>();

        public virtual ICollection<Supplier> SupplierGoodsId2Navigations { get; set; } = new List<Supplier>();

        public virtual ICollection<Supplier> SupplierGoodsId3Navigations { get; set; } = new List<Supplier>();

        public virtual GoodsType Type { get; set; } = null;
    }
}
