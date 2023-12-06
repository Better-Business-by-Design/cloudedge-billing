using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsReceivable.BL.Models.Application;

public class Invoice : IDataRow
{
    public static string TypeName => "Invoice";
    
    [Key]
    [Column("customer_id", Order = 1)]
    public int CustomerId { get; set; }
    
    [Column("domain_uuid")]
    public Guid DomainUuid { get; set; }

    [Column("customer_name")]
    public string CustomerName { get; set; } = null!;

    [Column("xero_contact_name")]
    public string XeroContactName { get; set; } = null!;
    
    [Key]
    [Column("date_time", Order = 2)]
    public DateTime DateTime { get; set; }
    
    [Column("landline_mobile_sum_cost")]
    public decimal LandlineMobileSumCost { get; set; }
    
    [Column("landline_national_sum_cost")]
    public decimal LandlineNationalSumCost { get; set; }
    
    [Column("landline_international_sum_cost")]
    public decimal LandlineInternationalSumCost { get; set; }
    
    [Column("toll_free_from_landline_sum_cost")]
    public decimal TollFreeFromLandlineSumCost { get; set; }
    
    [Column("toll_free_from_mobile_sum_cost")]
    public decimal TollFreeFromMobileSumCost { get; set; }
    
    [Column("total_voip_cost")]
    public decimal TotalVoipCost { get; set; }
    
    [Column("total_toll_free_cost")]
    public decimal TotalTollFreeCost { get; set; }
    
    [Column("total_cost")]
    public decimal TotalCost { get; set; }
    
    [Column("plan_name")]
    public string? PlanName { get; set; }
    
    [Column("plan_price")]
    public decimal? PlanPrice { get; set; }
}