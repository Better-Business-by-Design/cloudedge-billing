using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace AccountsReceivable.BL.Models.Application;

public class Customer : IDataRow
{
    [Key]
    [Column("tenant_code")]
    public int Id { get; set; }

    [Column("parent_name")]
    public string ParentName { get; set; } = null!;
    
    [Column("customer_name")]
    public string? CustomerName { get; set; }
    
    [Column("domain_uuid")]
    public Guid? DomainUuid { get; set; }

    [Column("invoice_name")] 
    public string InvoiceName { get; set; } = null!;
    
    [ForeignKey(nameof(PayMonthlyPlan))]
    [Column("pay_monthly_plan_id")]
    public int? PayMonthlyPlanId { get; set; }
    
    public PayMonthlyPlan? PayMonthlyPlan { get; set; }
    
    [Column("is_active")]
    public bool IsActive { get; set; }
    
    [Column("location")]
    public string? Location { get; set; }

    // ReSharper disable once CollectionNeverUpdated.Global
    public ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();

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