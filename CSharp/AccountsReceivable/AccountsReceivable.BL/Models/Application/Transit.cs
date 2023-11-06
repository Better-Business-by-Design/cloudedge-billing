using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Application;

public class Transit
{
    public uint Id { get; set; }

    public string? DocumentId { get; set; }
    public virtual Document? Document { get; set; }

    public ushort FarmCostCentre { get; set; }
    public virtual Farm Farm { get; set; } = null!;

    public string PlantName { get; set; } = null!;
    public virtual Plant Plant { get; set; } = null!;

    public DateTime Date { get; set; }

    public SpeciesTypeId SpeciesTypeId { get; set; }
    public SpeciesType SpeciesType { get; set; } = null!;

    public ushort Quantity { get; set; }
}