using ORM_Individual.Interfaces;
using System;
using System.Collections.Generic;

namespace ORM_Individual.Models.Entities;

public partial class Order : IEntity
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

    public virtual Component? Component1 { get; set; }

    public virtual Component? Component2 { get; set; }

    public virtual Component? Component3 { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Service? Service1 { get; set; }

    public virtual Service? Service2 { get; set; }

    public virtual Service? Service3 { get; set; }
}
