using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsReceivable.BL.Models.Application;

public class LineItem
{
    [Column("ID")]
    public int Id { get; set; }

    [Column("ClientID")]
    public int? CustomerId { get; set; }
    
    public Customer? Customer { get; set; }

    [Column("ClientName")] 
    public string? CustomerName { get; set; }
    
    [Column("Description")]
    public string? Description { get; set; }
    
    [Column("Qty")]
    public int? Quantity { get; set; }
    
    [Column("UnitPrice")]
    public decimal? UnitPrice { get; set; }
    
    [Column("Discount")]
    public decimal? Discount { get; set; }
    
    [Column("Account")]
    public string? Account { get; set; }
    
    [Column("Business")]
    public string? Business { get; set; }
}