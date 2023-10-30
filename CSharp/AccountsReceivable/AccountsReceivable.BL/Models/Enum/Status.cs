namespace AccountsReceivable.BL.Models.Enum;

public enum StatusId : byte
{
    Missing = 0,
    Pending = 1,

    Approved = 2,
    Declined = 3,

    Overridden = 4,
    Superseded = 5
}

public class Status
{
    /* Properties */

    public StatusId Id { get; set; }
    public string Name { get; set; } = null!;
}

public class StatusHelper
{
    private static readonly Dictionary<StatusId, Status> Dictionary = new()
    {
        {
            StatusId.Missing,
            new Status
            {
                Id = StatusId.Missing,
                Name = "Missing"
            }
        },
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
        }
    };

    public static Status GetInfo(StatusId id)
    {
        return (Dictionary.TryGetValue(id, out var value) ? value : default) ?? throw new InvalidOperationException();
    }

    public static ICollection<Status> GetAll()
    {
        return Dictionary.Values;
    }
}