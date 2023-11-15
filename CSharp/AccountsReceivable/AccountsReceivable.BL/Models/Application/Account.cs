using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsReceivable.BL.Models.Application;

public class Account : IDataRow
{
    [Key]
    [Column("account_id")]
    public Guid AccountId { get; set; }
    
    [Column("code")]
    public int Code { get; set; }

    [Column("name")] 
    public string Name { get; set; } = null!;
}