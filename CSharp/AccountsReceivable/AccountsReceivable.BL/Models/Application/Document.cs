using AccountsReceivable.BL.Models.Enum;
using AccountsReceivable.BL.Models.Source;

namespace AccountsReceivable.BL.Models.Application;

public class Document
{
    public string Id { get; set; } = null!;

    public string? PreviousDocumentId { get; set; }
    public virtual Document? PreviousDocument { get; set; }

    public string DocumentType { get; set; } = null!;

    public DateTime DateProcessed { get; set; }

    public DateTime? FromDateProcessed { get; set; }

    public DateTime PaymentDate { get; set; }

    public string ProceedsToBankAccount { get; set; } = null!;

    public string? ProceedsToCompanyName { get; set; }

    public string Terms { get; set; } = null!;

    public string GstRegistrationNo { get; set; } = null!;

    public int FarmCostCentre { get; set; }
    public virtual Farm Farm { get; set; } = null!;

    public string PlantName { get; set; } = null!;
    public virtual Plant Plant { get; set; } = null!;

    public string? DairySupplierNumber { get; set; }

    public uint KillSheet { get; set; }

    public string? BookingRef { get; set; }

    public string FieldRepName { get; set; } = null!;

    public string CarrierName { get; set; } = null!;

    public string? NaitNo { get; set; }

    public string? ConsignedFrom { get; set; }

    public virtual ICollection<Animal>? Animals { get; set; }
    public virtual ICollection<string>? SupplierComments { get; set; }
    public virtual ICollection<AnimalTypeSummary> AnimalTypeSummaries { get; set; } = null!;

    public SpeciesTypeId? SpeciesTypeId { get; set; }
    public virtual SpeciesType? SpeciesType { get; set; }

    public ushort? ScheduleId { get; set; }
    public virtual Schedule? Schedule { get; set; }

    public ushort? TransitId { get; set; }
    public virtual TransitDto? Transit { get; set; }

    public StatusId StatusId { get; set; } = StatusId.Pending;
    public virtual Status Status { get; set; } = null!;

    /*public ValidationId ValidationId { get; set; } = ValidationId.Pending;
    public virtual Validation Validation { get; set; } = null!;*/

    /* SFF Pricing */

    public int StockTotal { get; set; }

    public decimal StockWeightTotal { get; set; }

    public decimal WeightCostTotal { get; set; }

    public decimal DeductionCostTotal { get; set; }

    public decimal PremiumCostTotal { get; set; }

    public decimal NetCostTotal { get; set; }

    public decimal GstCostTotal { get; set; }

    public decimal GrossCostTotal { get; set; }

    /* Calculated Pricing */

    public int CalcStockTotal { get; set; }

    public decimal CalcStockWeightTotal { get; set; }

    public decimal CalcWeightCostTotal { get; set; }

    public decimal CalcDeductionCostTotal { get; set; }

    public decimal CalcPremiumCostTotal { get; set; }

    public decimal CalcNetCostTotal { get; set; }

    public decimal CalcGstCostTotal { get; set; }

    public decimal CalcGrossCostTotal { get; set; }

    
}

/* Nested Classes */

public class AnimalTypeSummary
{
    public string DocumentId { get; set; } = null!;
    public virtual Document Document { get; set; } = null!;

    public AnimalTypeId AnimalTypeId { get; set; }
    public virtual AnimalType AnimalType { get; set; } = null!;

    public int StockCount { get; set; }

    public decimal StockWeightKg { get; set; }

    public decimal StockCost { get; set; }
}