using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Application;

public class Uplift
{
    public uint Id { get; set; }

    public ushort ScheduleId { get; set; }
    public virtual Schedule Schedule { get; set; } = null!;

    public string Name { get; set; } = null!;

    public AnimalTypeId AnimalTypeId { get; set; }
    public virtual AnimalType AnimalType { get; set; } = null!;
    
    public decimal Rate { get; set; }
}