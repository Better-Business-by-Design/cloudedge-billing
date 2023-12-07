using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CloudEdgeBilling.API.Shared.EditTemplates;

/// <summary>
///     Abstract class <c>DataRowEditor</c> provides missing functionality for MudBlazor's MudDataGrid EditTemplate
///     implementation. Code originally taken from <a href="https://github.com/MudBlazor/MudBlazor/issues/4811#issuecomment-1594812883">here</a>.
/// </summary>
/// <example>
/// <code>
/// &lt;PropertyColumn Property="@(x =&gt; x.Account.Name)" Title="Account" Required="false"&gt;
///     &lt;EditTemplate&gt;
///         &lt;AccountEditor Context="@context" Accounts="Accounts" /&gt;
///     &lt;/EditTemplate&gt;
/// &lt;/PropertyColumn&gt;
/// </code>
/// </example>
/// <remarks>
///     Using a MudSelect naively inside a MudDataGrid will run into an issue where the <c>CommittedItemChanges</c>
///     method does not fire when the selected value is changed. This class, and it's inheritors, are used to manually
///     fire the <c>CommittedItemChanges</c> method when a value is changed.
/// </remarks>
/// <typeparam name="TGrid">The type of data row being edited</typeparam>
/// <typeparam name="TValue">The type of value being selected</typeparam>
public abstract partial class DataRowEditor<TGrid, TValue> 
    where TGrid : IDataRow 
    where TValue : IDataRow
{
    /// <summary>
    /// The current Data Row whose values are being edited, wrapped in a <c>CellContext</c>.
    /// </summary>
    /// <remarks>
    /// Must be passed in manually as it is, annoyingly, not an automatically cascading parameter similar to
    /// <see cref="DataGrid"/>.
    /// </remarks>
    [Parameter]
    [EditorRequired]
    public CellContext<TGrid> Context { get; set; } = default!;
    
    /// <summary>
    /// The set of all possible/valid values for the user to select between.
    /// </summary>
    /// <remarks>
    /// Directly passing in the <c>DbSet</c> is not recommended as it will cause an SQL query to be sent for every
    /// single one of these selectors. A more efficient solution is to capture the DbSet as an immutable
    /// list on component initialization and then feed that list to this class.
    /// That way the SQL query will only be sent once.
    /// </remarks>
    [Parameter]
    public IEnumerable<TValue> Values { get; set; } = default!;
    
    /// <summary>
    /// The <c>DataGrid</c> this class is being used as an edit selector in.
    /// </summary>
    /// <remarks>
    /// Cascading parameter is passed implicitly by the <c>EditTemplate</c> tag this class expects to be placed inside of.
    /// </remarks>
    [CascadingParameter]
    public MudDataGrid<TGrid> DataGrid { get; set; } = default!;

    /// <summary>
    /// Whether or not the MudSelect should be clearable.
    /// </summary>
    /// <remarks>
    /// Setting this to true for an inheritor with a non-nullable <c>TValue</c> type will likely cause the website to
    /// crash.
    /// </remarks>
    [Parameter]
    public virtual bool Clearable { get; set; }

    /// <summary>
    /// The value being changed by the selector. Generally a field provided by <see cref="Context"/>.
    /// </summary>
    /// <example>
    /// <code>protected override Account Value => Context.Item.Account</code>
    /// </example>
    /// <remarks>
    /// If the implementation of MudDataGrid wasn't broken then binding this value would be enough to cause it to change
    /// when a different value is selected. Unfortunately it isn't so it has to be changed manually in <see cref="HandleValueChanged"/>.
    /// </remarks>
    protected abstract TValue? Value { get; }

    /// <summary>
    /// Handles the manual updating of the value being selected, implements any checks for selection validity, and
    /// must call the <c>CommittedItemChanges</c> method on the <see cref="DataGrid"/>.
    /// </summary>
    /// <param name="newValue">The new value that has been selected by the user.</param>
    /// <example>
    /// <code>
    /// protected override async Task HandleValueChanged(Account? newValue) {
    ///     Context.Item.Account = newValue ?? throw new ArgumentNullException(nameof(newValue));
    ///     await DataGrid.CommittedItemChanges.InvokeAsync(Context.Item);
    /// }
    /// </code>
    /// </example>
    protected abstract Task HandleValueChanged(TValue? newValue);

    /// <summary>
    /// Virtual method to allow for inheritors to override the generic ToString() representation of values in the
    /// selector.
    /// </summary>
    /// <param name="value">The value visible to the user in the selector.</param>
    /// <returns>A human-readable representation of the value that distinguishes it from other similar objects.</returns>
    /// <remarks>Rather than overriding this method, overriding ToString() in the DTO class is probably more efficient.</remarks>
    protected virtual string GetPrintValue(TValue? value)
    {
        return value?.ToString() ?? string.Empty;
    }
}