namespace AccountsReceivable.BL.Models.Account;

public class Audit
{
    public DateTime Timestamp;

    public string UserId;

    public string Action { get; set; }
    public virtual User User { get; set; }

    public string? Comment { get; set; }
}