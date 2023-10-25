namespace AccountsReceivable.BL.Models.Enum;

public enum SpeciesTypeId : byte
{
    Bobby = 0,
    Bovine = 1,
    Ovine = 2,
    Deer = 3
}

public class SpeciesType
{
    /* Properties */

    public SpeciesTypeId Id { get; set; }
    public string Name { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
}

public class SpeciesTypeHelper
{
    private static readonly Dictionary<SpeciesTypeId, SpeciesType> _dictionary = new()
    {
        {
            SpeciesTypeId.Bobby,
            new SpeciesType
            {
                Id = SpeciesTypeId.Bobby,
                Name = "BOBBY",
                DisplayName = "Bobby"
            }
        },
        {
            SpeciesTypeId.Bovine,
            new SpeciesType
            {
                Id = SpeciesTypeId.Bovine,
                Name = "BOVINE",
                DisplayName = "Cattle"
            }
        },
        {
            SpeciesTypeId.Ovine,
            new SpeciesType
            {
                Id = SpeciesTypeId.Ovine,
                Name = "OVINE",
                DisplayName = "Sheep"
            }
        },
        {
            SpeciesTypeId.Deer,
            new SpeciesType
            {
                Id = SpeciesTypeId.Deer,
                Name = "DEER",
                DisplayName = "Deer"
            }
        }
    };

    public static SpeciesType GetInfo(SpeciesTypeId id)
    {
        return (_dictionary.TryGetValue(id, out var value) ? value : default) ?? throw new InvalidOperationException();
    }

    public static ICollection<SpeciesType> GetAll()
    {
        return _dictionary.Values;
    }
}