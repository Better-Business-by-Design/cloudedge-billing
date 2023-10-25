namespace AccountsReceivable.BL.Models.Enum;

public enum ValidationId : ushort
{
    Pending = 0,

    Low = 1,
    Valid = 2,
    High = 3
}

public class Validation
{
    /* Properties */

    public ValidationId Id { get; set; }
    public string Name { get; set; } = null!;
}

public class ValidationHelper
{
    private static readonly Dictionary<ValidationId, Validation> _dictionary = new()
    {
        {
            ValidationId.Pending,
            new Validation
            {
                Id = ValidationId.Pending,
                Name = "Pending"
            }
        },
        {
            ValidationId.Low,
            new Validation
            {
                Id = ValidationId.Low,
                Name = "Low"
            }
        },
        {
            ValidationId.Valid,
            new Validation
            {
                Id = ValidationId.Valid,
                Name = "Valid"
            }
        },
        {
            ValidationId.High,
            new Validation
            {
                Id = ValidationId.High,
                Name = "High"
            }
        }
    };

    public static Validation GetInfo(ValidationId id)
    {
        return (_dictionary.TryGetValue(id, out var value) ? value : default) ?? throw new InvalidOperationException();
    }

    public static ICollection<Validation> GetAll()
    {
        return _dictionary.Values;
    }
}