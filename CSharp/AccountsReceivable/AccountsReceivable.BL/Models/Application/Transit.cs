using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Application;

public class Transit
{
    public uint Id { get; set; }

    public string? DocumentId { get; set; }
    public virtual Document? Document { get; set; }
    
    public DateTime Date { get; set; }

    public SpeciesTypeId SpeciesTypeId { get; set; }
    public SpeciesType SpeciesType { get; set; } = null!;

    public ushort Quantity { get; set; }
}