namespace AccountsReceivable.BL.Models.Enum;

public enum AnimalTypeId : byte
{
    Bobby = 0,
    Bull = 1,
    Cow = 2,
    MCow = 3,
    Heifer = 4,
    Steer = 5,

    Lamb = 6,
    Mutton = 7,
    Ram = 8,

    Hind = 9,
    Stag = 10
}

public class AnimalType
{
    /* Properties */

    public AnimalTypeId Id { get; set; }
    public SpeciesTypeId SpeciesTypeId { get; set; }
    public string Name { get; set; } = null!;
    public string DisplayName { get; set; } = null!;

    /* Navigation */

    public virtual SpeciesType SpeciesType { get; set; } = null!;
}

public class AnimalTypeHelper
{
    private static readonly Dictionary<AnimalTypeId, AnimalType> _dictionary = new()
    {
        {
            AnimalTypeId.Bobby,
            new AnimalType
            {
                Id = AnimalTypeId.Bobby,
                SpeciesTypeId = SpeciesTypeId.Bobby,
                Name = "BOBBY",
                DisplayName = "Bobby Calves"
            }
        },
        {
            AnimalTypeId.Bull,
            new AnimalType
            {
                Id = AnimalTypeId.Bull,
                SpeciesTypeId = SpeciesTypeId.Bovine,
                Name = "BULL",
                DisplayName = "Bull"
            }
        },
        {
            AnimalTypeId.Cow,
            new AnimalType
            {
                Id = AnimalTypeId.Cow,
                SpeciesTypeId = SpeciesTypeId.Bovine,
                Name = "COW",
                DisplayName = "Cow"
            }
        },
        {
            AnimalTypeId.MCow,
            new AnimalType
            {
                Id = AnimalTypeId.MCow,
                SpeciesTypeId = SpeciesTypeId.Bovine,
                Name = "MCOW",
                DisplayName = "Manufacturing Cow"
            }
        },
        {
            AnimalTypeId.Heifer,
            new AnimalType
            {
                Id = AnimalTypeId.Heifer,
                SpeciesTypeId = SpeciesTypeId.Bovine,
                Name = "HEIFER",
                DisplayName = "Heifer"
            }
        },
        {
            AnimalTypeId.Steer,
            new AnimalType
            {
                Id = AnimalTypeId.Steer,
                SpeciesTypeId = SpeciesTypeId.Bovine,
                Name = "STEER",
                DisplayName = "Steer"
            }
        },
        {
            AnimalTypeId.Lamb,
            new AnimalType
            {
                Id = AnimalTypeId.Lamb,
                SpeciesTypeId = SpeciesTypeId.Ovine,
                Name = "LAMB",
                DisplayName = "Lamb"
            }
        },
        {
            AnimalTypeId.Mutton,
            new AnimalType
            {
                Id = AnimalTypeId.Mutton,
                SpeciesTypeId = SpeciesTypeId.Ovine,
                Name = "MUTTON",
                DisplayName = "Mutton"
            }
        },
        {
            AnimalTypeId.Ram,
            new AnimalType
            {
                Id = AnimalTypeId.Ram,
                SpeciesTypeId = SpeciesTypeId.Ovine,
                Name = "RAM",
                DisplayName = "Ram"
            }
        },
        {
            AnimalTypeId.Hind,
            new AnimalType
            {
                Id = AnimalTypeId.Hind,
                SpeciesTypeId = SpeciesTypeId.Deer,
                Name = "HIND",
                DisplayName = "Hind"
            }
        },
        {
            AnimalTypeId.Stag,
            new AnimalType
            {
                Id = AnimalTypeId.Stag,
                SpeciesTypeId = SpeciesTypeId.Deer,
                Name = "STAG",
                DisplayName = "Stag"
            }
        }
    };

    public static AnimalType GetInfo(AnimalTypeId id)
    {
        return (_dictionary.TryGetValue(id, out var value) ? value : default) ?? throw new InvalidOperationException();
    }

    public static ICollection<AnimalType> GetAll()
    {
        return _dictionary.Values;
    }
}