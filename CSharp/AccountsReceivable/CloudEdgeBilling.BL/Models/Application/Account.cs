using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudEdgeBilling.BL.Models.Application;

public class Account : IDataRow
{
    [Key] [Column("account_id")] public Guid AccountId { get; set; }

    [Column("code")] public int Code { get; set; }

    [Column("account_name")] public string Name { get; set; } = null!;

    public static string TypeName => "Account";

    public override string ToString()
    {
        return Name;
    }
}