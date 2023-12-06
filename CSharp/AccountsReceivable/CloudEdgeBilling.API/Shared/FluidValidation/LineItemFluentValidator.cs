using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;
using FluentValidation;

namespace CloudEdgeBilling.API.Shared.FluidValidation;

public class LineItemFluentValidator : DataRowFluentValidator<LineItem>
{
    public LineItemFluentValidator(ApplicationDbContext dbContext) : base(dbContext)
    {
        RuleFor(l => l.Description)
            .NotEmpty()
            .Length(1, 255);

        RuleFor(l => l.Quantity)
            .GreaterThan(Convert.ToInt16(0));

        RuleFor(l => l.UnitPrice)
            .GreaterThanOrEqualTo(0M);

        RuleFor(l => l.Discount)
            .InclusiveBetween(0M, 100M);

        RuleFor(l => l.AccountId)
            .NotEmpty();

        RuleFor(l => l.BusinessId)
            .IsInEnum();
    }
}