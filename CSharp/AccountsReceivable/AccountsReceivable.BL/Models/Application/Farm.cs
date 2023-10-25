namespace AccountsReceivable.BL.Models.Application;

public class Farm
{
    public int CostCentre { get; set; }

    public string Name { get; set; } = null!;

    public string ManagerName { get; set; } = null!;

    public string ManagerEmail { get; set; } = null!;
}