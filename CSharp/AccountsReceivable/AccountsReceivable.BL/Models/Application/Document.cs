using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Application;

public class Document
{
    public string Id { get; set; } = null!;

    public string? PreviousDocumentId { get; set; }
    public virtual Document? PreviousDocument { get; set; }

    public string DocumentType { get; set; } = null!;

    public byte DocumentVersion { get; set; } = 1;

    public DateTime DateProcessed { get; set; }

    public DateTime? FromDateProcessed { get; set; }

    public DateTime PaymentDate { get; set; }

    public string ProceedsToBankAccount { get; set; } = null!;

    public string? ProceedsToCompanyName { get; set; }

    public string Terms { get; set; } = null!;

    public string GstRegistrationNo { get; set; } = null!;

    public ushort FarmCostCentre { get; set; }
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
    
    public virtual ICollection<Comment>? StaffComments { get; set; }
    
    
    public virtual ICollection<AnimalTypeSummary> AnimalTypeSummaries { get; set; } = null!;
    
    public SpeciesTypeId SpeciesTypeId { get; set; }
    public virtual SpeciesType SpeciesType { get; set; } = null!;
    
    public StatusId StatusId { get; set; } = StatusId.Pending;
    public virtual Status Status { get; set; } = null!;
    
    /* Transit */
    
    public ushort TransitQuantity { get; set; }
    
    public virtual ICollection<Transit>? Transits { get; set; }

    /* SFF Pricing */

    public ushort StockQuantity { get; set; }
    
    public decimal WeightTotal { get; set; }
    
    public decimal WeightCostTotal { get; set; }

    public decimal DeductionCostTotal { get; set; }

    public decimal PremiumCostTotal { get; set; }

    public decimal NetCostTotal { get; set; }
    
    /* Calculated Pricing - All Pamu prices used calculated values */
    
    public ushort? ScheduleId { get; set; }
    public virtual Schedule? Schedule { get; set; }
    
    public decimal CalcWeightCostTotal { get; set; }

    public decimal CalcDeductionCostTotal { get; set; }

    public decimal CalcPremiumCostTotal { get; set; }

    public decimal CalcNetCostTotal { get; set; }
    
    public DateTime? CalcTimestamp { get; set; }

    public ValidationId CalcValidationId { get; set; } = ValidationId.Pending;
    public virtual Validation CalcValidation { get; set; } = null!;
    
    
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