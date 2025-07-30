namespace BusinessObjects;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<ChatMessage> ChatMessageReceivers { get; set; } = new List<ChatMessage>();

    public virtual ICollection<ChatMessage> ChatMessageSenders { get; set; } = new List<ChatMessage>();

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CommunityPost> CommunityPosts { get; set; } = new List<CommunityPost>();

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual ICollection<QuitPlan> QuitPlans { get; set; } = new List<QuitPlan>();

    public virtual ICollection<SmokingStatus> SmokingStatuses { get; set; } = new List<SmokingStatus>();
}
