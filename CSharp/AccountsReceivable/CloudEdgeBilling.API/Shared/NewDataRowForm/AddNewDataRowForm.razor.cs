using CloudEdgeBilling.API.Shared.FluidValidation;
using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CloudEdgeBilling.API.Shared.NewDataRowForm;

/// <summary>
///     Abstract class <c>AddNewDataRowForm</c> represents a component using a MudForm to accept and validate user input
///     that can then be inputted into the database as a new record.
/// </summary>
/// <remarks>
///     Uses the built-in functionality from MudForm to use FluentValidation and DataAnnotations.
/// </remarks>
/// <typeparam name="T">
///     The type representing a database table row that the form is trying to generate a new valid version of.
///     Must extend IDataRow and be stored in a DbSet in the DBContext.
/// </typeparam>
public abstract partial class AddNewDataRowForm<T> : ComponentBase where T : IDataRow, new()
{
    [CascadingParameter] public required IDialogService DialogService { get; set; }
    [CascadingParameter] public required MudDialogInstance MudDialog { get; set; }

    /// <value>
    ///     Cascading parameter <c>Validator</c> passes down a <c>FluentValidator</c> that the MudForm can use for validation.
    /// </value>
    /// <remarks>
    ///     We pass this down instead of creating it in the inheritor as generally the parent of an <c>AddNewDataRowForm</c>
    ///     is an <c>EditableDataGridPage</c> which also needs the validator to implement row editing.
    /// </remarks>
    [Parameter]
    public DataRowFluentValidator<T> Validator { get; set; } = null!;

    // The new data row backing the MudForm.
    [Parameter] public required T NewDataRow { get; set; }

    // The MudForm this class represents.
    protected MudForm Form { get; set; } = default!;

    protected void OnCancel()
    {
        MudDialog.Cancel();
    }

    /// <summary>
    ///     Task <c>OnSubmit</c> ensures that the inputted form values are valid before the results are passed back to the
    ///     parent.
    /// </summary>
    /// <remarks>
    ///     A better implementation might be to simply bind <c>Form.IsValid</c> to a variable and then use it to disable
    ///     the submit button.
    /// </remarks>
    // ReSharper disable once VirtualMemberNeverOverridden.Global
    protected virtual async Task OnSubmit()
    {
        await Form.Validate();
        if (Form.IsValid) MudDialog.Close(DialogResult.Ok(NewDataRow));
    }
}