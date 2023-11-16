using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using FluentValidation;

namespace AccountsReceivable.API.Shared.FluidValidation;

public class PayMonthlyPlanFluentValidator: DataRowFluentValidator<PayMonthlyPlan>
{
    public PayMonthlyPlanFluentValidator(ApplicationDbContext dbContext) : base(dbContext)
    {
        RuleFor(p => p.PlanName)
            .NotEmpty()
            .Length(1, 255);

        RuleFor(l => l.LocalSize)
            .GreaterThan(0);

        RuleFor(l => l.NationalSize)
            .GreaterThan(0);

        RuleFor(l => l.MobileSize)
            .GreaterThan(0);

        RuleFor(l => l.InternationalSize)
            .GreaterThan(0);

        RuleFor(l => l.TollFreeLandlineSize)
            .GreaterThan(0);

        RuleFor(p => p.TollFreeMobileSize)
            .GreaterThan(0);

        RuleFor(p => p.Price)
            .NotNull()
            .GreaterThanOrEqualTo(0);

        RuleFor(p => p.MinPrice)
            .GreaterThan(0);
    }
}