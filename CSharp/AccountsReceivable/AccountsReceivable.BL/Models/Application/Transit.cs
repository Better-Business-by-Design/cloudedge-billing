using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Application;

public class Transit
{
    public uint Id { get; set; }

    public string DocumentId { get; set; } = null!;
    public virtual Document Document { get; set; } = null!;
    
    public DateTime Date { get; set; }

    public ushort Quantity { get; set; }
}