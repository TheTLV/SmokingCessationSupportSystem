using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class QuitPlan
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Reason { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime TargetDate { get; set; }

    public string Stages { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
