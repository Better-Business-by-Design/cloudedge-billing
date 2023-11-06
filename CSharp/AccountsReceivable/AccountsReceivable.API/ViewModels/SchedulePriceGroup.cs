using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.API.ViewModels;

public class SchedulePriceGroup
{
    private AnimalTypeId AnimalTypeId { get; }
    public AnimalType AnimalType => AnimalTypeHelper.GetInfo(AnimalTypeId);
    
    private GradeId[] GradeIds { get; }
    public Grade[] Grades => GradeIds.Select(GradeHelper.GetInfo).ToArray();
    
    public decimal MinWeight { get; }
    public decimal MaxWeight { get; }
    
    public decimal Cost { get; }
    
    public SchedulePriceGroup(AnimalTypeId animalTypeId, GradeId[] gradeIds, decimal minWeight, decimal maxWeight, decimal cost)
    {
        AnimalTypeId = animalTypeId;
        GradeIds = gradeIds;
        MinWeight = minWeight;
        MaxWeight = maxWeight;
        Cost = cost;
    }
}