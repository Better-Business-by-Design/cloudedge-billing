using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Index
{
    private MudTable<Document> _documentTable = null!;

    private int _documentTotal;
    private MudTable<Schedule> _scheduleTable = null!;
    private int _scheduleTotal;

    private string _table = "documents";

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    protected virtual NavigationManager Navigation { get; set; } = default!;

    private void SwitchTable(string table)
    {
        _table = table;

        StateHasChanged();
    }

    private async Task<Tuple<IQueryable<Document>, IQueryable<Schedule>>> ServerReload(TableState state)
    {
        var documentQueryable = DbContext.Documents
            .AsNoTracking()
            .Include(document => document.Farm)
            .Include(document => document.Plant)
            .Include(document => document.Status)
            .Include(document => document.SpeciesType)
            .Include(document => document.Plant.Meatwork);

        var filteredDocumentQueryable =
            documentQueryable.Where(document => document.StatusId == StatusId.Pending);

        var orderedDocumentQueryable = _table.Equals("documents")
            ? state.SortLabel switch
            {
                "date_field" => filteredDocumentQueryable.OrderByDirection(state.SortDirection,
                    document => document.DateProcessed),
                "killsheet_field" => filteredDocumentQueryable.OrderByDirection(state.SortDirection,
                    document => document.KillSheet),
                "farm_field" => filteredDocumentQueryable.OrderByDirection(state.SortDirection,
                    document => document.Farm.Name),
                "species_field" => filteredDocumentQueryable.OrderByDirection(state.SortDirection,
                    document => document.SpeciesTypeId),
                "stock_field" => filteredDocumentQueryable.OrderByDirection(state.SortDirection,
                    document => document.Animals.Count()),
                "plant_field" => filteredDocumentQueryable.OrderByDirection(state.SortDirection,
                    document => document.Plant.Name),
                "status_field" => filteredDocumentQueryable.OrderByDirection(state.SortDirection,
                    document => document.StatusId),
                "ats_field" => filteredDocumentQueryable.OrderByDirection(state.SortDirection,
                    document => document.TransitId),
                _ => filteredDocumentQueryable
            }
            : filteredDocumentQueryable;

        var scheduleQueryable = DbContext.Schedules
            .AsNoTracking()
            .Include(schedule => schedule.Status)
            .Include(schedule => schedule.Meatwork);

        var filteredScheduleQueryable =
            scheduleQueryable.Where(schedule => schedule.StatusId == StatusId.Pending);

        var orderedScheduleQueryable = _table.Equals("schedules")
            ? state.SortLabel switch
            {
                "start_field" => filteredScheduleQueryable.OrderByDirection(state.SortDirection,
                    schedule => schedule.StartDate),
                "end_field" =>
                    filteredScheduleQueryable.OrderByDirection(state.SortDirection, schedule => schedule.EndDate),
                "works_field" => filteredScheduleQueryable.OrderByDirection(state.SortDirection,
                    schedule => schedule.Meatwork.Name),
                "status_field" => filteredScheduleQueryable.OrderByDirection(state.SortDirection,
                    schedule => schedule.StatusId),
                _ => filteredScheduleQueryable
            }
            : filteredScheduleQueryable;

        _documentTotal = await orderedDocumentQueryable.CountAsync();
        _scheduleTotal = await orderedScheduleQueryable.CountAsync();

        return new Tuple<IQueryable<Document>, IQueryable<Schedule>>(
            orderedDocumentQueryable,
            orderedScheduleQueryable
        );
    }

    private async Task<TableData<Document>> DocumentServerReload(TableState state)
    {
        var reloadTuple = await ServerReload(state);

        return new TableData<Document>
        {
            TotalItems = _documentTotal,
            Items = await reloadTuple.Item1.Skip(state.Page * state.PageSize).Take(state.PageSize)
                .ToArrayAsync()
        };
    }

    private void DocumentRowClickEvent(TableRowClickEventArgs<Document> clickEvent)
    {
        Navigation.NavigateTo($"invoices/{clickEvent.Item.Id}");
    }

    private async Task<TableData<Schedule>> ScheduleServerReload(TableState state)
    {
        var reloadTuple = await ServerReload(state);

        return new TableData<Schedule>
        {
            TotalItems = _scheduleTotal,
            Items = await reloadTuple.Item2.Skip(state.Page * state.PageSize).Take(state.PageSize)
                .ToArrayAsync()
        };
    }

    private void ScheduleRowClickEvent(TableRowClickEventArgs<Schedule> clickEvent)
    {
        Navigation.NavigateTo($"pricing/{clickEvent.Item.Id}");
    }
}