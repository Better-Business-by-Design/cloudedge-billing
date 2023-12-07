using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CloudEdgeBilling.API.Shared.EditTemplates;

public abstract partial class DataRowEditor<TGrid, TValue> 
    where TGrid : IDataRow 
    where TValue : IDataRow
{
    [Parameter]
    [EditorRequired]
    public CellContext<TGrid> Context { get; set; } = default!;
    
    [Parameter]
    public IEnumerable<TValue> Values { get; set; } = default!;
    
    [CascadingParameter]
    public MudDataGrid<TGrid> DataGrid { get; set; } = default!;

    [Parameter]
    public virtual bool Clearable { get; set; } = false;

    protected abstract TValue? Value { get; }

    protected abstract Task HandleValueChanged(TValue? newValue);

    protected virtual string GetPrintValue(TValue? value)
    {
        return value?.ToString() ?? string.Empty;
    }
}