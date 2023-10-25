namespace AccountsReceivable.BL.Models.Enum;

public enum StatusId : ushort
{
    Pending = 0,

    Approved = 1,
    Declined = 2,

    Overridden = 3,
    Superseded = 4,
    
    Missing = 5
}

public class Status
{
    /* Properties */

    public StatusId Id { get; set; }
    public string Name { get; set; } = null!;
}

public class StatusHelper
{
    private static readonly Dictionary<StatusId, Status> _dictionary = new()
    {
        {
            StatusId.Pending,
            new Status
            {
                Id = StatusId.Pending,
                Name = "Pending"
            }
        },
        {
            StatusId.Approved,
            new Status
            {
                Id = StatusId.Approved,
                Name = "Approved"
            }
        },
        {
            StatusId.Declined,
            new Status
            {
                Id = StatusId.Declined,
                Name = "Declined"
            }
        },
        {
            StatusId.Overridden,
            new Status
            {
                Id = StatusId.Overridden,
                Name = "Overridden"
            }
        },
        {
            StatusId.Superseded,
            new Status
            {
                Id = StatusId.Superseded,
                Name = "Superseded"
            }
        },
        {
            StatusId.Missing,
            new Status()
            {
                Id = StatusId.Missing,
                Name = "Missing"
            }
        }
    };

    public static Status GetInfo(StatusId id)
    {
        return (_dictionary.TryGetValue(id, out var value) ? value : default) ?? throw new InvalidOperationException();
    }

    public static ICollection<Status> GetAll()
    {
        return _dictionary.Values;
    }
}