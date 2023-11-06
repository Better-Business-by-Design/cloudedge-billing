using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.API.ViewModels;

public class AnimalPriceGroup
{
    private AnimalTypeId AnimalTypeId { get; }
    public AnimalType AnimalType => AnimalTypeHelper.GetInfo(AnimalTypeId);
    
    private GradeId GradeId { get; }
    public Grade Grade => GradeHelper.GetInfo(GradeId);
    
    private ValidationId ValidationId { get; }
    public Validation Validation => ValidationHelper.GetInfo(ValidationId);
    
    public decimal MinWeight { get; set; }
    
    public decimal MaxWeight { get; set; }
    
    public ushort StockCount { get; set; }
    
    public decimal StockWeight { get; set; }
    
    public decimal PremiumCost { get; set; }
    
    public decimal CalcPremiumCost { get; set; }
    
    public decimal DeductionCost { get; set; }
    
    public decimal CalcDeductionCost { get; set; }
    
    public decimal Cost { get; set; }
    
    public decimal CalcCost { get; set; }

    public AnimalPriceGroup(AnimalTypeId animalTypeId, GradeId gradeId, ValidationId validationId, decimal minWeight, decimal maxWeight, ushort stockCount, decimal stockWeight, decimal premiumCost, decimal calcPremiumCost, decimal deductionCost, decimal calcDeductionCost, decimal cost, decimal calcCost)
    {
        AnimalTypeId = animalTypeId;
        GradeId = gradeId;
        ValidationId = validationId;
        MinWeight = minWeight;
        MaxWeight = maxWeight;
        StockCount = stockCount;
        StockWeight = stockWeight;
        PremiumCost = premiumCost;
        CalcPremiumCost = calcPremiumCost;
        DeductionCost = deductionCost;
        CalcDeductionCost = calcDeductionCost;
        Cost = cost;
        CalcCost = calcCost;
    }
}