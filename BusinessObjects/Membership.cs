using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Membership
{
    public int Id { get; set; }

    public string PackageName { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
