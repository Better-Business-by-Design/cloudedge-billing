using AccountsReceivable.API.Pages;
using AccountsReceivable.BAL.Data;
using FluentValidation;

namespace AccountsReceivable.API.Shared.FluidValidation;

public class CloudEdgeBillingJobArgumentsFluidValidator : DataRowFluentValidator<Robot.CloudEdgeBillingJobArguments>
{
    public CloudEdgeBillingJobArgumentsFluidValidator(ApplicationDbContext dbContext) : base(dbContext)
    {
        RuleFor(a => a.in_StartRangeDateTime)
            .NotEmpty()
            .LessThan(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)).WithMessage("Invoice month must be in the past");
    }
}