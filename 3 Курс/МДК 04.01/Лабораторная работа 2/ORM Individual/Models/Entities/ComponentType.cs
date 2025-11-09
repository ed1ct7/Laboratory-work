using System;
using System.Collections.Generic;

namespace ORM_Individual.Models.Entities;

public partial class ComponentType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Component> Components { get; set; } = new List<Component>();
}
