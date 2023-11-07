using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AccountsReceivable.BL.Models.Application;

public class PayMonthlyPlan : IDataRow
{
    [Column("plan_id")]
    public int PlanId { get; set; }
    
    [Column("plan_name")]
    public string PlanName { get; set; }
    
    [Column("local_size")]
    public int? LocalSize { get; set; }
    
    [Column("national_size")]
    public int? NationalSize { get; set; }
    
    [Column("mobile_size")]
    public int? MobileSize { get; set; }
    
    // TODO... remove null
    [Column("price")]
    public decimal? Price { get; set; }

    [JsonIgnore]
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public override string ToString()
    {
        return PlanName;
    }
}