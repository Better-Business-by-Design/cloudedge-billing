using AccountsReceivable.API.Shared.FluidValidation;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AccountsReceivable.API.Shared.NewDataRowForm;

/// <summary>
/// Abstract class <c>AddNewDataRowForm</c> represents a component using a MudForm to accept and validate user input
/// that can then be inputted into the database as a new record.
/// </summary>
/// <remarks>
/// Uses the built-in functionality from MudForm to use FluentValidation and DataAnnotations.
/// </remarks>
/// <typeparam name="T">
/// The type representing a database table row that the form is trying to generate a new valid version of.
/// Must extend IDataRow and be stored in a DbSet in the DBContext.
/// </typeparam>
public abstract partial class AddNewDataRowForm<T> : ComponentBase where T : IDataRow, new()
{
    /// <value>
    /// Property <c>IsVisible</c> dictates whether or not the form is rendered.
    /// </value>>
    /// <remarks>
    /// <c>IsVisible</c> is implemented by the inheritors and can be ignored if the form is always visible.
    /// </remarks>
    public bool IsVisible { get; set; }
    
    /// <value>
    /// Cascading parameter <c>Validator</c> passes down a <c>FluentValidator</c> that the MudForm can use for validation.
    /// </value>
    /// <remarks>
    /// We pass this down instead of creating it in the inheritor as generally the parent of an <c>AddNewDataRowForm</c>
    /// is an <c>EditableDataGridPage</c> which also needs the validator to implement row editing.
    /// </remarks>
    [CascadingParameter] protected DataRowFluentValidator<T> Validator { get; set; } = null!;

    /// <value>
    /// Parameter <c>BuildNewDefaultRow</c> provides functionality from the parent for generating the default values
    /// for the row object backing the form.
    /// </value>
    [Parameter] public Func<Task<T>> BuildNewDefaultRow { get; set; } = null!;
    
    /// <value>
    /// Event callback <c>OnNewDataRowSubmitted</c> exposes the result of the form, once successfully validated, to the
    /// parent so the parent can implement the process of actually updating the database with the new record. 
    /// </value>
    [Parameter] public EventCallback<T> OnNewDataRowSubmitted { get; set; }

    // The new data row backing the MudForm.
    protected T? NewDataRow { get; set; }
    
    // The MudForm this class represents.
    protected MudForm Form { get; set; } = default!;
    
    [Inject] protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    /*
     We do it this way instead of getting the parent to just pass down a default row because we need to be able to 
    reset the value when the form closes or gets submitted.
    */
    protected override async Task OnParametersSetAsync()
    {
        NewDataRow = await BuildNewDefaultRow.Invoke();
    }
    
    /// <summary>
    /// Public task <c>OpenForm</c> provides easy access for the parent to control the state of the form's visibility.
    /// </summary>
    /// <remarks>
    /// <c>OpenForm</c> is not used if the inheritor does not implement <see cref="IsVisible"/>
    /// </remarks>
    public async Task OpenForm()
    {
        IsVisible = true;
        NewDataRow = await BuildNewDefaultRow.Invoke();
        StateHasChanged();
    }

    /// <summary>
    /// Task <c>OnCancel</c> ensures that the form values are reset and, if <see cref="IsVisible"/> is implemented,
    /// that the form closes, when the user presses the exit/cancel button.
    /// </summary>
    protected async Task OnCancel()
    {
        IsVisible = false;
        NewDataRow = await BuildNewDefaultRow.Invoke();
        StateHasChanged();
    }

    /// <summary>
    /// Task <c>OnSubmit</c> ensures that the inputted form values are valid before the results are passed back to the
    /// parent. It also resets the form values.
    /// </summary>
    /// <remarks>
    /// A better implementation would be to simply bind <c>Form.IsValid</c> to a variable and then use it to disable
    /// the submit button.
    /// </remarks>
    /// <seealso cref="OnNewDataRowSubmitted"/>
    protected async Task OnSubmit()
    {
        await Form.Validate();
        if (Form.IsValid)
        {
            IsVisible = false;
            await OnNewDataRowSubmitted.InvokeAsync(NewDataRow);
            NewDataRow = await BuildNewDefaultRow.Invoke();
            StateHasChanged();
        }
    }
}