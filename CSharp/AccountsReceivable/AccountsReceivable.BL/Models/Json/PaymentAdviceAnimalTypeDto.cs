using System.Text.Json.Serialization;

namespace AccountsReceivable.BL.Models.Json;

public class PaymentAdviceAnimalTypeDto
{
    /* Properties */

    [JsonPropertyName("AnimalType")]
    public string AnimalType { get; set; } = null!;

    [JsonPropertyName("AnimalTypeTotalStockReceived")]
    public int AnimalTypeTotalStockReceived { get; set; }

    [JsonPropertyName("AnimalTypeTotalMeatKg")]
    public decimal AnimalTypeTotalMeatKg { get; set; }

    [JsonPropertyName("AnimalTypeTotalPricePaid")]
    public decimal AnimalTypeTotalPricePaid { get; set; }
}