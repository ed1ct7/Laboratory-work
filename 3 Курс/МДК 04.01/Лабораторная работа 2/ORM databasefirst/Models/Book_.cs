using System;
using System.Collections.Generic;

namespace ORM_databasefirst.Models;

public partial class Book_
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int CountPage { get; set; }

    public double? Price { get; set; }
}
