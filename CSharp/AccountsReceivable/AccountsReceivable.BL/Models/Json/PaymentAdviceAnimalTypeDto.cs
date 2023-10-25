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
    public double AnimalTypeTotalMeatKg { get; set; }

    [JsonPropertyName("AnimalTypeTotalPricePaid")]
    public double AnimalTypeTotalPricePaid { get; set; }
}