using AccountsReceivable.BL.Models.Enum;

namespace AccountsReceivable.BL.Models.Application;

public class Animal
{
    public uint Id { get; set; }

    public string DocumentId { get; set; } = null!;
    public virtual Document Document { get; set; } = null!;

    public ulong KillAgenda { get; set; }

    public DateTime DateKilled { get; set; }

    public GradeId GradeId { get; set; }
    public virtual Grade Grade { get; set; } = null!;

    public ulong? NaitEid { get; set; }

    public string? NaitVisual { get; set; }

    public string? SffEarTag { get; set; }

    public string ProcessDescription { get; set; } = null!;

    public string? CondemnedBy { get; set; }

    public string UnitOfPrice { get; set; } = null!;

    public double Price { get; set; }

    public string? SplitPaymentPercentage { get; set; }

    public string? Retained { get; set; }

    public bool MeetsOptimumRange { get; set; }

    public string? MeetsMasterGrade { get; set; }

    public double Ph { get; set; }

    public string Presentation { get; set; } = null!;

    public string? PresentationReason { get; set; }

    public string? TailLengthDescription { get; set; }

    public string? FinishingContract { get; set; }

    public int InventoryCost { get; set; }

    public int FinishingAmount { get; set; }

    public virtual ICollection<DeductionDetail> DeductionDetails { get; set; } = null!;

    public virtual ICollection<PremiumDetail> PremiumDetails { get; set; } = null!;

    public virtual ICollection<string> Defects { get; set; } = null!;

    /*public ValidationId ValidationId { get; set; } = ValidationId.Pending;
    public virtual Validation Validation { get; set; } = null!;*/

    /* SFF Pricing */

    public double StockWeight { get; set; }

    public double WeightCost { get; set; }

    public double DeductionCost { get; set; }

    public double PremiumCost { get; set; }

    public double NetCost { get; set; }

    public double GstCost { get; set; }

    public double GrossCost { get; set; }

    /* Calculated Pricing */

    public double CalcStockWeight { get; set; }

    public double CalcWeightCost { get; set; }

    public double CalcDeductionCost { get; set; }

    public double CalcPremiumCost { get; set; }

    public double CalcNetCost { get; set; }

    public double CalcGstCost { get; set; }

    public double CalcGrossCost { get; set; }

    /* Nested Classes */

    public class DeductionDetail
    {
        public uint AnimalId { get; set; }
        public virtual Animal Animal { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Uom { get; set; } = null!;

        public double Rate { get; set; }

        public double PaymentSummaryAmount { get; set; }
    }

    public class PremiumDetail
    {
        public uint AnimalId { get; set; }
        public virtual Animal Animal { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Uom { get; set; } = null!;

        public double Rate { get; set; }

        public double PaymentSummaryAmount { get; set; }
    }
}