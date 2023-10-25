﻿using System.Text.Json.Serialization;

namespace AccountsReceivable.BL.Models.Json;

public class AnimalPaymentSummaryDetailDto
{
    /* Properties */

    [JsonPropertyName("Code")]
    public string Code { get; set; } = null!;

    [JsonPropertyName("Description")]
    public string Description { get; set; } = null!;

    [JsonPropertyName("UOM")]
    public string Uom { get; set; } = null!;

    [JsonPropertyName("Rate")]
    public double Rate { get; set; }

    [JsonPropertyName("PaymentSummaryAmount")]
    public double PaymentSummaryAmount { get; set; }
}