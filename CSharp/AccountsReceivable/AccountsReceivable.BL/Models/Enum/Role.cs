namespace AccountsReceivable.BL.Models.Enum;

public enum RoleId : ushort
{
    Read = 0,
    ReadWrite = 1,
    Administrator = 2
}

public class Role
{
    /* Properties */

    public RoleId Id { get; set; }
    public string Name { get; set; } = null!;
}

public class RoleHelper
{
    private static readonly Dictionary<RoleId, Role> _dictionary = new()
    {
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
        return (_dictionary.TryGetValue(id, out var value) ? value : default) ?? throw new InvalidOperationException();
    }

    public static ICollection<Role> GetAll()
    {
        return _dictionary.Values;
    }
}