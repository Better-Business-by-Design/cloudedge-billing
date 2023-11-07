using System.Linq.Expressions;
using AccountsReceivable.API.Shared;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Shared;

public abstract partial class EditableDataGridPage<T> : DataGridPage<T>
{

    protected HashSet<object> SelectedRows = new();
    protected bool ReadOnly = true;

    protected abstract List<BreadcrumbItem> Breadcrumb { get; set; }
    
    protected override void RowClicked(DataGridRowClickEventArgs<T> args)
    {
        if (ReadOnly) { ReadOnlyRowClicked(args); }
    }

    protected abstract void ReadOnlyRowClicked(DataGridRowClickEventArgs<T> args);

    protected virtual async Task AddRow()
    {
        var newDefaultRow = BuildNewDefaultRow();
        await DbContext.AddValues(new List<object> {newDefaultRow ?? throw new ArgumentNullException(nameof(newDefaultRow))});
        await DataGrid.ReloadServerData();
    }

    protected abstract T BuildNewDefaultRow();

    protected virtual async Task RemoveRows()
    {
        await DbContext.RemoveValues(SelectedRows);
        
        // Clear selection
        SelectedRows = new HashSet<object>();
        await DataGrid.ReloadServerData();
    }
    
    protected void SelectedRowsChanged(HashSet<T> rows)
    {
        SelectedRows = new HashSet<object>(rows.Cast<object>());
    }
    
    protected async Task CommittedRowChanges(T row)
    {
        await DbContext.EditValues(new List<object>() { row ?? throw new ArgumentNullException(nameof(row)) });
        await DataGrid.ReloadServerData();
    }
    
    
}
