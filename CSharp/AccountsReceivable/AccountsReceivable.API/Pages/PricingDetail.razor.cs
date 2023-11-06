using AccountsReceivable.API.ViewModels;
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

    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Pricing Schedules", "pricing"),
        new BreadcrumbItem("Detail", null, true)
    };
    
    private MudDataGrid<SchedulePriceGroup> _dataGrid = null!;

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

    private async Task<GridData<SchedulePriceGroup>> GridServerReload(GridState<SchedulePriceGroup> state)
    {
        if (_schedule == null)
        {
            if (ushort.TryParse(Id, out var id))
                _schedule = DbContext.Schedules
                    .Include(schedule => schedule.Status)
                    .Include(schedule => schedule.Uplifts)
                    .Include(schedule => schedule.Meatwork)
                    .FirstOrDefault(schedule => schedule.Id == id);

            if (_schedule is null)
            {
                Navigation.NavigateTo("pricing");

                return new GridData<SchedulePriceGroup>
                {
                    TotalItems = 0,
                    Items = Array.Empty<SchedulePriceGroup>()
                };
            }         
            
            StateHasChanged();
        }

        var fullQuery =await DbContext.Schedules
            .AsNoTracking()
            .Include(schedule => schedule.Prices).ThenInclude(price => price.Grade.AnimalType)
            .Where(schedule => schedule.Id.Equals(_schedule.Id))
            .SelectMany(schedule => schedule.Prices)
            .GroupBy(price => new { price.Grade.AnimalTypeId, price.MinWeight, price.MaxWeight, price.Cost })
            .OrderBy(group => group.Key.AnimalTypeId)
            .ThenBy(group => group.Key.MinWeight)
            .ThenByDescending(group => group.Key.MaxWeight)
            .Select(group => new SchedulePriceGroup(
                group.Key.AnimalTypeId, 
                group.Select(price => price.GradeId).ToArray(), 
                group.Key.MinWeight,
                group.Key.MaxWeight,
                group.Key.Cost))
            .ToListAsync();

        return new GridData<SchedulePriceGroup>
        {
            TotalItems = fullQuery.Count,
            Items = fullQuery
        };
    }

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