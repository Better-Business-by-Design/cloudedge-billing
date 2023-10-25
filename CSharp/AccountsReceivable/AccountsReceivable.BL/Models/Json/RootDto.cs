using System.Text.Json.Serialization;

namespace AccountsReceivable.BL.Models.Json;

public class RootDto
{
    /* Properties */

    [JsonPropertyName("Status")]
    public int? Status { get; set; }

    [JsonPropertyName("ErrorMessage")]
    public string? ErrorMessage { get; set; }

    [JsonPropertyName("LastEntry")]
    public int? LastEntry { get; set; }

    [JsonPropertyName("DocumentCount")]
    public int? DocumentCount { get; set; }

    [JsonPropertyName("Timestamp")]
    public DateTime Timestamp { get; set; }
    
    [JsonPropertyName("Documents")]
    public virtual ICollection<DocumentDto> Documents { get; set; } = null!;
}