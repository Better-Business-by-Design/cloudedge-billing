using System.Linq.Expressions;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Shared;

public abstract class DataGridPage<T> : ComponentBase
{
    protected MudDataGrid<T> DataGrid = null!;
    public int TotalItems;
    
    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    protected virtual NavigationManager Navigation { get; set; } = default!;

    [Parameter] public bool ShowBreadcrumb { get; set; } = true;
    [Parameter] public bool StatusEditable { get; set; } = true;
    
    [Parameter]
    public EventCallback<int> OnGridServerReload { get; set; }
    
    [Parameter]
    public EventCallback<IQueryable<T>> StaticFilter { get; set; }

    
    protected async Task<GridData<T>> GridServerReload(GridState<T> state)
    {
        var fullQuery = BuildFullQuery();
        await StaticFilter.InvokeAsync(fullQuery);
        var filteredQuery = FilterFullQuery(fullQuery, state.FilterDefinitions);
        var orderedQuery = OrderFilteredQuery(filteredQuery, state.SortDefinitions);

        TotalItems = orderedQuery.Count();
        await OnGridServerReload.InvokeAsync(TotalItems);
        return new GridData<T>()
        {
            TotalItems = TotalItems,
            Items = await orderedQuery.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArrayAsync()
        };
    }

    protected abstract IQueryable<T> BuildFullQuery();

    protected abstract IQueryable<T> FilterFullQuery(IQueryable<T> fullQuery, IEnumerable<IFilterDefinition<T>> filterDefinitions);

    protected abstract IOrderedQueryable<T> OrderFilteredQuery(IQueryable<T> filteredQuery, IEnumerable<SortDefinition<T>> sortDefinitions);
    
    protected virtual void RowClicked(DataGridRowClickEventArgs<T> args) {}

    protected virtual string RowStyleFunc(T row, int i)
    {
        return string.Empty;
    }
    
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

    protected static Expression<Func<Enum, bool>> GenerateEnumLogicPredicate(string logicOperator, object value)
    {
        return logicOperator switch
        {
            "is" => property => property.Equals(value),
            "is not" => property => !property.Equals(value),
            _ => throw new NotImplementedException()
        };
    }

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

    protected static Expression<Func<DateTime, bool>> GenerateDateTimeLogicPredicate(string logicOperator, DateTime value)
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
    
}