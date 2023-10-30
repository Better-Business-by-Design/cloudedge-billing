namespace AccountsReceivable.BL.Models.Enum;

public enum RoleId : byte
{
    Missing = 0,
    
    Read = 1,
    ReadWrite = 2,
    Administrator = 3,
}

public class Role
{
    /* Properties */

    public RoleId Id { get; set; }
    public string Name { get; set; } = null!;
}

public class RoleHelper
{
    private static readonly Dictionary<RoleId, Role> Dictionary = new()
    {
        {
            RoleId.Missing,
            new Role
            {
                Id = RoleId.Missing,
                Name = "Missing"
            }
        },
        {
            RoleId.Read,
            new Role
            {
                Id = RoleId.Read,
                Name = "Read"
            }
        },
        {
            RoleId.ReadWrite,
            new Role
            {
                Id = RoleId.ReadWrite,
                Name = "Read/Write"
            }
        },
        {
            RoleId.Administrator,
            new Role
            {
                Id = RoleId.Administrator,
                Name = "Administrator"
            }
        }
    };

    public static Role GetInfo(RoleId id)
    {
        return (Dictionary.TryGetValue(id, out var value) ? value : default) ?? throw new InvalidOperationException();
    }

    public static ICollection<Role> GetAll()
    {
        return Dictionary.Values;
    }
}