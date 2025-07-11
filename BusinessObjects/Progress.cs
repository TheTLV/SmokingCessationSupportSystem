using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Progress
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public DateTime Date { get; set; }

    public int CigarettesSmoked { get; set; }

    public string HealthStatus { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
