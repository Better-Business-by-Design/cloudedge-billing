using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Application;

public class Price
{
    public uint Id { get; set; }

    public ushort ScheduleId { get; set; }
    public virtual Schedule Schedule { get; set; } = null!;

    public GradeId GradeId { get; set; }
    public virtual Grade Grade { get; set; } = null!;

    public double MinWeight { get; set; }

    public double MaxWeight { get; set; }

    public double Modifier { get; set; }

    public double Cost { get; set; }
}