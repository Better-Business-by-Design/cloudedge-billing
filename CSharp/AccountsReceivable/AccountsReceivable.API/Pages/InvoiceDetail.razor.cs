using System.Security.Authentication;
using AccountsReceivable.API.ViewModels;
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
    private MudDataGrid<PriceAnimals> _dataGrid = null!;

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

    private async Task<GridData<PriceAnimals>> GridServerReload(GridState<PriceAnimals> state)
    {
        if (_document is null)
        {
            /*var ds = await DbContext.Documents.Include(s => s.Schedule).ToListAsync();
            foreach (var d in ds) await d.CalculatePricesAsync(DbContext);
            await DbContext.SaveChangesAsync();*/

            _document = await DbContext.Documents
                .Include(document => document.Farm)
                .Include(document => document.Status)
                .Include(document => document.Plant)
                .Include(document => document.Plant.Meatwork)
                .FirstOrDefaultAsync(document => document.Id == Id);

            if (_document is null)
            {
                Navigation.NavigateTo("invoices");

                return new GridData<PriceAnimals>
                {
                    TotalItems = _totalItems,
                    Items = Array.Empty<PriceAnimals>()
                };
            }

            StateHasChanged();
        }

        var fullQuery = DbContext.Documents
            .AsNoTracking()
            .Include(document => document.Schedule!.Prices)
            .ThenInclude(prices => prices.Grade)
            .Include(document => document.Animals!)
            .ThenInclude(animal => animal.Grade)
            .Where(document => document.Id.Equals(Id))
            .SelectMany(document => document.Schedule!.Prices.Select(price => new PriceAnimals
                {
                    Price = price,
                    Grade = price.Grade,
                    StockCount = (ushort)(document.Animals != null ? document.Animals.Count(animal => animal.GradeId == price.GradeId && animal.Weight <= price.MaxWeight && animal.Weight >= price.MinWeight) : 0),
                    StockWeight = document.Animals != null
                        ? document.Animals.Where(animal => animal.GradeId == price.GradeId && animal.Weight <= price.MaxWeight && animal.Weight >= price.MinWeight).Sum(animal => animal.Weight)
                        : 0,
                    Cost = document.Animals != null ? document.Animals.First(animal => animal.GradeId == price.GradeId && animal.Weight <= price.MaxWeight && animal.Weight >= price.MinWeight).Price : 0,
                    CalcCost = document.Animals != null ? document.Animals.First(animal => animal.GradeId == price.GradeId && animal.Weight <= price.MaxWeight && animal.Weight >= price.MinWeight).CalcPrice : 0
                }
            ).Where(animalPrice => animalPrice.StockCount != 0));

        _totalItems = fullQuery.Count();

        return new GridData<PriceAnimals>
        {
            TotalItems = _totalItems,
            Items = await fullQuery.ToArrayAsync()
        };
    }

    private string RowStyleFunc(PriceAnimals priceAnimals, int index)
    {
        var striped = index % 2 == 1;
        var valid = priceAnimals.Cost == priceAnimals.CalcCost; // Switch to Validation

        return "background-color: " + (
            striped && valid ? Colors.LightGreen.Lighten3 :
            valid ? Colors.LightGreen.Lighten4 :
            striped ? Colors.DeepOrange.Lighten3 :
            Colors.DeepOrange.Lighten4
        );
    }

    private async Task RecalculatePricing()
    {
        await _document!.CalculatePricesAsync(DbContext);
        await DbContext.SaveChangesAsync();

        Navigation.NavigateTo($"invoices/{_document!.Id}", true);
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