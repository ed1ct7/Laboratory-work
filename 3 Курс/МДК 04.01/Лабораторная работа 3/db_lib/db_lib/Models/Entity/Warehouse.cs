using System;
using System.Collections.Generic;
namespace db_lib.Models.Entity
{
    public partial class Warehouse
    {
        public string ReceiptDate { get; set; }

        public string OrderDate { get; set; }

        public string DispatchDate { get; set; }

        public int GoodsId { get; set; }

        public int CustomerId { get; set; }

        public int SuppliersId { get; set; }

        public string DeliveryMethod { get; set; } = null;

        public int Volume { get; set; }

        public decimal Price { get; set; }

        public int EmployeeId { get; set; }

        public virtual Customer Customer { get; set; } = null;

        public virtual Employee Employee { get; set; } = null;

        public virtual Good Goods { get; set; } = null;

        public virtual Supplier Suppliers { get; set; } = null;
    }
}