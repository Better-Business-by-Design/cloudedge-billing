using AccountsReceivable.BL.Models.Application;
using MudBlazor;

namespace AccountsReceivable.API.Shared;

public abstract partial class EditableDataGridPage<T> : DataGridPage<T> where T : IDataRow
{

    protected HashSet<T> SelectedRows = new();
    protected bool ReadOnly = true;
    protected Stack<IDataRowChange> CompletedChanges = new();
    protected T? OriginalDataRow { get; set; }
    

    protected abstract List<BreadcrumbItem> Breadcrumb { get; set; }
    
    protected override void RowClicked(DataGridRowClickEventArgs<T> args)
    {
        if (ReadOnly) { ReadOnlyRowClicked(args); }
    }

    protected abstract void ReadOnlyRowClicked(DataGridRowClickEventArgs<T> args);

    protected virtual async Task AddRow()
    {
        var change = new AddDataRowChange(BuildNewDefaultRow());
        await change.ApplyChange(DbContext);
        CompletedChanges.Push(change);
        await DataGrid.ReloadServerData();
    }

    protected abstract IDataRow BuildNewDefaultRow();

    protected virtual async Task RemoveRows()
    {
        var change = new RemoveDataRowsChange(SelectedRows.Cast<IDataRow>());
        await change.ApplyChange(DbContext);
        
        CompletedChanges.Push(change);
        SelectedRows.Clear();
        await DataGrid.ReloadServerData();
    }
    
    protected async Task CommittedRowChanges(T row)
    {
        var change = new EditDataRowChange()
        {
            OriginalDataRow = OriginalDataRow ?? throw new ArgumentNullException(nameof(OriginalDataRow),"Applying changes to row when original is null"),
            CurrentDataRow = row
        };

        await change.ApplyChange(DbContext);
        CompletedChanges.Push(change);
        OriginalDataRow = default;
        await DataGrid.ReloadServerData();
    }

    protected async Task UndoLastRowChange()
    {
        if (CompletedChanges.TryPop(out var result))
        {
            await result.RevertChange(DbContext);
            await DataGrid.ReloadServerData();
        }
    }
    
    protected void SelectedRowsChanged(HashSet<T> rows)
    {
        Console.WriteLine($"Selected Rows Changed, Now: {string.Join(",", rows)}");
    }
    
    protected void StartedEditingRow(T row) { OriginalDataRow = row; }

    protected void CanceledEditingRow(T row) { OriginalDataRow = default; } 
    
    
    
}
