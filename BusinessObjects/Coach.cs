using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Coach
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Specialization { get; set; } = null!;

    public int ExperienceYears { get; set; }

    public virtual User User { get; set; } = null!;
}
