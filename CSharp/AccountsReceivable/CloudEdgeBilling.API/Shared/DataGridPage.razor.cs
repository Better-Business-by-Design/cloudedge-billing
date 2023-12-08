using System.Linq.Expressions;
using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace CloudEdgeBilling.API.Shared;

/// <summary>
///     Abstract class <c>DataGridPage</c> represents a page/component using a MudDataGrid to present a database table.
/// </summary>
/// <remarks>
///     Contains functionality for sorting, filtering, and row click handling.
/// </remarks>
/// <typeparam name="T">
///     The type representing a single row in the table being shown in the MudDataGrid.
///     Must extend IDataRow and should be stored in a DbSet in the DBContext.
/// </typeparam>
public abstract partial class DataGridPage<T> : ComponentBase where T : IDataRow
{
    protected abstract string StateKey { get; }

    protected DataGridPageState<T>? PageState;
    
    protected MudDataGrid<T>? DataGrid;

    /// <value>
    ///     Property <c>ShowBreadcrumb</c> dictates whether or not the Breadcrumb should be visible, defaults to true.
    /// </value>
    [Parameter]
    public bool ShowBreadcrumb { get; set; } = true;

    /// <value>
    ///     Property <c>StatusInteractable</c> dictates whether or not the status column in the table can be used for sorting
    ///     or filtering, defaults to true.
    ///     Implementation of this property, including deciding which column is the status column, is left to inheritors.
    /// </value>
    [Parameter]
    public bool StatusInteractable { get; set; } = true;

    /// <value>
    ///     Property <c>Visible</c> dictates whether or not the MudDataGrid (and Breadcrumb) should be visible, defaults to
    ///     true.
    ///     Implementation of this property, on the MudDataGrid, is left to inheritors.
    /// </value>
    [Parameter]
    public bool Visible { get; set; } = true;

    /// <value>
    ///     Abstract property <c>Breadcrumb</c> contains the list of <c>BreadCrumbItem</c> objects that will be rendered in
    ///     the page's Breadcrumb render fragment.
    /// </value>
    protected abstract List<BreadcrumbItem> Breadcrumb { get; set; }

    /// <value>
    ///     EventCallback <c>OnGridServerReload</c> provides access for parent objects to run callback methods when the
    ///     MudDataGrid
    ///     reloads its contents from the database table it represents. Includes the total number of rows in the MudDataGrid
    ///     as a parameter.
    /// </value>
    [Parameter]
    public EventCallback<int> OnGridServerReload { get; set; }

    /// <value>
    ///     Nullable property <c>StaticFilter</c> provides functionality for enforcing a filter on a table so that only a
    ///     section of the database table is displayed.
    /// </value>
    [Parameter]
    public Func<IQueryable<T>, IQueryable<T>>? StaticFilter { get; set; }

    [Inject] protected virtual IDbContextFactory<ApplicationDbContext> DbContextFactory { get; set; } = default!;

    [Inject] protected virtual NavigationManager Navigation { get; set; } = default!;

    [Inject] protected virtual ProtectedSessionStorage ProtectedSessionStore { get; set; } = default!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var result = await ProtectedSessionStore.GetAsync<DataGridPageState<T>>(StateKey);
            if (result.Success)
            {
                PageState = result.Value;
                Console.WriteLine($"Read page state {PageState}");
                if (PageState!.SimplifiedFilterDefinitions.Any())
                {
                    await DataGrid!.ClearFiltersAsync();
                    foreach (var simplifiedFilterDefinition in PageState!.SimplifiedFilterDefinitions)
                    {
                        Console.WriteLine($"Added Filter: {simplifiedFilterDefinition}");
                        await DataGrid!.AddFilterAsync(simplifiedFilterDefinition.GetFullFilterDefinition(DataGrid));
                    }
                    DataGrid!.ToggleFiltersMenu();
                }
                
                foreach (var sortDefinition in PageState!.SimplifiedSortDefinitions)
                {
                    await DataGrid!.ExtendSortAsync(
                        sortDefinition.SortBy,
                        sortDefinition.Descending ? SortDirection.Descending : SortDirection.Ascending,
                        null);
                }
            }
        }
        await base.OnAfterRenderAsync(firstRender);
    }

    /// <summary>
    ///     Implementation of MudDataGrid's <c>ServerData</c> property.
    ///     Calls hook methods to load data from the database, filter it, and sort it.
    /// </summary>
    /// <seealso cref="BuildFullQuery" />
    /// <seealso cref="FilterFullQuery" />
    /// <seealso cref="OrderFilteredQuery" />
    /// <remarks>
    ///     Calls both optional parameter callbacks. <see cref="StaticFilter" />, <see cref="OnGridServerReload" />
    /// </remarks>
    /// <param name="state">The current state of the MudDataGrid.</param>
    /// <returns>A Task for the MudDataGrid to use to update its state.</returns>
    protected async Task<GridData<T>> GridServerReload(GridState<T> state)
    {
        var pageState = new DataGridPageState<T>(
            state.SortDefinitions.Select(SimplifiedSortDefinition<T>.Simplify),
            state.FilterDefinitions.Select(SimplifiedFilterDefinition<T>.Simplify)
        );
        if (!pageState.IsEmpty() && !pageState.Equals(PageState))
        {
            Console.WriteLine($"Saving page state {pageState}");
            PageState = pageState;
            await ProtectedSessionStore.SetAsync(StateKey, pageState);
        }

        await using var dbContext = await DbContextFactory.CreateDbContextAsync(); 
        var fullQuery = BuildFullQuery(dbContext);
        var initialFilteredQuery = StaticFilter?.Invoke(fullQuery) ?? fullQuery;
        var filteredQuery = FilterFullQuery(initialFilteredQuery, state.FilterDefinitions);
        var orderedQuery = OrderFilteredQuery(filteredQuery, state.SortDefinitions);

        var totalItems = await orderedQuery.CountAsync();
        await OnGridServerReload.InvokeAsync(totalItems);
        return new GridData<T>
        {
            TotalItems = totalItems,
            Items = await orderedQuery.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArrayAsync()
        };
    }

    /// <summary>
    ///     Abstract method <c>BuildFullQuery</c> returns the contents of the related database table as an <c>IQueryable</c> of
    ///     <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">The type representing a single row in the related database table.</typeparam>
    protected abstract IQueryable<T> BuildFullQuery(ApplicationDbContext dbContext);

    /// <summary>
    ///     Abstract method <c>FilterFullQuery</c> returns the contents of the related database table after the currently
    ///     active filters on the MudDataGrid have been applied to it.
    /// </summary>
    /// <remarks>
    ///     MudDataGrid has an inbuilt implementation for its filters but it doesn't work when you're using the
    ///     <c>ServerData</c> property to feed it data straight from a database.
    /// </remarks>
    /// <param name="fullQuery">The full contents (after static filters have been applied) of the related database table</param>
    /// <param name="filterDefinitions">The collection of currently active filters applied to the MudDataGrid</param>
    /// <typeparam name="T">The type representing a single row in the related database table.</typeparam>
    protected abstract IQueryable<T> FilterFullQuery(IQueryable<T> fullQuery,
        IEnumerable<IFilterDefinition<T>> filterDefinitions);

    /// <summary>
    ///     Abstract method <c>OrderFilteredQuery</c> returns the ordered contents of the related database table after the
    ///     currently active sorts on the MudDataGrid have been applied to it.
    /// </summary>
    /// <remarks>
    ///     MudDataGrid has an inbuilt implementation for its sorting but it doesn't work when you're using the
    ///     <c>ServerData</c> property to feed it data straight from a database.
    /// </remarks>
    /// <param name="filteredQuery">The filtered contents of the related database table</param>
    /// <param name="sortDefinitions">The ordered collection of currently active sorts applied to the MudDataGrid</param>
    /// <typeparam name="T">The type representing a single row in the related database table.</typeparam>
    protected abstract IOrderedQueryable<T> OrderFilteredQuery(IQueryable<T> filteredQuery,
        IEnumerable<SortDefinition<T>> sortDefinitions);

    /// <summary>
    ///     Virtual method <c>RowClicked</c> provides functionality for inheritors to perform tasks when rows are clicked by
    ///     the user.
    /// </summary>
    /// <remarks>
    ///     Connecting this method to the MudDataGrid's <c>RowClicked</c> property is left to inheritors.
    /// </remarks>
    /// <param name="args">The description of the row click that caused this event to fire.</param>
    protected virtual void RowClicked(DataGridRowClickEventArgs<T> args)
    {
    }

    /// <summary>
    ///     Virtual method <c>RowStyleFunc</c> provides functionality for inheritors to include custom styling based on row
    ///     values.
    /// </summary>
    /// <remarks>
    ///     Returning an incorrectly formatted css property will simply cause the value to be ignored.
    /// </remarks>
    /// <param name="row">The row being styled.</param>
    /// <param name="i">The index of the row in the MudDataGrid.</param>
    /// <returns>A string detailing a css property.</returns>
    protected virtual string RowStyleFunc(T row, int i)
    {
        return string.Empty;
    }

    /// <summary>
    ///     Static method <c>GenerateStringLogicPredicate</c> is our custom implementation of all the filters MudDataGrid
    ///     exposes for string columns.
    /// </summary>
    /// <param name="logicOperator">The name of the filter to be implemented.</param>
    /// <param name="value">The string value being subjected to the filter implementation.</param>
    /// <returns>
    ///     A lambda expression which returns a boolean detailing whether the string value does or doesn't pass the
    ///     filter.
    /// </returns>
    /// <exception cref="NotImplementedException">
    ///     Thrown when a requested filter's name has no matching custom implementation
    ///     specified.
    /// </exception>
    protected static Expression<Func<string, bool>> GenerateStringLogicPredicate(string logicOperator, string value)
    {
        return logicOperator switch
        {
            "contains" => property => property.Contains(value),
            "not contains" => property => !property.Contains(value),
            "equals" => property => property.Equals(value),
            "not equals" => property => !property.Equals(value),
            "starts with" => property => property.StartsWith(value),
            "ends with" => property => property.EndsWith(value),
            "is empty" => property => property.Equals(string.Empty),
            "is not empty" => property => !property.Equals(string.Empty),
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    ///     Static method <c>GenerateEnumLogicPredicate</c> is our custom implementation of all the filters MudDataGrid
    ///     exposes for enum columns.
    /// </summary>
    /// <param name="logicOperator">The name of the filter to be implemented.</param>
    /// <param name="value">The enum value being subjected to the filter implementation.</param>
    /// <returns>A lambda expression which returns a boolean detailing whether the enum value does or doesn't pass the filter.</returns>
    /// <exception cref="NotImplementedException">
    ///     Thrown when a requested filter's name has no matching custom implementation
    ///     specified.
    /// </exception>
    protected static Expression<Func<Enum, bool>> GenerateEnumLogicPredicate(string logicOperator, object value)
    {
        return logicOperator switch
        {
            "is" => property => property.Equals(value),
            "is not" => property => !property.Equals(value),
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    ///     Static method <c>GenerateDecimalLogicPredicate</c> is our custom implementation of all the filters MudDataGrid
    ///     exposes for number columns.
    /// </summary>
    /// <param name="logicOperator">The name of the filter to be implemented.</param>
    /// <param name="value">The decimal value being subjected to the filter implementation.</param>
    /// <returns>
    ///     A lambda expression which returns a boolean detailing whether the decimal value does or doesn't pass the
    ///     filter.
    /// </returns>
    /// <exception cref="NotImplementedException">
    ///     Thrown when a requested filter's name has no matching custom implementation
    ///     specified.
    /// </exception>
    protected static Expression<Func<decimal, bool>> GenerateDecimalLogicPredicate(string logicOperator, decimal value)
    {
        return logicOperator switch
        {
            "=" => property => property == value,
            "!=" => property => property != value,
            ">" => property => property > value,
            ">=" => property => property >= value,
            "<" => property => property < value,
            "<=" => property => property <= value,
            "is empty" => property => false, // TODO... find better way to implement
            "is not empty" => property => true, // TODO... find better way to implement
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    ///     Static method <c>GenerateDateTimeLogicPredicate</c> is our custom implementation of all the filters MudDataGrid
    ///     exposes for DateTime columns.
    /// </summary>
    /// <param name="logicOperator">The name of the filter to be implemented.</param>
    /// <param name="value">The DateTime value being subjected to the filter implementation.</param>
    /// <returns>
    ///     A lambda expression which returns a boolean detailing whether the DateTime value does or doesn't pass the
    ///     filter.
    /// </returns>
    /// <exception cref="NotImplementedException">
    ///     Thrown when a requested filter's name has no matching custom implementation
    ///     specified.
    /// </exception>
    protected static Expression<Func<DateTime, bool>> GenerateDateTimeLogicPredicate(string logicOperator,
        DateTime value)
    {
        return logicOperator switch
        {
            "is" => property => property.Equals(value),
            "is not" => property => !property.Equals(value),
            "is after" => property => property.CompareTo(value) > 0,
            "is on or after" => property => property.CompareTo(value) >= 0,
            "is before" => property => property.CompareTo(value) < 0,
            "is on or before" => property => property.CompareTo(value) <= 0,
            "is empty" => property => property.CompareTo(new DateTime()) == 0,
            "is not empty" => property => property.CompareTo(new DateTime()) != 0,
            _ => throw new NotImplementedException()
        };
    }

    /// <summary>
    ///     Static method <c>GenerateBooleanLogicPredicate</c> is our custom implementation of all the filters MudDataGrid
    ///     exposes for boolean columns.
    /// </summary>
    /// <param name="logicOperator">The name of the filter to be implemented.</param>
    /// <param name="value">The boolean value being subjected to the filter implementation.</param>
    /// <returns>
    ///     A lambda expression which returns a boolean detailing whether the boolean value does or doesn't pass the
    ///     filter.
    /// </returns>
    /// <exception cref="NotImplementedException">
    ///     Thrown when a requested filter's name has no matching custom implementation
    ///     specified.
    /// </exception>
    protected static Expression<Func<bool, bool>> GenerateBooleanLogicPredicate(string logicOperator, bool value)
    {
        return logicOperator switch
        {
            "is" => property => property == value,
            _ => throw new NotImplementedException(
                $"'{logicOperator}' is not implemented in Boolean logic predicate building.")
        };
    }

    /// <summary>
    ///     Static method <c>GenerateGuidLogicPredicate</c> is our custom implementation of all the filters MudDataGrid
    ///     exposes for Guid columns.
    /// </summary>
    /// <param name="logicOperator">The name of the filter to be implemented.</param>
    /// <param name="value">The Guid value being subjected to the filter implementation.</param>
    /// <returns>A lambda expression which returns a boolean detailing whether the Guid value does or doesn't pass the filter.</returns>
    /// <exception cref="NotImplementedException">
    ///     Thrown when a requested filter's name has no matching custom implementation
    ///     specified.
    /// </exception>
    protected static Expression<Func<Guid, bool>> GenerateGuidLogicPredicate(string logicOperator, Guid value)
    {
        return logicOperator switch
        {
            "is" => property => property.Equals(value),
            "is not" => property => !property.Equals(value),
            "is after" => property => property.CompareTo(value) > 0,
            "is before" => property => property.CompareTo(value) < 0,
            _ => throw new NotImplementedException(
                $"'{logicOperator}' is not implemented in Guid logic predicate building.")
        };
    }
}

public record DataGridPageState<T>(
    IEnumerable<SimplifiedSortDefinition<T>> SimplifiedSortDefinitions,
    IEnumerable<SimplifiedFilterDefinition<T>> SimplifiedFilterDefinitions) where T : IDataRow
{
    public bool IsEmpty()
    {
        return !SimplifiedSortDefinitions.Any() && !SimplifiedFilterDefinitions.Any();
    }

    public override string ToString()
    {
        return $"SimplifiedSortDefinitions: {string.Join(", ", SimplifiedSortDefinitions)} SimplifiedFilterDefinitions: {string.Join(",", SimplifiedFilterDefinitions)}";
    }

    public virtual bool Equals(DataGridPageState<T>? other)
    {
        if (other is null) return false;
        if (SimplifiedSortDefinitions.Count() != other.SimplifiedSortDefinitions.Count()) return false;
        if (SimplifiedFilterDefinitions.Count() != other.SimplifiedFilterDefinitions.Count()) return false;
        if (SimplifiedSortDefinitions.Zip(other.SimplifiedSortDefinitions, (a, b) => new { a, b }).Any(item => item.a != item.b))
        {
            return false;
        }
        if (SimplifiedFilterDefinitions.Zip(other.SimplifiedFilterDefinitions, (a, b) => new { a, b }).Any(item => item.a != item.b))
        {
            return false;
        }

        return true;
    }
}

public record SimplifiedSortDefinition<T> (
    string SortBy,
    bool Descending
    ) where T : IDataRow
{
    public static SimplifiedSortDefinition<T> Simplify(SortDefinition<T> sortDefinition) => new(
        sortDefinition.SortBy,
        sortDefinition.Descending
    );
}

public record SimplifiedFilterDefinition<T> (
    string Title,
    SimplifiedFieldType SimplifiedFieldType,
    string Operator,
    object? Value
    ) where T : IDataRow
{
    public static SimplifiedFilterDefinition<T> Simplify(IFilterDefinition<T> filterDefinition) => new(
        filterDefinition.Title ?? throw new ArgumentNullException(nameof(filterDefinition), "Unable to save filter definition with no title."),
        SimplifiedFieldType.Simplify(filterDefinition.FieldType), 
        filterDefinition.Operator ?? throw new ArgumentNullException(nameof(filterDefinition), "Unable to save filter definition with no operator."),
        filterDefinition.Value
    );

    public IFilterDefinition<T> GetFullFilterDefinition(MudDataGrid<T> dataGrid)
    {

        return new FilterDefinition<T>()
        {
            Id = new Guid(),
            Title = Title,
            Column = dataGrid.RenderedColumns.Find(c => c.Title?.Equals(Title) ?? false),
            Operator = Operator,
            Value = Value
        };
    }
}

public record SimplifiedFieldType (
    string TypeName)
{
    public static SimplifiedFieldType Simplify(FieldType fieldType)
    {
        return new SimplifiedFieldType(fieldType.InnerType!.FullName!);
    }

    public FieldType GetFieldType()
    {
        return FieldType.Identify(Type.GetType(TypeName));
    }
}