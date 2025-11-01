using System;
using System.Collections.Generic;

namespace ORM_databasefirst.Models;

public partial class Auth_
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Age { get; set; }
}
