using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace AccountsReceivable.BL.Models.Application;

public class PayMonthlyPlan : IDataRow
{
    [Key]
    [Column("plan_id")]
    public int PlanId { get; set; }

    [Column("plan_name")] 
    public string PlanName { get; set; } = null!;
    
    [Column("local_size")]
    public int? LocalSize { get; set; }
    
    [Column("national_size")]
    public int? NationalSize { get; set; }
    
    [Column("mobile_size")]
    public int? MobileSize { get; set; }
    
    [Column("price")]
    public decimal Price { get; set; }
    
    [Column("min_price")]
    public decimal? MinPrice { get; set; }

    [JsonIgnore]
    // ReSharper disable once CollectionNeverUpdated.Global
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public override string ToString()
    {
        return PlanName;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() == GetType()) return ((PayMonthlyPlan)obj).PlanId == PlanId;
        // ReSharper disable once BaseObjectEqualsIsObjectEquals
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return PlanId;
    }
}