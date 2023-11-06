using System.ComponentModel.DataAnnotations.Schema;
using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Application;

public class Animal
{
    public uint Id { get; set; }

    public string DocumentId { get; set; } = null!;
    public virtual Document Document { get; set; } = null!;

    public uint KillAgenda { get; set; }

    public DateTime DateKilled { get; set; }

    public GradeId GradeId { get; set; }
    public virtual Grade Grade { get; set; } = null!;

    public ulong? NaitEid { get; set; }

    public string? NaitVisual { get; set; }

    public string? SffEarTag { get; set; }

    public string ProcessDescription { get; set; } = null!;

    public string? CondemnedBy { get; set; }

    public string UnitOfPrice { get; set; } = null!;

    public string? SplitPaymentPercentage { get; set; }

    public string? Retained { get; set; }

    public bool MeetsOptimumRange { get; set; }

    public bool MeetsMasterGrade { get; set; }

    public decimal Ph { get; set; }

    public string Presentation { get; set; } = null!;

    public string? PresentationReason { get; set; }

    public string? TailLengthDescription { get; set; }

    public string? FinishingContract { get; set; }

    public int InventoryCost { get; set; }

    public int FinishingAmount { get; set; }

    public virtual ICollection<DeductionDetail>? DeductionDetails { get; set; }

    public virtual ICollection<PremiumDetail>? PremiumDetails { get; set; }

    public virtual ICollection<string>? Defects { get; set; }

    /*public ValidationId ValidationId { get; set; } = ValidationId.Pending;
    public virtual Validation Validation { get; set; } = null!;*/

    /* SFF Pricing */

    public decimal Weight { get; set; }

    public decimal Price { get; set; }

    public decimal WeightCost { get; set; }

    public decimal DeductionCost { get; set; }

    public decimal PremiumCost { get; set; }

    public decimal NetCost { get; set; }


    /* Calculated Pricing - All Pamu prices used calculated values */


    public decimal CalcPrice { get; set; }

    public decimal CalcWeightCost { get; set; }

    public decimal CalcDeductionCost { get; set; }

    public decimal CalcPremiumCost { get; set; }

    public decimal CalcNetCost { get; set; }

    public ValidationId ValidationId { get; set; } = ValidationId.Pending;
    public Validation Validation { get; set; } = null!;
}

/* Nested Classes */

public class DeductionDetail
{
    public uint AnimalId { get; set; }
    public virtual Animal Animal { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public decimal Rate { get; set; }

    public decimal PaymentSummaryAmount { get; set; }
}

public class PremiumDetail
{
    public uint AnimalId { get; set; }
    public virtual Animal Animal { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Uom { get; set; } = null!;

    public decimal Rate { get; set; }

    public decimal PaymentSummaryAmount { get; set; }
}