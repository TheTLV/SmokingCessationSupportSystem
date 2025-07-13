namespace BusinessObjects;

public partial class Feedback
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Content { get; set; } = null!;

    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
