using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.API.ViewModels;

public class InvoiceDetails
{
    public Document Document { get; set; } = null!;

    public ICollection<PriceAnimals> PriceAnimals { get; set; } = null!;
}

public class PriceAnimals
{
    public Price Price { get; set; } = null!;
    public Grade Grade { get; set; } = null!;
    
    public ushort StockCount { get; set; }
    
    public decimal StockWeight { get; set; }
    
    public decimal Cost { get; set; }
    
    public decimal CalcCost { get; set; }
}