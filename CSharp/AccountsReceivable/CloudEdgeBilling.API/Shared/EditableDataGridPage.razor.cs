﻿using System.Collections.Immutable;
using System.Text.Json;
using CloudEdgeBilling.API.Shared.DataRowChange;
using CloudEdgeBilling.API.Shared.FluidValidation;
using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MudBlazor;

namespace CloudEdgeBilling.API.Shared;

/// <summary>
///     Abstract class <c>EditableDataGridPage</c> represents a page/component using a MudDataGrid to present,
///     and allow editing of, a database table.
/// </summary>
/// <remarks>
///     Contains functionality for adding rows, removing rows, editing rows, and undoing changes.
/// </remarks>
/// <typeparam name="T">
///     The type representing a single row in the table being shown in the MudDataGrid.
///     Must extend IDataRow and be stored in a DbSet in the DBContext.
/// </typeparam>
public abstract partial class EditableDataGridPage<T> : DataGridPage<T> where T : class, IDataRow
{
    // Stack of all changes that have been submitted so far in this session, only used if Revertable is true.
    protected readonly Stack<IDataRowChange> CompletedChanges = new();

    // String combining all error messages generated by the validator.
    protected string ErrorMessage = string.Empty;
    
    /// <summary>
    /// Stores the current value of the Read Only &lt;-&gt; Edit Mode toggle, only used if Editable is true.
    /// </summary>
    protected bool ReadOnly { get; set; } = true;

    /// <value>
    ///     Property <c>Editable</c> dictates whether or not the EditableDataGridPage should show the controls used to edit
    ///     the underlying database table.
    /// </value>
    [Parameter]
    public bool Editable { get; set; } = true;

    [Parameter] public virtual bool Insertable { get; set; } = true;
    [Parameter] public virtual bool Removable { get; set; } = true;

    /// <value>
    ///     Property <c>Revertable</c> dictates whether or not the EditableDataGrid should show the control used to revert
    ///     previously completed changes.
    /// </value>
    /// <remarks>
    ///     Due to this functionality being poorly implemented earlier versions of EFCore (including the latest version
    ///     supported by UIPath) aren't able to present it.
    /// </remarks>
    [Parameter]
    public virtual bool Revertable { get; set; } = true;


    /// <value>
    ///     EventCallback <c>OnRowAdded</c> provides access for parent objects to run callback methods when a new record is
    ///     added to the underlying database table.
    /// </value>
    [Parameter]
    public EventCallback<IDataRow> OnRowAdded { get; set; }

    /// <value>
    ///     Property <c>Validator</c> provides validation functionality for use in adding and editing row values.
    ///     Validator rules should mirror (or extend) all database table rules to ensure SQLExceptions are avoided.
    /// </value>
    protected DataRowFluentValidator<T> Validator { get; set; } = default!;

    [Inject] protected IDialogService DialogService { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var result = await ProtectedSessionStore.GetAsync<bool>(nameof(ReadOnly));
            //Console.WriteLine($"Retrieval of ReadOnly completed successfully? {result.Success}\nRetrieved {(result.Success ? result.Value.ToString(): string.Empty)}");
            ReadOnly = result.Success ? result.Value : ReadOnly;
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    /// <summary>
    ///     Abstract task <c>OnAddButtonClicked</c> provides functionality for inheritors to perform tasks when the inbuilt
    ///     Add Row button is clicked.
    /// </summary>
    /// <remarks>
    ///     Any inheritors using always visible <c>NewDataRowForm</c> should throw an error for this method as they should
    ///     be using the buttons in the form, not the default ones presented by <c>EditableDataGridPage</c>. Inheritors with
    ///     hidden forms should use this to open the form.
    /// </remarks>
    protected abstract Task OnAddButtonClicked();

    /// <summary>
    ///     Sealed method <c>RowClicked</c> overrides the abstract method inherited from <c>DataGridPage</c>. This sealed
    ///     override redirects the functionality previously exposed by <c>RowClicked</c> to <c>ReadOnlyRowClicked</c>.
    ///     This is to allow for the MudDataGrid's Cell edit style to interact with row clicks.
    /// </summary>
    /// <seealso cref="ReadOnlyRowClicked" />
    /// <param name="args">The description of the row click that caused this event to fire.</param>
    protected sealed override void RowClicked(DataGridRowClickEventArgs<T> args)
    {
        if (ReadOnly) ReadOnlyRowClicked(args);
    }

    /// <summary>
    ///     Abstract method <c>ReadOnlyRowClicked</c> provides functionality for inheritors to perform tasks when rows are
    ///     clicked while the component is in <c>ReadOnly</c> mode.
    /// </summary>
    /// <seealso cref="RowClicked" />
    /// <param name="args">The description of the row click that caused this event to fire.</param>
    protected abstract void ReadOnlyRowClicked(DataGridRowClickEventArgs<T> args);

    /// <summary>
    ///     Virtual task <c>AddRow</c> provides a hook method for asynchronously adding new rows to the underlying table.
    /// </summary>
    /// <remarks>
    ///     If the inheritor implements some form of state based new row data generation then they can pass their own rows
    ///     to this method by overriding it. If no specific values need to be set then this task will simply add a default
    ///     row.
    /// </remarks>
    /// <param name="row">Optional row with prefilled values if the default values aren't sufficient.</param>
    protected virtual async Task AddRow(IDataRow? row)
    {
        var dbContext = await DbContextFactory.CreateDbContextAsync();
        row ??= BuildNewDefaultRow();
        Console.WriteLine($"Row added: {row}");
        var change = new AddDataRowChange(row, dbContext);
        await change.ApplyChange();
        CompletedChanges.Push(change);
        await DataGrid!.ReloadServerData();
    }

    /// <summary>
    ///     Abstract method <c>BuildNewDefaultRow</c> ensures inheritors present a method for generating valid default
    ///     rows.
    /// </summary>
    /// <returns>A new row with valid default values.</returns>
    protected abstract T BuildNewDefaultRow();

    /// <summary>
    ///     Virtual task <c>RemoveRows</c> provides a hook method for asynchronously removing selected rows from the
    ///     underlying database table.
    /// </summary>
    protected virtual async Task RemoveRows()
    {
        var dbContext = await DbContextFactory.CreateDbContextAsync();
        Console.WriteLine(
            $"Rows removed:\n {string.Join("\n", DataGrid!.SelectedItems.Select(row => JsonSerializer.Serialize(row)))}");
        var change = new RemoveDataRowsChange(DataGrid.SelectedItems.ToImmutableList(), dbContext);
        await change.ApplyChange();
        CompletedChanges.Push(change);
        DataGrid.SelectedItems.Clear();
        await DataGrid.ReloadServerData();
    }

    /// <summary>
    ///     Virtual task <c>CommittedRowChanges</c> provides a hook method for the callback that fires when a row edit is
    ///     completed.
    /// </summary>
    /// <remarks>
    ///     Fluent validation is very nicely implemented in <c>AddNewDataRowForm</c> but has to be hacked together here to
    ///     provide similar error presentation functionality. If MudDataGrid was updated to accept a validator this would be
    ///     much cleaner.
    /// </remarks>
    /// <param name="row">The row being changed.</param>
    /// <exception cref="ArgumentNullException">Thrown if the row being changed comes through as null.</exception>
    protected virtual async Task CommittedRowChanges(T row)
    {
        if (row is null) throw new ArgumentNullException(nameof(row), "Applying changes to row when it is null");

        var result = await Validator.ValidateAsync(row);
        if (result.IsValid)
        {
            var dbContext = await DbContextFactory.CreateDbContextAsync();
            Console.WriteLine($"Row edit committed: {JsonSerializer.Serialize(row)}");
            var entry = GetEntityEntry(row, dbContext);
            entry.State = EntityState.Modified;

            var change = new EditDataRowChange
            (
                (IDataRow)(await entry.GetDatabaseValuesAsync()).Clone().ToObject(),
                row,
                dbContext
            );
            await change.ApplyChange();
            CompletedChanges.Push(change);
            await DataGrid!.ReloadServerData();
        }
        else
        {
            Console.WriteLine($"Row edit failed!: {JsonSerializer.Serialize(row)}");
            ErrorMessage = string.Join(" ", result.Errors);
        }
    }

    /// <summary>
    ///     Task <c>UndoLastRowChange</c> implements the functionality for reverting previously committed (during the
    ///     current session) database changes.
    /// </summary>
    /// <remarks>
    ///     Due to issues with the poor implementation of reverting edited rows (as compared to reverting row
    ///     addition/removal), this method has to be disabled on earlier versions of EFCore (including the latest version
    ///     compatible with UIPath).
    ///     Further work could also be done to store changes in session storage so that the changes are not lost whenever
    ///     the page is refreshed.
    /// </remarks>
    /// <seealso cref="Revertable" />
    protected async Task UndoLastRowChange()
    {
        if (!Revertable) return;

        if (CompletedChanges.TryPop(out var result))
        {
            Console.WriteLine($"Reverting last change: {result}");
            await result.RevertChange();
            await DataGrid!.ReloadServerData();
        }
    }

    /// <summary>
    ///     Virtual method <c>SelectedRowsChanges</c> provides a hook method for the event that fires when the user changes
    ///     the current row selection.
    /// </summary>
    /// <param name="rows">The set of currently selected rows.</param>
    // ReSharper disable once VirtualMemberNeverOverridden.Global
    protected virtual void SelectedRowsChanged(HashSet<T> rows)
    {
        Console.WriteLine($"Selected Rows Changed, Now: {string.Join(",", rows)}");
    }

    /// <summary>
    ///     Method <c>GetEntityEntry</c> is a hack to get around the EFCore issue where the first time you access an
    ///     <c>EntityEntry</c> it throws an <c>InvalidOperationException</c>. The method simply catches the first exception,
    ///     logs it, and then attempts to return the second attempt at access.
    /// </summary>
    /// <param name="row">The row to get the entity entry of.</param>
    /// <returns>The entity entry that corresponds to the inputted row.</returns>
    /// <exception cref="InvalidOperationException">
    ///     Thrown when the expected InvalidOperationException gets thrown more than
    ///     once.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///     Thrown when access fails without throwing the expected
    ///     InvalidOperationException.
    /// </exception>
    private EntityEntry<T> GetEntityEntry(T row, ApplicationDbContext dbContext)
    {
        EntityEntry<T>? entityEntry = null;
        InvalidOperationException? invalidOperationException = null;
        for (var i = 0; i < 2; i++)
            try
            {
                entityEntry = dbContext.Entry(row);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"Received InvalidOperationException ({e.Message}), trying again.");
                invalidOperationException = e;
            }

        if (entityEntry is null)
        {
            if (invalidOperationException is not null)
                throw invalidOperationException;
            throw new ArgumentNullException(nameof(invalidOperationException),
                "Attempting to access entity entry failed without throwing an InvalidOperationException??");
        }

        return entityEntry;
    }

    [Inject] public required ProtectedSessionStorage ProtectedSessionStore { get; set; }
    
    private async Task EditModeChanged(bool? isReadOnly)
    {
        ReadOnly = isReadOnly ?? throw new ArgumentNullException(nameof(isReadOnly));
        Console.WriteLine($"Saving Read Only: {isReadOnly}");
        await ProtectedSessionStore.SetAsync(nameof(ReadOnly), ReadOnly);
    } 
}