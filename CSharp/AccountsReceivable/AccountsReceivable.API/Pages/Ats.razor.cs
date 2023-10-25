using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Account;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Ats
{
    private MudTable<Document> _table = null!;
    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Animals in Transit", null, true)
    };

    private string _searchString = string.Empty;
    private int _totalItems;

    [CascadingParameter]
    private User? User { get; set; }

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    private IDialogService DialogService { get; set; } = default!;

    private async Task<TableData<Document>> ServerReload(TableState state)
    {
        var fullQuery = DbContext.Documents
            .AsNoTracking()
            .Include(document => document.Farm)
            .Include(document => document.Plant)
            .Include(document => document.SpeciesType)
            .Include(document => document.Plant.Meatwork)
            .Where(document => new[] { StatusId.Approved, StatusId.Overridden }.Contains(document.StatusId));

        var filteredQuery = fullQuery.Where(document =>
            string.IsNullOrWhiteSpace(_searchString) ||
            EF.Functions.Like(document.KillSheet.ToString(), $"%{_searchString}%") ||
            EF.Functions.Like(document.Farm.Name, $"%{_searchString}%") ||
            EF.Functions.Like(document.Plant.Name, $"%{_searchString}%") ||
            EF.Functions.Like(document.Plant.Meatwork.Name, $"%{_searchString}%") ||
            EF.Functions.Like(
                document.SpeciesType != null ? document.SpeciesType.DisplayName : "Unknown",
                $"%{_searchString}%")
        );

        var orderedQuery = state.SortLabel switch
        {
            "date_field" => filteredQuery.OrderByDirection(state.SortDirection, document => document.DateProcessed),
            "killsheet_field" => filteredQuery.OrderByDirection(state.SortDirection, document => document.KillSheet),
            "plant_field" => filteredQuery.OrderByDirection(state.SortDirection,
                document => document.Plant.Name),
            "farm_field" => filteredQuery.OrderByDirection(state.SortDirection,
                document => document.Farm.Name),
            "species_field" => filteredQuery.OrderByDirection(state.SortDirection,
                document => document.SpeciesTypeId),
            _ => filteredQuery
        };

        _totalItems = orderedQuery.Count();

        return new TableData<Document>
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

    private async void OpenDialog()
    {
        var result = await DialogService.ShowMessageBox(
            "Override Confirmation",
            "Are you sure you want to override this Animal in Transit?",
            "Override", cancelText: "Cancel");

        if (result == null) return;

        // Todo
        await _table.ReloadServerData();
    }
}