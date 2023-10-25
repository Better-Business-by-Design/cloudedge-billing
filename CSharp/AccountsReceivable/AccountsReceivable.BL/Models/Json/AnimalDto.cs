using System.Text.Json.Serialization;

namespace AccountsReceivable.BL.Models.Json;

public class AnimalDto
{
    /* Properties */

    [JsonPropertyName("KillAgenda")]
    public ulong KillAgenda { get; set; }

    [JsonPropertyName("DateKilled")]
    public DateTime DateKilled { get; set; }

    [JsonPropertyName("SpeciesType")]
    public string SpeciesType { get; set; } = null!;

    [JsonPropertyName("AnimalType")]
    public string AnimalType { get; set; } = null!;

    [JsonPropertyName("NAITEID")]
    public ulong? NaitEid { get; set; }

    [JsonPropertyName("NAITVisual")]
    public string? NaitVisual { get; set; }

    [JsonPropertyName("SFFEarTag")]
    public string? SffEarTag { get; set; }

    [JsonPropertyName("ProcessDescription")]
    public string ProcessDescription { get; set; } = null!;

    [JsonPropertyName("CondemnedBy")]
    public string? CondemnedBy { get; set; }

    [JsonPropertyName("Weight")]
    public double Weight { get; set; }

    [JsonPropertyName("Grade")]
    public string Grade { get; set; } = null!;

    [JsonPropertyName("UnitOfPrice")]
    public string UnitOfPrice { get; set; } = null!;

    [JsonPropertyName("Price")]
    public double Price { get; set; }

    [JsonPropertyName("PaymentAdvicePricePaid")]
    public double PaymentAdvicePricePaid { get; set; }

    [JsonPropertyName("SplitPaymentPercentage")]
    public string? SplitPaymentPercentage { get; set; }

    [JsonPropertyName("Retained")]
    public string? Retained { get; set; }

    [JsonPropertyName("MeetsOptimumRange")]
    public bool MeetsOptimumRange { get; set; }

    [JsonPropertyName("MeetsMasterGrade")]
    public string? MeetsMasterGrade { get; set; }

    [JsonPropertyName("pH")]
    public double Ph { get; set; }

    [JsonPropertyName("Presentation")]
    public string Presentation { get; set; } = null!;

    [JsonPropertyName("PresentationReason")]
    public string? PresentationReason { get; set; }

    [JsonPropertyName("TailLengthDescription")]
    public string? TailLengthDescription { get; set; }

    [JsonPropertyName("FinishingContract")]
    public string? FinishingContract { get; set; }

    [JsonPropertyName("InventoryCost")]
    public int InventoryCost { get; set; }

    [JsonPropertyName("FinishingAmount")]
    public int FinishingAmount { get; set; }

    public string DocumentDocument { get; set; } = null!;

    /* Navigation */

    [JsonPropertyName("AnimalAdditionalPremiumsDeductionsDetail")]
    public virtual ICollection<AnimalAdditionalPremiumsDeductionsDetailDto> AnimalAdditionalPremiumsDeductionsDetail
    {
        get;
        set;
    } = null!;

    [JsonPropertyName("AnimalPaymentSummaryDetail")]
    public virtual ICollection<AnimalPaymentSummaryDetailDto> AnimalPaymentSummaryDetail { get; set; } = null!;

    [JsonPropertyName("Defects")]
    public virtual ICollection<DefectDto> Defects { get; set; } = null!;
}