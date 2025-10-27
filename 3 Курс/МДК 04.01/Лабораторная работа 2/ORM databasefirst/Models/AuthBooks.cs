using System;
using System.Collections.Generic;

namespace Models;

public partial class AuthBooks
{
    public int Id { get; set; }

    public int AuthId { get; set; }

    public int BookId { get; set; }

    public virtual Auths Auth { get; set; } = null!;

    public virtual Books Book { get; set; } = null!;
}