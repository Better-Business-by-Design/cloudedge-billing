namespace AccountsReceivable.BL.Models.Source;

public class TransitDto
{
    /* Properties */

    public ushort Id { get; set; }

    public DateTime Date { get; set; }

    public int CostCentre { get; set; }

    public string PlantName { get; set; } = null!;

    public string AnimalType { get; set; } = null!;

    public int Quantity { get; set; }
}