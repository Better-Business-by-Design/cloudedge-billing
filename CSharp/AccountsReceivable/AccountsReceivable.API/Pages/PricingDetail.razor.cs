using System.Security.Authentication;
using AccountsReceivable.BAL.Data;
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
                    .Include(pricing => pricing.Status)
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

    private async void OpenApproveDialog()
    {
        if (_schedule is null) throw new InvalidOperationException();
        if (User?.RoleId is not (RoleId.ReadWrite or RoleId.Administrator)) throw new AuthenticationException();

        var result = await DialogService.ShowMessageBox(
            "Approval Confirmation",
            "Are you sure you want to approve this Pricing Schedule?",
            "Approve", cancelText: "Cancel");

        if (result == null) return;

        _schedule.StatusId = StatusId.Approved;

        var audit = new Audit
        {
            UserId = User.EmailAddress,
            Action = $"Approved Schedule [{_schedule.Id}]",
            //Comment = !string.IsNullOrWhiteSpace(comment) ? comment : null, // TODO
            Timestamp = DateTime.Now
        };
        await DbContext.AddAsync(audit);

        await DbContext.SaveChangesAsync();
        Navigation.NavigateTo("pricing");
    }

    private async void OpenDeclineDialog()
    {
        if (_schedule is null) throw new InvalidOperationException();
        if (User?.RoleId is not (RoleId.ReadWrite or RoleId.Administrator)) throw new AuthenticationException();

        var result = await DialogService.ShowMessageBox(
            "Decline Confirmation",
            "Are you sure you want to decline this Pricing Schedule?",
            "Decline", cancelText: "Cancel");

        if (result == null) return;

        _schedule.StatusId = StatusId.Declined;

        var audit = new Audit
        {
            UserId = User.EmailAddress,
            Action = $"Declined Schedule [{_schedule.Id}]",
            //Comment = comment, // TODO
            Timestamp = DateTime.Now
        };
        await DbContext.AddAsync(audit);

        await DbContext.SaveChangesAsync();
        Navigation.NavigateTo("pricing");
    }
}