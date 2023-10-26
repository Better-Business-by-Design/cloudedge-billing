using System.Linq.Expressions;
using AccountsReceivable.API.Shared;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            .Include(document => document.SpeciesType);

        var filteredQuery = fullQuery.Where(document =>
            string.IsNullOrWhiteSpace(_searchString) ||
            EF.Functions.Like(document.KillSheet.ToString(), $"%{_searchString}%") ||
            EF.Functions.Like(document.Farm.Name, $"%{_searchString}%") ||
            EF.Functions.Like(document.Plant.Name, $"%{_searchString}%") ||
            EF.Functions.Like(document.Status.Name, $"%{_searchString}%") ||
            EF.Functions.Like(
                document.SpeciesType != null ? document.SpeciesType.DisplayName : "Unknown",
                $"%{_searchString}%")
        );


        foreach (var filterDefinition in state.FilterDefinitions)
        {
            if (filterDefinition.Value is null && filterDefinition.Operator is not ("is empty" or "is not empty")) 
            {
                continue;
            }
            
            Expression<Func<Document, bool>> fullPredicate;
            
            if (filterDefinition.FieldType.IsString)
            {
                var value = filterDefinition.Value?.ToString() ?? string.Empty;
                
                Expression<Func<Document, string>> selectPredicate = filterDefinition.Title switch
                {
                    "Farm" => document => document.Farm.Name,
                    "Plant (Works)" => document => document.Plant.Name,
                    _ => throw new NotImplementedException()
                };

                Expression<Func<string, bool>> logicPredicate = filterDefinition.Operator switch
                {
                    "contains" => property => property.Contains(value),
                    "not contains" => property => !property.Contains(value),
                    "equals" => property => property.Equals(value),
                    "not equals" => property => !property.Equals(value),
                    "starts with" => property => property.StartsWith(value),
                    "ends with" => property => property.EndsWith(value),
                    "is empty" => property => property.Equals(string.Empty),
                    "is not empty" => property => !property.Equals(string.Empty),
                    _ => throw new NotImplementedException()
                };

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsBoolean)
            {
                throw new NotImplementedException();
            } 
            else if (filterDefinition.FieldType.IsEnum)
            {
                var value = filterDefinition.Value;
                Expression<Func<Document, Enum>> selectPredicate = filterDefinition.Title switch
                {
                    "Species" => document => document.SpeciesTypeId,
                    "Validation" => throw new NotImplementedException(), // TODO...
                    "Approval Status" => document => document.StatusId,
                    "ATS Status" => throw new NotImplementedException(), // TODO...
                    _ => throw new NotImplementedException()
                };

                Expression<Func<Enum, bool>> logicPredicate = filterDefinition.Operator switch
                {
                    "is" => property => property.Equals(value),
                    "is not" => property => !property.Equals(value),
                    _ => throw new NotImplementedException()
                };

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsGuid)
            {
                throw new NotImplementedException();
            } 
            else if (filterDefinition.FieldType.IsNumber)
            {
                var value = Convert.ToDecimal(filterDefinition.Value ?? 0);
                Expression<Func<Document, decimal>> selectPredicate = filterDefinition.Title switch
                {
                    "KillSheet" => document => document.KillSheet,
                    "Stock Count" => document => document.StockCount,
                    "Stock Weight" => document => document.WeightTotal,
                    _ => throw new NotImplementedException()
                };

                Expression<Func<decimal, bool>> logicPredicate = filterDefinition.Operator switch
                {
                    "=" => property => property == value,
                    "!=" => property => property != value,
                    ">" => property => property > value,
                    ">=" => property => property >= value,
                    "<" => property => property < value,
                    "<=" => property => property <= value,
                    "is empty" => property => false, // TODO... find better way to implement
                    "is not empty" => property => true, // TODO... find better way to implement
                    _ => throw new NotImplementedException()
                };

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsDateTime)
            {
                var value = Convert.ToDateTime(filterDefinition.Value ?? new DateTime());
                Expression<Func<Document, DateTime>> selectPredicate = filterDefinition.Title switch
                {
                    "Date" => document => document.DateProcessed,
                    _ => throw new NotImplementedException()
                };

                Expression<Func<DateTime, bool>> logicPredicate = filterDefinition.Operator switch
                {
                    "is" => property => property.Equals(value),
                    "is not" => property => !property.Equals(value),
                    "is after" => property => property.CompareTo(value) > 0,
                    "is on or after" => property => property.CompareTo(value) >= 0,
                    "is before" => property => property.CompareTo(value) < 0,
                    "is on or before" => property => property.CompareTo(value) <= 0,
                    "is empty" => property => property.CompareTo(new DateTime()) == 0,
                    "is not empty" => property => property.CompareTo(new DateTime()) != 0,
                    _ => throw new NotImplementedException()
                };
                fullPredicate = selectPredicate.Compose(logicPredicate);
            }
            else
            {
                throw new NotImplementedException();
            }
            filteredQuery = filteredQuery.Where(fullPredicate);
        }
        
        Expression<Func<Document, object>> keySelector = document => 0;
        var orderedQuery = filteredQuery.OrderBy(keySelector);
        
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var sortDefinition in state.SortDefinitions)
        {
            
            keySelector = sortDefinition.SortBy switch
            {
                "DateProcessed" => document => document.DateProcessed,
                "KillSheet" => document => document.KillSheet,
                "Farm.Name" => document => document.Farm.Name,
                "SpeciesType" => document => document.SpeciesType.DisplayName,
                "StockCount" => document => document.StockCount,
                "WeightTotal" => document => document.WeightTotal,
                "Plant.Name" => document => document.Plant.Name,
                "StatusId" => document => document.StatusId,
                "TransitId" => document => document.TransitId == null ? string.Empty : document.TransitId,
                _ => throw new NotImplementedException()
            };

            orderedQuery = sortDefinition.Descending
                ? orderedQuery.ThenByDescending(keySelector)
                : orderedQuery.ThenBy(keySelector);
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
        var color = document.StatusId switch
        {
            StatusId.Pending => Colors.LightBlue.Lighten4,
            StatusId.Approved => Colors.LightGreen.Lighten4,
            StatusId.Overridden => Colors.Red.Lighten4,
            StatusId.Declined => Colors.Red.Lighten2,
            StatusId.Superseded => Colors.Grey.Lighten4,
            StatusId.Missing => Colors.Red.Lighten1,
            _ => Colors.Shades.White
        }; 
        return $"background-color: {color}";
        
    };
}
