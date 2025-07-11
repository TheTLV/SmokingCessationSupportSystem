using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class ChatMessage
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int ReceiverId { get; set; }

    public string Message { get; set; } = null!;

    public DateTime SentAt { get; set; }

    public bool IsRead { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
