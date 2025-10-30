using System;
using System.Collections.Generic;

namespace ORM_databasefirst.Models;

public partial class Auth
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Age { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
