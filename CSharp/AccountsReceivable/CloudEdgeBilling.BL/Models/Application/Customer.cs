using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MudBlazor;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace CloudEdgeBilling.BL.Models.Application;

public class Customer : IDataRow
{
    [Key] [Column("customer_id")] public int Id { get; set; }

    [Column("customer_name")] [Label("Customer Name")] public string CustomerName { get; set; } = null!;

    [Column("domain_name")] [Label("Domain Name")] public string? DomainName { get; set; }

    [Column("domain_uuid")] [Label("Domain UUID")] public Guid? DomainUuid { get; set; }

    [Column("xero_contact_name")] [Label("Xero Contact Name")] public string XeroContactName { get; set; } = null!;

    [ForeignKey(nameof(PayMonthlyPlan))]
    [Column("pay_monthly_plan_id")]
    public int? PayMonthlyPlanId { get; set; }

    [Label("Pay Monthly Plan")]
    public PayMonthlyPlan? PayMonthlyPlan { get; set; }

    [NotMapped] public string PlanName => PayMonthlyPlan?.PlanName ?? string.Empty;

    [Column("is_active")] [Label("Is Active")] public bool IsActive { get; set; }

    [Column("location")] [Label("Location")] public string? Location { get; set; }

    public static string TypeName => "Customer";

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