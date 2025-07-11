using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Achievement
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateTime AchievedDate { get; set; }

    public virtual User User { get; set; } = null!;
}
