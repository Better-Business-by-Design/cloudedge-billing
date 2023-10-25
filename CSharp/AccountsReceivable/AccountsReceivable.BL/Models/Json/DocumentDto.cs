using System.Text.Json.Serialization;

namespace AccountsReceivable.BL.Models.Json;

public class DocumentDto
{
    /* Properties */

    [JsonPropertyName("DocumentType")]
    public string DocumentType { get; set; } = null!;

    [JsonPropertyName("Document")]
    public string Document { get; set; } = null!;

    [JsonPropertyName("PreviousDocument")]
    public string? PreviousDocument { get; set; }

    [JsonPropertyName("DateProcessed")]
    public DateTime DateProcessed { get; set; }

    [JsonPropertyName("FromDateProcessed")]
    public DateTime? FromDateProcessed { get; set; }

    [JsonPropertyName("PaymentDate")]
    public DateTime PaymentDate { get; set; }

    [JsonPropertyName("ProceedsToBankAccount")]
    public string ProceedsToBankAccount { get; set; } = null!;

    [JsonPropertyName("ProceedsToCompanyName")]
    public string? ProceedsToCompanyName { get; set; }

    [JsonPropertyName("Terms")]
    public string Terms { get; set; } = null!;

    [JsonPropertyName("GSTRegistrationNo")]
    public string GstRegistrationNo { get; set; } = null!;

    [JsonPropertyName("SupplierNo")]
    public string SupplierNo { get; set; } = null!;

    [JsonPropertyName("SupplierName")]
    public string SupplierName { get; set; } = null!;

    [JsonPropertyName("DairySupplierNumber")]
    public string? DairySupplierNumber { get; set; }

    [JsonPropertyName("KillSheet")]
    public uint KillSheet { get; set; }

    [JsonPropertyName("BookingRef")]
    public string? BookingRef { get; set; }

    [JsonPropertyName("FieldRepName")]
    public string FieldRepName { get; set; } = null!;

    [JsonPropertyName("CarrierName")]
    public string CarrierName { get; set; } = null!;

    [JsonPropertyName("PlantName")]
    public string PlantName { get; set; } = null!;

    [JsonPropertyName("NaitNo")]
    public string? NaitNo { get; set; }

    [JsonPropertyName("ConsignedFrom")]
    public string? ConsignedFrom { get; set; }

    [JsonPropertyName("PaymentAdviceTotalStockReceived")]
    public int PaymentAdviceTotalStockReceived { get; set; }

    [JsonPropertyName("PaymentAdviceTotalMeatKg")]
    public decimal PaymentAdviceTotalMeatKg { get; set; }

    [JsonPropertyName("PaymentAdviceTotalPricePaid")]
    public decimal PaymentAdviceTotalPricePaid { get; set; }

    [JsonPropertyName("AdditionalPremiumsDeductions")]
    public decimal AdditionalPremiumsDeductions { get; set; }

    [JsonPropertyName("PaymentSummaryAdvanceTotalDeductions")]
    public decimal PaymentSummaryAdvanceTotalDeductions { get; set; }

    [JsonPropertyName("NetAdvance")]
    public decimal NetAdvance { get; set; }

    [JsonPropertyName("GSTOnOutputs")]
    public decimal GstOnOutputs { get; set; }

    [JsonPropertyName("GSTOnInputs")]
    public decimal GstOnInputs { get; set; }

    [JsonPropertyName("Total")]
    public decimal Total { get; set; }

    [JsonPropertyName("SupplierComment")]
    public virtual ICollection<string> SupplierComment { get; set; } = null!;

    [JsonPropertyName("PaymentAdviceAnimalType")]
    public virtual ICollection<PaymentAdviceAnimalTypeDto> PaymentAdviceAnimalType { get; set; } = null!;

    [JsonPropertyName("Animal")]
    public virtual ICollection<AnimalDto> Animal { get; set; } = null!;
}