using System;
using System.Collections.Generic;

namespace ORM_Individual.Models.Entities;

public partial class Service
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal? Price { get; set; }

    public virtual ICollection<Order> OrderService1s { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderService2s { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderService3s { get; set; } = new List<Order>();
}
