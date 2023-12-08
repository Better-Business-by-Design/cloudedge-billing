using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;
using FluentValidation;

namespace CloudEdgeBilling.API.Shared.FluidValidation;

public class CustomerFluentValidator : DataRowFluentValidator<Customer>
{
    public CustomerFluentValidator() : base()
    {
        RuleFor(c => c.CustomerName)
            .NotEmpty()
            .Length(1, 100);

        RuleFor(c => c.DomainName)
            .Length(0, 100);

        RuleFor(c => c.XeroContactName)
            .NotEmpty()
            .Length(1, 100);

        //RuleFor(c => c.IsActive);

        RuleFor(c => c.Location)
            .Length(0, 100);
    }
}