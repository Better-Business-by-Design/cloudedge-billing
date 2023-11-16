using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using MudBlazor;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace AccountsReceivable.BL.Models.Application;

public class PayMonthlyPlan : IDataRow
{
    public static string TypeName => "Pay Monthly Plan";
    
    [Key]
    [Column("plan_id")]
    public int PlanId { get; set; }

    [Column("plan_name")]
    [Label("Plan Name")]
    public string PlanName { get; set; } = null!;
    
    [Column("local_size")]
    [Label("Local Size")]
    public int? LocalSize { get; set; }
    
    [Column("national_size")]
    [Label("National Size")]
    public int? NationalSize { get; set; }
    
    [Column("mobile_size")]
    [Label("Mobile Size")]
    public int? MobileSize { get; set; }
    
    [Column("international_size")]
    [Label("International Size")]
    public int? InternationalSize { get; set; }
    
    [Column("toll_free_landline_size")]
    [Label("Toll Free Landline Size")]
    public int? TollFreeLandlineSize { get; set; }
    
    [Column("toll_free_mobile_size")]
    [Label("Toll Free Mobile Size")]
    public int? TollFreeMobileSize { get; set; }
    
    [Column("price")]
    [Label("Price")]
    public decimal Price { get; set; }
    
    [Column("min_price")]
    [Label("Min Price")]
    public decimal? MinPrice { get; set; }
    
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