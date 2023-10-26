using System.Security.Authentication;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BAL.Extensions;
using AccountsReceivable.BL.Models.Account;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class PricingDetail
{
    private Schedule? _schedule;
    private string _searchString = string.Empty;
    private MudTable<Price> _table = null!;

    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Pricing Schedules", "pricing"),
        new BreadcrumbItem("Detail", null, true)
    };

    private int _totalItems;

    [CascadingParameter]
    private User? User { get; set; }

    [Parameter]
    public string? Id { get; set; }

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    protected virtual NavigationManager Navigation { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private async Task<TableData<Price>> ServerReload(TableState state)
    {
        if (_schedule == null)
        {
            ushort id;
            if (ushort.TryParse(Id, out id))
                _schedule = DbContext.Schedules
                    .Include(schedule => schedule.Status)
                    .Include(schedule => schedule.Uplifts)
                    .Include(schedule => schedule.Meatwork)
                    .FirstOrDefault(schedule => schedule.Id == id);

            if (_schedule == null)
            {
                Navigation.NavigateTo("pricing");

                return new TableData<Price>
                {
                    TotalItems = _totalItems,
                    Items = Array.Empty<Price>()
                };
            }         
            
            StateHasChanged();
        }

        var fullQuery = DbContext.Set<Price>()
            .AsNoTracking()
            .Include(pricing => pricing.Grade)
            .Include(pricing => pricing.Grade.AnimalType)
            .Include(pricing => pricing.Grade.AnimalType.SpeciesType)
            .Where(pricing => pricing.ScheduleId == _schedule!.Id);

        /*var filteredQuery = fullQuery.Where(schedule =>
            string.IsNullOrWhiteSpace(_searchString) ||
            EF.Functions.Like(schedule.Status.Name, $"%{_searchString}%") ||
            EF.Functions.Like(schedule.Meatwork.Name, $"%{_searchString}%")
        );*/

        /*var orderedQuery = state.SortLabel switch
        {
            "start_field" => fullQuery.OrderByDirection(state.SortDirection, schedule => schedule.StartDate),
            "end_field" => fullQuery.OrderByDirection(state.SortDirection, schedule => schedule.EndDate),
            "works_field" => fullQuery.OrderByDirection(state.SortDirection,
                schedule => schedule.Meatwork.Name),
            "status_field" => fullQuery.OrderByDirection(state.SortDirection,
                schedule => schedule.StatusId),
            _ => fullQuery
        };*/

        _totalItems = fullQuery.Count();

        return new TableData<Price>
        {
            TotalItems = _totalItems,
            Items = await fullQuery.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArrayAsync()
        };
    }

    /*private void OnSearch(string searchString)
    {
        _searchString = searchString;
        _table.ReloadServerData();
    }*/

    private async Task SetStatusApproved()
    {
        await _schedule!.ProcessDocuments(DbContext);
        await SetDocumentStatus(StatusId.Approved);
        
        Navigation.NavigateTo("pricing");
    }
    private async Task SetStatusDeclined()
    {
        await SetDocumentStatus(StatusId.Declined);
        
        Navigation.NavigateTo("pricing");
    }

    private async Task SetDocumentStatus(StatusId statusId)
    {
        _schedule!.StatusId = statusId;

        var audit = new Audit
        {
            UserId = User!.EmailAddress,
            Action = $"{StatusHelper.GetInfo(statusId).Name} Schedule [{_schedule.Id}]",
            //Comment = !string.IsNullOrWhiteSpace(comment) ? comment : null, // TODO
            Timestamp = DateTime.Now
        };
        await DbContext.AddAsync(audit);

        await DbContext.SaveChangesAsync();
    }
    

    
}