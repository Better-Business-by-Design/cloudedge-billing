using System.Security.Authentication;
using AccountsReceivable.API.Shared;
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

    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Buyer Created Invoices", "invoices"),
        new BreadcrumbItem("Detail", null, true)
    };

    private MudDataGrid<AnimalPriceGroup> _dataGrid = null!;

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

    private async Task<GridData<AnimalPriceGroup>> GridServerReload(GridState<AnimalPriceGroup> state)
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
                .Include(document => document.StaffComments)
                .Include(document => document.Plant.Meatwork)
                .FirstOrDefaultAsync(document => document.Id == Id);

            if (_document is null)
            {
                Navigation.NavigateTo("invoices");

                return new GridData<AnimalPriceGroup>
                {
                    TotalItems = 0,
                    Items = Array.Empty<AnimalPriceGroup>()
                };
            }

            StateHasChanged();
        }

        var fullQuery = await DbContext.Documents
            .AsNoTracking()
            .Include(document => document.Schedule!.Prices).ThenInclude(price => price.Grade.AnimalType)
            .Include(document => document.Animals!).ThenInclude(animal => animal.Grade)
            .Where(document => document.Id.Equals(Id))
            .SelectMany(document => document.Schedule!.Prices.Select(price => new
                {
                    Price = price,
                    ValidAnimals = document.Animals!.Where(animal => animal.GradeId == price.GradeId && animal.Weight <= price.MaxWeight && animal.Weight >= price.MinWeight)
                }
            ).Where(result => result.ValidAnimals.Any()))
            .ToListAsync();

        var animalPriceGroups = fullQuery.Select(result => new AnimalPriceGroup(
            result.Price.Grade.AnimalTypeId,
            result.Price.GradeId,
            result.ValidAnimals.First().ValidationId,
            result.Price.MinWeight,
            result.Price.MaxWeight,
            (ushort) result.ValidAnimals.Count(),
            result.ValidAnimals.Sum(animal => animal.Weight),
            result.ValidAnimals.Sum(animal => animal.PremiumCost),
            result.ValidAnimals.Sum(animal => animal.CalcPremiumCost),
            result.ValidAnimals.Sum(animal => animal.DeductionCost),
            result.ValidAnimals.Sum(animal => animal.CalcDeductionCost),
            result.ValidAnimals.First().Price,
            result.ValidAnimals.First().CalcPrice
        )).ToList();

        return new GridData<AnimalPriceGroup>
        {
            TotalItems = animalPriceGroups.Count,
            Items = animalPriceGroups
        };
    }

    private string RowStyleFunc(AnimalPriceGroup animalPriceGroup, int index)
    {
        //var striped = index % 2 == 1;
        var valid = animalPriceGroup.Cost == animalPriceGroup.CalcCost; // Switch to Validation

        return "background-color: " + (
            //striped && valid ? Colors.LightGreen.Lighten4 :
            valid
                ? Colors.LightGreen.Lighten4
                :
                //striped ? Colors.DeepOrange.Lighten4 :
                Colors.DeepOrange.Lighten4
        );
    }

    private async Task OpenComments()
    {
        if (_document == null || User == null) throw new NullReferenceException();

        var users = await DbContext.Set<User>()
            .AsNoTracking()
            .ToListAsync();

        var parameters = new DialogParameters<CommentDialog>
        {
            {dialog => dialog.DocumentId, _document.Id},
            {dialog => dialog.Comments, _document.StaffComments},
            {dialog => dialog.Users, users},
            {dialog => dialog.CurrentUser, User.EmailAddress}
        };

        var options = new DialogOptions
        {
            CloseButton = true,

            MaxWidth = MaxWidth.ExtraLarge,
            FullWidth = true
        };

        var dialog = await DialogService.ShowAsync<CommentDialog>("Comments", parameters, options);
        await dialog.Result; // Wait for Dialog to Close

        await DbContext.SaveChangesAsync();
    }

    private async Task OpenTransit()
    {
        if (_document == null || User == null) throw new NullReferenceException();

        var transits = await DbContext.Set<Transit>()
            .Include(transit => transit.SpeciesType)
            .Where(transit =>
                (transit.DocumentId == null || transit.DocumentId.Equals(_document.Id)) &&
                _document.DateProcessed.AddDays(-7) <= transit.Date &&
                transit.Date <= _document.DateProcessed.AddDays(7))
            .ToListAsync();

        var parameters = new DialogParameters<TransitDialog>
        {
            {dialog => dialog.DocumentId, _document.Id},
            {dialog => dialog.Transits, transits}
        };

        var options = new DialogOptions
        {
            CloseButton = true,

            MaxWidth = MaxWidth.ExtraLarge,
            FullWidth = true
        };

        var dialog = await DialogService.ShowAsync<TransitDialog>("Animals in Transit", parameters, options);
        await dialog.Result; // Wait for Dialog to Close

        _document.TransitQuantity = (ushort) transits
            .Where(transit => transit.DocumentId != null && transit.DocumentId.Equals(_document.Id))
            .Sum(transit => transit.Quantity);

        await DbContext.SaveChangesAsync();
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