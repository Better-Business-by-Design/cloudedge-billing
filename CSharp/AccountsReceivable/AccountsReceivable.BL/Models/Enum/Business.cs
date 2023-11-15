namespace AccountsReceivable.BL.Models.Enum;

public enum BusinessId : byte
{
    None = 0,
    CloudEdge = 1,
    MaxHub = 2
}

public class Business
{
    public BusinessId Id { get; set; }
    public string Name { get; set; } = null!;
}

public static class BusinessHelper
{
    private static readonly Dictionary<BusinessId, Business> Dictionary = new()
    {
        {
            BusinessId.None,
            new Business
            {
                Id = BusinessId.None,
                Name = "None"
            }
        },
        {
            BusinessId.CloudEdge,
            new Business
            {
                Id = BusinessId.CloudEdge,
                Name = "Cloud Edge"
            }
        },
        {
            BusinessId.MaxHub,
            new Business
            {
                Id = BusinessId.MaxHub,
                Name = "MaxHub"
            }
        }
    };

    public static Business GetInfo(BusinessId id)
    {
        return (Dictionary.TryGetValue(id, out var value) ? value : null) ?? throw new InvalidOperationException();
    }

    public static ICollection<Business> GetAll()
    {
        return Dictionary.Values;
    }
}