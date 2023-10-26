using System.Security.Authentication;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BAL.Extensions;
using AccountsReceivable.BL.Models.Account;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class InvoiceDetail
{
    private Document? _document;
    private string _searchString = string.Empty;
    private bool _showAnimalId;
    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Buyer Created Invoices", "invoices"),
        new BreadcrumbItem("Detail", null, true)
    };

    private MudTable<Animal> _table = null!;
    /*private TableGroupDefinition<Animal> _group = new()
    {
        GroupName = "Animal",
        Indentation = false,
        Expandable = true,
        IsInitiallyExpanded = false,
        Selector = animal => $"{animal.GradeId} {animal.Weight}"
    };*/

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

    private async Task<TableData<Animal>> ServerReload(TableState state)
    {
        if (_document is null)
        {
            _document = await DbContext.Documents
                .Include(document => document.Farm)
                .Include(document => document.Status)
                .Include(document => document.Plant)
                .Include(document => document.Plant.Meatwork)
                .FirstOrDefaultAsync(document => document.Id == Id);

            if (_document is null)
            {
                Navigation.NavigateTo("invoices");

                return new TableData<Animal>
                {
                    TotalItems = _totalItems,
                    Items = Array.Empty<Animal>()
                };
            }

            StateHasChanged();
        }

        var fullQuery = DbContext.Set<Animal>()
            .AsNoTracking()
            .Include(animal => animal.Grade)
            .Include(animal => animal.Grade!.AnimalType)
            .Include(animal => animal.DeductionDetails)
            .Include(animal => animal.PremiumDetails)
            .Where(animal => animal.DocumentId == _document!.Id);

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
        _showAnimalId = fullQuery.Any(animal => animal.NaitEid != null);

        return new TableData<Animal>
        {
            TotalItems = _totalItems,
            Items = await fullQuery.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArrayAsync()
        };
    }

    private string RowStyleFunc(Animal animal, int index)
    {
        // TODO - Validation
        return /*animal.ValidationId is ValidationId.Low or ValidationId.High
            ? $"background-color: {Colors.DeepOrange.Lighten4};"
            :*/ string.Empty;
    }

    private async Task RecalculatePricing()
    {
        await _document!.CalculatePrices(DbContext);
        await DbContext.SaveChangesAsync();
        
        Navigation.NavigateTo($"invoices/{_document!.Id}");
    }
    
    private async Task SetStatusApproved()
    {
        await SetDocumentStatus(StatusId.Approved);
        
        Navigation.NavigateTo("invoices");
    }
    
    private async Task SetStatusDeclined()
    {
        await SetDocumentStatus(StatusId.Declined);
        
        Navigation.NavigateTo("invoices");
    }

    private async Task SetDocumentStatus(StatusId statusId)
    {
        _document!.StatusId = statusId;

        var audit = new Audit
        {
            UserId = User!.EmailAddress,
            Action = $"{StatusHelper.GetInfo(statusId).Name} BCI [{_document.Id}]",
            // Comment = comment, // todo
            Timestamp = DateTime.Now
        };
        
        await DbContext.AddAsync(audit);
        await DbContext.SaveChangesAsync();
    }
}