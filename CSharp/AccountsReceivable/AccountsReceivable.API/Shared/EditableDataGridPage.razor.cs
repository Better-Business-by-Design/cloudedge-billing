using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MudBlazor;
using Newtonsoft.Json;

namespace AccountsReceivable.API.Shared;

public abstract partial class EditableDataGridPage<T> : DataGridPage<T> where T : class, IDataRow
{

    [Parameter] public bool Editable { get; set; } = true;
    
    protected HashSet<T> SelectedRows = new();
    protected bool ReadOnly = true;
    protected Stack<IDataRowChange> CompletedChanges = new();

    protected abstract List<BreadcrumbItem> Breadcrumb { get; set; }
    
    protected override void RowClicked(DataGridRowClickEventArgs<T> args)
    {
        if (ReadOnly) { ReadOnlyRowClicked(args); }
    }

    protected abstract void ReadOnlyRowClicked(DataGridRowClickEventArgs<T> args);

    protected virtual async Task AddRow()
    {
        var row = BuildNewDefaultRow();
        Console.WriteLine($"Row added: {System.Text.Json.JsonSerializer.Serialize(row)}");
        var change = new AddDataRowChange(row);
        await change.ApplyChange(DbContext);
        CompletedChanges.Push(change);
        await DataGrid.ReloadServerData();
    }

    protected abstract IDataRow BuildNewDefaultRow();

    protected virtual async Task RemoveRows()
    {
        Console.WriteLine($"Rows removed:\n {string.Join("\n",SelectedRows.Select(row => System.Text.Json.JsonSerializer.Serialize(row)))}");
        var change = new RemoveDataRowsChange(SelectedRows.Cast<IDataRow>());
        await change.ApplyChange(DbContext);
        
        CompletedChanges.Push(change);
        SelectedRows.Clear();
        await DataGrid.ReloadServerData();
    }
    
    protected async Task CommittedRowChanges(T row)
    {
        if (row is null) throw new ArgumentNullException(nameof(row), "Applying changes to row when it is null");
        Console.WriteLine($"Row edit committed: {System.Text.Json.JsonSerializer.Serialize(row)}");
        var entry = GetEntityEntry(row);
        
        var change = new EditDataRowChange()
        {
            OriginalDataRow = (IDataRow) entry.OriginalValues.Clone().ToObject(),
            DataRow = row,
        };

        await change.ApplyChange(DbContext);
        CompletedChanges.Push(change);
        await DataGrid.ReloadServerData();
    }

    protected async Task UndoLastRowChange()
    {
        if (CompletedChanges.TryPop(out var result))
        {
            Console.WriteLine($"Reverting last change: {result}");
            await result.RevertChange(DbContext);
            await DataGrid.ReloadServerData();
        }
    }
    
    protected void SelectedRowsChanged(HashSet<T> rows)
    {
        Console.WriteLine($"Selected Rows Changed, Now: {string.Join(",", rows)}");
    }

    private EntityEntry<T> GetEntityEntry(T row)
    {
        EntityEntry<T>? entityEntry = null;
        InvalidOperationException? invalidOperationException = null;
        for (var i = 0; i < 2; i++)
        {
            try
            {
                entityEntry = DbContext.Entry(row);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Received InvalidOperationException ({e.Message}), trying again.");
                invalidOperationException = e;
            }
        }

        if (entityEntry is null)
        {
            if (invalidOperationException is not null)
            {
                throw invalidOperationException;
            }
            else
            {
                throw new ArgumentNullException(nameof(invalidOperationException),
                    "Attempting to access entity entry failed without throwing an InvalidOperationException??");
            }
        }

        return entityEntry;
    }
    
    
}
