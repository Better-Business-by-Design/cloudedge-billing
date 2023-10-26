using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.API.ViewModels;

public class GradeGroup
{
    public GradeId GradeId { get; set; }
    
    public decimal MinWeight { get; set; }
    
    public decimal MaxWeight { get; set; }
    
    public ushort AnimalTotal { get; set; }
    
    public decimal NetCost { get; set; }
    
    public decimal CalcNetCost { get; set; }
}