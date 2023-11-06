namespace AccountsReceivable.BL.Models.Application;

public class PayMonthlyPlan
{
    public int PlanId { get; set; }
    
    public string PlanName { get; set; }
    
    public int? LocalSize { get; set; }
    
    public int? NationalSize { get; set; }
    
    public int? MobileSize { get; set; }
    
    // TODO... remove null
    public decimal? Price { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}