using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace AccountsReceivable.BL.Models.Application;

public class Customer : IDataRow
{
    [Column("tenant_code")]
    public int Id { get; set; }

    [Column("parent_name")]
    public string ParentName { get; set; } = null!;
    
    [Column("customer_name")]
    public string? CustomerName { get; set; }
    
    [Column("CustomRates")]
    public bool? HasCustomRates { get; set; }
    
    public Guid? DomainUuid { get; set; }
    
    [Column("CustomPackage")]
    public bool? HasCustomPackage { get; set; }
    
    [Column("invoice_name")]
    public string? InvoiceName { get; set; }

    [Column("pay_monthly_plan_id")]
    public int? PayMonthlyPlanId { get; set; }
    
    public virtual PayMonthlyPlan? PayMonthlyPlan { get; set; }
    
    [Column("is_active")]
    public bool IsActive { get; set; }
    
    [Column("location")]
    public string? Location { get; set; }

    public virtual ICollection<LineItem> LineItems { get; set; } = new List<LineItem>();
    
    
}