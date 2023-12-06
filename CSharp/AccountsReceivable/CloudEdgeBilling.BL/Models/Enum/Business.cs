using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CloudEdgeBilling.BL.Models.Application;

namespace CloudEdgeBilling.BL.Models.Enum;

public enum BusinessId : byte
{
    None = 0,
    CloudEdge = 1,
    MaxHub = 2,
    Merge = 3
}

public class Business : IDataRow
{
    public static string TypeName => "Business";
    
    [Key]
    [Column("business_id")]
    public BusinessId BusinessId { get; set; }
    
    [Column("business_name")]
    public string Name { get; set; } = null!;

    [ForeignKey(nameof(Branding))] 
    [Column("branding_theme_id")]
    public Guid BrandingThemeId { get; set; }

    public virtual Branding Branding { get; set; } = null!;
}

public static class BusinessHelper
{
    private static readonly Dictionary<BusinessId, Business> Dictionary = new()
    {
        {
            BusinessId.None,
            new Business
            {
                BusinessId = BusinessId.None,
                Name = "None"
            }
        },
        {
            BusinessId.CloudEdge,
            new Business
            {
                BusinessId = BusinessId.CloudEdge,
                Name = "Cloud Edge"
            }
        },
        {
            BusinessId.MaxHub,
            new Business
            {
                BusinessId = BusinessId.MaxHub,
                Name = "MaxHub"
            }
        },
        {
            BusinessId.Merge,
            new Business
            {
                BusinessId = BusinessId.Merge,
                Name = "Merge"
            }
        }
    };

    public static Business GetInfo(BusinessId id)
    {
        return (Dictionary.TryGetValue(id, out var value) ? value : null) ?? throw new InvalidOperationException();
    }

    public static ICollection<Business> GetAll()
    {
        return Dictionary.Values;
    }
}