using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using FluentValidation;

namespace AccountsReceivable.API.Shared.FluidValidation;

public class LineItemFluentValidator : DataRowFluentValidator<LineItem>
{
    public LineItemFluentValidator(ApplicationDbContext dbContext) : base(dbContext)
    {
        RuleFor(l => l.Description)
            .NotEmpty()
            .Length(1, 255);

        RuleFor(l => l.Quantity)
            .NotEmpty()
            .GreaterThan(Convert.ToInt16(0));

        RuleFor(l => l.UnitPrice)
            .NotEmpty()
            .GreaterThanOrEqualTo(0M);

        RuleFor(l => l.Discount)
            .NotEmpty()
            .InclusiveBetween(0M, 100M);

        RuleFor(l => l.Account)
            .NotEmpty();

        RuleFor(l => l.Business)
            .NotEmpty()
            .Length(0, 100);
    }
}