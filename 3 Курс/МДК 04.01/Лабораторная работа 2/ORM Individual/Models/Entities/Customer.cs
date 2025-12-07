using ORM_Individual.Interfaces;
using System;
using System.Collections.Generic;

namespace ORM_Individual.Models.Entities;

public partial class Customer : IEntity
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
