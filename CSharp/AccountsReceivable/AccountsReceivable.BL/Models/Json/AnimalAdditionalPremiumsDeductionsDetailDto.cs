using System.Text.Json.Serialization;

namespace AccountsReceivable.BL.Models.Json;

public class AnimalAdditionalPremiumsDeductionsDetailDto
{
    /* Properties */

    [JsonPropertyName("Code")]
    public string Code { get; set; } = null!;

    [JsonPropertyName("Description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("UOM")]
    public string Uom { get; set; } = null!;

    [JsonPropertyName("Rate")]
    public decimal Rate { get; set; }

    [JsonPropertyName("PaymentSummaryAmount")]
    public decimal PaymentSummaryAmount { get; set; }
}