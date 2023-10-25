using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Pricing
{
    private string _searchString = string.Empty;
    private MudTable<Schedule> _table = null!;

    private int _totalItems;

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    protected virtual NavigationManager Navigation { get; set; } = default!;

    private async Task<TableData<Schedule>> ServerReload(TableState state)
    {
        var fullQuery = DbContext.Schedules
            .AsNoTracking()
            .Include(schedule => schedule.Status)
            .Include(schedule => schedule.Meatwork);

        var filteredQuery = fullQuery.Where(schedule =>
            string.IsNullOrWhiteSpace(_searchString) ||
            EF.Functions.Like(schedule.Status.Name, $"%{_searchString}%") ||
            EF.Functions.Like(schedule.Meatwork.Name, $"%{_searchString}%")
        );

        var orderedQuery = state.SortLabel switch
        {
            "start_field" => filteredQuery.OrderByDirection(state.SortDirection, schedule => schedule.StartDate),
            "end_field" => filteredQuery.OrderByDirection(state.SortDirection, schedule => schedule.EndDate),
            "works_field" => filteredQuery.OrderByDirection(state.SortDirection, schedule => schedule.Meatwork.Name),
            "status_field" => filteredQuery.OrderByDirection(state.SortDirection, schedule => schedule.StatusId),
            _ => filteredQuery
        };

        _totalItems = orderedQuery.Count();

        return new TableData<Schedule>
        {
            TotalItems = _totalItems,
            Items = await orderedQuery.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArrayAsync()
        };
    }

    private void OnSearch(string searchString)
    {
        _searchString = searchString;
        _table.ReloadServerData();
    }

    private void RowClickEvent(TableRowClickEventArgs<Schedule> clickEvent)
    {
        Navigation.NavigateTo($"pricing/{clickEvent.Item.Id}");
    }
}