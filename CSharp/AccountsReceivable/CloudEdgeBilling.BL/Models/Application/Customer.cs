using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace CloudEdgeBilling.BL.Models.Application;

public class Customer : IDataRow
{
    public static string TypeName => "Customer";

    [Key]
    [Column("customer_id")]
    public int Id { get; set; }

    [Column("customer_name")]
    public string CustomerName { get; set; } = null!;
    
    [Column("domain_name")]
    public string? DomainName { get; set; }
    
    [Column("domain_uuid")]
    public Guid? DomainUuid { get; set; }

    [Column("xero_contact_name")] 
    public string XeroContactName { get; set; } = null!;
    
    [ForeignKey(nameof(PayMonthlyPlan))]
    [Column("pay_monthly_plan_id")]
    public int? PayMonthlyPlanId { get; set; }
    
    public PayMonthlyPlan? PayMonthlyPlan { get; set; }
    
    [Column("is_active")]
    public bool IsActive { get; set; }
    
    [Column("location")]
    public string? Location { get; set; }
    
    public override string ToString()
    {
        return Id.ToString();
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj.GetType() == GetType()) return ((Customer)obj).Id == Id;
        // ReSharper disable once BaseObjectEqualsIsObjectEquals
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return Id;
    }
}