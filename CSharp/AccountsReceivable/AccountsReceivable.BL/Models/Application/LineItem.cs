using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace AccountsReceivable.BL.Models.Application;

public class LineItem : IDataRow
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [ForeignKey(nameof(Customer))]
    [Column("customer_id")]
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    [Column("description")] 
    public string Description { get; set; } = null!;
    
    [Column("quantity")]
    public short Quantity { get; set; }
    
    [Column("unit_price")]
    public decimal UnitPrice { get; set; }
    
    [Column("discount")]
    public decimal Discount { get; set; }

    // TODO... Change this to an Enum
    [Column("account")] 
    public int Account { get; set; }

    // TODO... Change this to an Enum
    [Column("business")] 
    public string Business { get; set; } = null!;
    

    public override string ToString()
    {
        return Description;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;

        if (obj.GetType() == GetType())
        {
            return Id == ((LineItem)obj).Id;
        }
        else
        {
            // ReSharper disable once BaseObjectEqualsIsObjectEquals
            return base.Equals(obj);
        }
    }

    public override int GetHashCode()
    {
        // ReSharper disable once NonReadonlyMemberInGetHashCode
        return Id;
    }
}