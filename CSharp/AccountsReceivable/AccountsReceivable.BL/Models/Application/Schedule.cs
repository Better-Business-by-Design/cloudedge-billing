using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Application;

public class Schedule
{
    /* Properties */

    public ushort Id { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string MeatworkName { get; set; } = null!;
    public virtual Meatwork Meatwork { get; set; } = null!;

    public StatusId StatusId { get; set; } = StatusId.Pending;
    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<Price> Prices { get; set; } = new List<Price>();
    public virtual ICollection<Uplift> Uplifts { get; set; } = new List<Uplift>();
}