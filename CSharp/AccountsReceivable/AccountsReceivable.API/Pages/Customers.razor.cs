using System.Linq.Expressions;
using AccountsReceivable.API.Shared;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Customers : DataGridPage<Customer>
{
    
    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Customers", null, true)
    };

    protected override IQueryable<Customer> BuildFullQuery()
    {
        return DbContext.Customers
            .AsNoTracking()
            .Include(customer => customer.PayMonthlyPlan);
    }
    
    protected override IQueryable<Customer> FilterFullQuery(
        IQueryable<Customer> fullQuery, 
        IEnumerable<IFilterDefinition<Customer>> filterDefinitions)
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
            
            Expression<Func<Customer, bool>> fullPredicate;
            
            // Annoying that this can't be a switch
            if (filterDefinition.FieldType.IsString)
            {
                var value = filterDefinition.Value?.ToString() ?? string.Empty;
                
                Expression<Func<Customer, string>> selectPredicate = filterDefinition.Title switch
                {
                    "Parent Name" => customer => customer.ParentName,
                    "Customer Name" => customer => customer.CustomerName ?? string.Empty,
                    "Invoice Name" => customer => customer.InvoiceName ?? string.Empty,
                    "Plan Name" => customer => customer.PayMonthlyPlan != null ? customer.PayMonthlyPlan.PlanName : string.Empty, 
                    "Location" => customer => customer.Location ?? string.Empty,
                    _ => throw new NotImplementedException()
                };

                var logicPredicate = GenerateStringLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsBoolean)
            {
                var value = Convert.ToBoolean(filterDefinition.Value ?? true);
                Expression<Func<Customer, bool>> selectPredicate = filterDefinition.Title switch
                {
                    "Is Active" => customer => customer.IsActive,
                    _ => throw new NotImplementedException($"{filterDefinition.Title} not implemented in Boolean filters.")
                };

                var logicPredicate = GenerateBooleanLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsEnum)
            {
                throw new NotImplementedException(
                    $"No enum filtering implemented for Customers table, including not for {filterDefinition.Title}");
            } 
            else if (filterDefinition.FieldType.IsGuid)
            {
                var value = Guid.Parse(filterDefinition.Value?.ToString() ?? string.Empty);
                Expression<Func<Customer, Guid>> selectPredicate = filterDefinition.Title switch
                {
                    "Domain UUID" => customer => customer.DomainUuid ?? Guid.Empty,
                    _ => throw new NotImplementedException($"{filterDefinition.Title} not implemented in Guid filters.")
                };

                var logicPredicate = GenerateGuidLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsNumber)
            {
                var value = Convert.ToDecimal(filterDefinition.Value ?? 0);
                Expression<Func<Customer, decimal>> selectPredicate = filterDefinition.Title switch
                {
                    "ID" => customer => customer.Id,
                    _ => throw new NotImplementedException()
                };

                var logicPredicate = GenerateDecimalLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsDateTime)
            {
                throw new NotImplementedException(
                    $"No datetime filtering implemented for Customers table, including not for {filterDefinition.Title}");
            }
            else
            {
                throw new NotImplementedException(
                    $"No {filterDefinition.FieldType} filtering implemented for Customers table.");
            }
            filteredQuery = filteredQuery.Where(fullPredicate);
        }

        return filteredQuery;
    }

    protected override IOrderedQueryable<Customer> OrderFilteredQuery(
        IQueryable<Customer> filteredQuery, 
        IEnumerable<SortDefinition<Customer>> sortDefinitions)
    {
        var orderedQuery = filteredQuery.OrderBy(customer => true);
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var sortDefinition in sortDefinitions)
        {
            Expression<Func<Customer, object>> keySelector = sortDefinition.SortBy switch
            {
                "Id" => customer => customer.Id,
                "ParentName" => customer => customer.ParentName,
                "CustomerName" => customer => customer.CustomerName ?? string.Empty,
                "DomainUuid" => customer => customer.DomainUuid ?? Guid.Empty,
                "InvoiceName" => customer => customer.InvoiceName ?? string.Empty,
                "PayMonthlyPlan.PlanName" => customer => customer.PayMonthlyPlan != null ? customer.PayMonthlyPlan.PlanName : string.Empty, 
                "IsActive" => customer => customer.IsActive,
                "Location" => customer => customer.Location ?? string.Empty,
                _ => throw new NotImplementedException(
                    $"Sorting not implemented for {sortDefinition.SortBy} column in Customers table.")
            };

            orderedQuery = sortDefinition.Descending
                ? orderedQuery.ThenByDescending(keySelector)
                : orderedQuery.ThenBy(keySelector);
        }

        return orderedQuery;
    }
    
    protected override void RowClicked(DataGridRowClickEventArgs<Customer> args)
    {
        Navigation.NavigateTo($"customers/{args.Item.Id}");
    }
}
