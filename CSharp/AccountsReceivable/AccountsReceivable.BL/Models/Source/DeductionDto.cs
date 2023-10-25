namespace AccountsReceivable.BL.Models.Source;

public class DeductionDto
{
    /* Properties */

    public ushort Id { get; set; }

    public DateTime StartDate { get; set; }

    public string Meatworks { get; set; } = null!;

    public string Species { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public double Rate { get; set; }
}