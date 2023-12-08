using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CloudEdgeBilling.API.Shared.EditTemplates.Toggle;

public partial class IsActiveEditor
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
    public CellContext<Customer> Context { get; set; } = default!;
    
    /// <summary>
    /// The <c>DataGrid</c> this class is being used as an edit selector in.
    /// </summary>
    /// <remarks>
    /// Cascading parameter is passed implicitly by the <c>EditTemplate</c> tag this class expects to be placed inside of.
    /// </remarks>
    [CascadingParameter]
    public MudDataGrid<Customer> DataGrid { get; set; } = default!;
    
    protected bool? IsActive => Context.Item.IsActive;
    
    protected async Task HandleValueChanged(bool? newValue)
    {
        Context.Item.IsActive = newValue ?? throw new ArgumentNullException(nameof(newValue));
        await DataGrid.CommittedItemChanges.InvokeAsync(Context.Item);
    }
}