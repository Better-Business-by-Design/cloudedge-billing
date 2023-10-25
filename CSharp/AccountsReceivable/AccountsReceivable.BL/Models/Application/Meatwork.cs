namespace AccountsReceivable.BL.Models.Application;

public class Meatwork
{
    public string Name { get; set; } = null!;

    public virtual ICollection<Plant> Plants { get; set; } = new List<Plant>();
    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}