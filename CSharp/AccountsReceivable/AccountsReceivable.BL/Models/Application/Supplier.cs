namespace AccountsReceivable.BL.Models.Application;

public class Supplier
{
    public int FarmCostCentre { get; set; }
    public virtual Farm Farm { get; set; } = null!;

    public string MeatworkName { get; set; } = null!;
    public virtual Meatwork Meatwork { get; set; } = null!;

    public string SupplierNo { get; set; } = null!;
}