using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Account;

public class User
{
    public string EmailAddress;

    public string Name { get; set; }

    public RoleId RoleId { get; set; } = RoleId.Read;
    public virtual Role Role { get; set; }
}