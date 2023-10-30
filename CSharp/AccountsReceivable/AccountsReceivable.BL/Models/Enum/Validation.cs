namespace AccountsReceivable.BL.Models.Enum;

public enum ValidationId : byte
{
    Missing = 0,
    Pending = 1,

    Low = 2,
    High = 3,
    Valid = 4
}

public class Validation
{
    /* Properties */

    public ValidationId Id { get; set; }
    public string Name { get; set; } = null!;
}

public class ValidationHelper
{
    private static readonly Dictionary<ValidationId, Validation> Dictionary = new()
    {
        {
            ValidationId.Missing,
            new Validation
            {
                Id = ValidationId.Missing,
                Name = "Missing"
            }
        },
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
        return (Dictionary.TryGetValue(id, out var value) ? value : default) ?? throw new InvalidOperationException();
    }

    public static ICollection<Validation> GetAll()
    {
        return Dictionary.Values;
    }
}