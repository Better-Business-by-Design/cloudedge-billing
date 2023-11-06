using System.Linq.Expressions;
using AccountsReceivable.API.Shared;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Invoices : DataGridPage<Document>
{
    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Buyer Created Invoices", null, true)
    };

    protected override IQueryable<Document> BuildFullQuery()
    {
        return DbContext.Documents
            .AsNoTracking()
            .Include(document => document.Farm)
            .Include(document => document.Plant)
            .Include(document => document.Status)
            .Include(document => document.SpeciesType)
            .Include(document => document.CalcValidation);
    }
    
    protected override IQueryable<Document> FilterFullQuery(
        IQueryable<Document> fullQuery, 
        IEnumerable<IFilterDefinition<Document>> filterDefinitions)
    {
        var filteredQuery = fullQuery;
        foreach (var filterDefinition in filterDefinitions)
        {
            if (filterDefinition.Operator is null) { continue; }
            var logicOperator = filterDefinition.Operator!;
            
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

                var logicPredicate = GenerateStringLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsBoolean)
            {
                throw new NotImplementedException();
            } 
            else if (filterDefinition.FieldType.IsEnum)
            {
                var value = filterDefinition.Value!;
                Expression<Func<Document, Enum>> selectPredicate = filterDefinition.Title switch
                {
                    "Species" => document => document.SpeciesTypeId,
                    "Validation" => throw new NotImplementedException(), // TODO...
                    "Approval Status" => document => document.StatusId,
                    "ATS Status" => throw new NotImplementedException(), // TODO...
                    _ => throw new NotImplementedException()
                };

                var logicPredicate =
                    GenerateEnumLogicPredicate(logicOperator, value);

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
                    "Stock Count" => document => document.StockQuantity,
                    "Stock Weight" => document => document.WeightTotal,
                    _ => throw new NotImplementedException()
                };

                var logicPredicate = GenerateDecimalLogicPredicate(logicOperator, value);

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

                var logicPredicate = GenerateDateTimeLogicPredicate(logicOperator, value);
                fullPredicate = selectPredicate.Compose(logicPredicate);
            }
            else
            {
                throw new NotImplementedException();
            }
            filteredQuery = filteredQuery.Where(fullPredicate);
        }

        return filteredQuery;
    }

    protected override IOrderedQueryable<Document> OrderFilteredQuery(
        IQueryable<Document> filteredQuery, 
        IEnumerable<SortDefinition<Document>> sortDefinitions)
    {
        var orderedQuery = filteredQuery.OrderBy(document => true);
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var sortDefinition in sortDefinitions)
        {
            Expression<Func<Document, object>> keySelector = sortDefinition.SortBy switch
            {
                "DateProcessed" => document => document.DateProcessed,
                "KillSheet" => document => document.KillSheet,
                "Farm.Name" => document => document.Farm.Name,
                "SpeciesTypeId" => document => document.SpeciesType.DisplayName,
                "StockQuantity" => document => document.StockQuantity,
                "WeightTotal" => document => document.WeightTotal,
                "Plant.Name" => document => document.Plant.Name,
                "CalcValidationId" => document => document.CalcValidationId,
                "TransitQuantity" => document => document.TransitQuantity,
                "StatusId" => document => document.StatusId,
                _ => throw new NotImplementedException()
            };

            orderedQuery = sortDefinition.Descending
                ? orderedQuery.ThenByDescending(keySelector)
                : orderedQuery.ThenBy(keySelector);
        }

        return orderedQuery;
    }
    
    protected override void RowClicked(DataGridRowClickEventArgs<Document> args)
    {
        Navigation.NavigateTo($"invoices/{args.Item.Id}");
    }
}
