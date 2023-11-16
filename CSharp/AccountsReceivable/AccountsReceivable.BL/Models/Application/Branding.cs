using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountsReceivable.BL.Models.Application;

public class Branding : IDataRow
{
    [Key]
    [Column("branding_theme_id")]
    public Guid Id { get; set; }

    [Column("branding_theme_name")] 
    public string Name { get; set; } = null!;
}