using ORM_Individual.Interfaces;
using System;
using System.Collections.Generic;

namespace ORM_Individual.Models.Entities;

public partial class Position : IEntity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Salary { get; set; }

    public string? Duties { get; set; }

    public string? Requirements { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
