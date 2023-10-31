using AccountsReceivable.BL.Models.Account;

namespace AccountsReceivable.BL.Models.Application;

public class Comment
{
    public uint Id { get; set; }

    public string DocumentId { get; set; } = null!;
    public virtual Document Document { get; set; } = null!;

    public string UserEmailAddress { get; set; } = null!;
    public User User { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime Timestamp { get; set; }
}