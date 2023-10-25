using System.Text.Json.Serialization;

namespace AccountsReceivable.BL.Models.Json;

public class DefectDto
{
    /* Properties */

    [JsonPropertyName("DefectDescription")]
    public string DefectDescription { get; set; } = null!;
}