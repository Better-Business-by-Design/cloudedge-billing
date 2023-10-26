using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Utilities;


namespace AccountsReceivable.API.Pages;

partial class Invoices
{
    private MudDataGrid<Document> _dataGrid = null!;
    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Buyer Created Invoices", null, true)
    };
    
    private string _searchString = string.Empty;
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
            //EF.Functions.Like(document.Plant.Meatwork.Name, $"%{_searchString}%") ||
            EF.Functions.Like(
                document.SpeciesType != null ? document.SpeciesType.DisplayName : "Unknown",
                $"%{_searchString}%")
        );

        var orderedQuery = filteredQuery.OrderBy(document => 0);
        
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var sortDefinition in state.SortDefinitions)
        {
            orderedQuery = sortDefinition.SortBy switch
            {
                "DateProcessed" => orderedQuery.ThenBy(document => document.DateProcessed),
                "KillSheet" => orderedQuery.ThenBy(document => document.KillSheet),
                "Plant.Name" => orderedQuery.ThenBy(document => document.Plant.Name),
                "Farm.Name" => orderedQuery.ThenBy(document => document.Farm.Name),
                "SpeciesType" => orderedQuery.ThenBy(document => 
                    document.SpeciesType == null ? string.Empty : document.SpeciesType.DisplayName),
                "StatusId" => orderedQuery.ThenBy(document => document.StatusId),
                _ => throw new NotImplementedException()
            };
        }

        _totalItems = orderedQuery.Count();

        return new GridData<Document>
        {
            TotalItems = _totalItems,
            Items = await orderedQuery.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArrayAsync()
        };
    }
    
    private void OnSearch(string searchString)
    {
        _searchString = searchString;
        _dataGrid.ReloadServerData();
    }

    private void RowClicked(DataGridRowClickEventArgs<Document> args)
    {
        Navigation.NavigateTo($"invoices/{args.Item.Id}");
    }

    private Func<Document, int, string> _rowStyleFunc => (document, i) =>
    {
        if (document.SpeciesType is null)
        {
            return $"background-color: {Colors.DeepOrange.Lighten4}";
        }

        return string.Empty;
    };
}