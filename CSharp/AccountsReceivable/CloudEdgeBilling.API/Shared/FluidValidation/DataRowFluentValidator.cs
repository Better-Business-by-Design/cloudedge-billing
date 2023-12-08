using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;
using FluentValidation;
using Newtonsoft.Json;

namespace CloudEdgeBilling.API.Shared.FluidValidation;

/// <summary>
///     Abstract class <c>AuthenticatedDataRowFluentValidator</c> extends <c>AbstractValidator</c> and provides common
///     functionality for checking the current user's permissions.
/// </summary>
/// <remarks>
///     The specification of all field validation rules is handled by the inheritors, specifically in the constructor using
///     the <c>RuleFor</c> method.
/// </remarks>
/// <typeparam name="TDataRow">The type representing the database table row that the validator is made to validate.</typeparam>
public abstract class DataRowFluentValidator<TDataRow> : AbstractValidator<TDataRow> where TDataRow : IDataRow
{
    // ReSharper disable once MemberCanBePrivate.Global

    protected DataRowFluentValidator()
    {
    }


    /// <summary>
    ///     Function <c>ValidateValue</c> presents a function that accepts an object and the name of one of it's properties
    ///     then returns an array of error messages detailing any/all the validation rules it breaks.
    ///     If the object's property is valid then an empty array will be returned.
    /// </summary>
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        Console.WriteLine($"Validating model: {JsonConvert.SerializeObject(model)}");
        var result = await ValidateAsync(
            ValidationContext<TDataRow>.CreateWithOptions((TDataRow)model,
                x => x.IncludeProperties(propertyName)));

        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}