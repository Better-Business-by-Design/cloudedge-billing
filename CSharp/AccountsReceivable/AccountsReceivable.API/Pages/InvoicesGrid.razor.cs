using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class InvoicesGrid
{
    private string _searchString = string.Empty;
    private MudTable<Document> _table = null!;
    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Buyer Created Invoices", null, true)
    };

    private int _totalItems;

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    protected virtual NavigationManager Navigation { get; set; } = default!;
    

    private async Task<GridData<Document>> GridServerReload(GridState<Document> state)
    {
        var fullQuery = DbContext.Documents
            .AsNoTracking()
            .Include(document => document.Farm)
            .Include(document => document.Plant)
            .Include(document => document.Status)
            .Include(document => document.SpeciesType)
            .Include(document => document.Plant.Meatwork);

        var filteredQuery = fullQuery.Where(document =>
            string.IsNullOrWhiteSpace(_searchString) ||
            EF.Functions.Like(document.KillSheet.ToString(), $"%{_searchString}%") ||
            EF.Functions.Like(document.Farm.Name, $"%{_searchString}%") ||
            EF.Functions.Like(document.Plant.Name, $"%{_searchString}%") ||
            EF.Functions.Like(document.Status.Name, $"%{_searchString}%") ||
            EF.Functions.Like(document.Plant.Meatwork.Name, $"%{_searchString}%") ||
            EF.Functions.Like(
                document.SpeciesType != null ? document.SpeciesType.DisplayName : "Unknown",
                $"%{_searchString}%")
        );

        // var orderedQuery = state.SortLabel switch
        // {
        //     "date_field" => filteredQuery.OrderByDirection(state.SortDirection, document => document.DateProcessed),
        //     "killsheet_field" => filteredQuery.OrderByDirection(state.SortDirection, document => document.KillSheet),
        //     "plant_field" => filteredQuery.OrderByDirection(state.SortDirection,
        //         document => document.Plant.Name),
        //     "farm_field" => filteredQuery.OrderByDirection(state.SortDirection,
        //         document => document.Farm.Name),
        //     "species_field" => filteredQuery.OrderByDirection(state.SortDirection,
        //         document => document.SpeciesTypeId),
        //     "status_field" => filteredQuery.OrderByDirection(state.SortDirection,
        //         document => document.StatusId),
        //     _ => filteredQuery
        // };

        _totalItems = filteredQuery.Count();

        return new GridData<Document>
        {
            TotalItems = _totalItems,
            Items = await filteredQuery.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArrayAsync()
        };
    }

    // private void OnSearch(string searchString)
    // {
    //     _searchString = searchString;
    //     _table.ReloadServerData();
    // }
    
    private Func<Document, bool> _quickFilter => document =>
    {
        if (string.IsNullOrWhiteSpace(_searchString)) return true;

        if (document.KillSheet.ToString().Contains(_searchString, StringComparison.OrdinalIgnoreCase)) return true;
        if (document.Farm.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) return true;
        if (document.Plant.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) return true;
        if (document.Status.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) return true;
        if (document.Plant.Meatwork.Name.Contains(_searchString, StringComparison.OrdinalIgnoreCase)) return true;
        if (document.SpeciesType?.DisplayName.Contains(_searchString, StringComparison.OrdinalIgnoreCase) ?? false) return true;

        return false;
    };

    private void RowClicked(DataGridRowClickEventArgs<Document> args)
    {
        Navigation.NavigateTo($"invoices/{args.Item.Id}");
    }

    // private void RowClickEvent(TableRowClickEventArgs<Document> clickEvent)
    // {
    //     Navigation.NavigateTo($"invoices/{clickEvent.Item.Id}");
    // }

    private string RowStyleFunc(Document document, int index)
    {
        return string.Empty;

        // TODO - Validation
        return document.SpeciesType == null
            ? $"background-color: {Colors.DeepOrange.Lighten4};"
            : string.Empty;
    }
}