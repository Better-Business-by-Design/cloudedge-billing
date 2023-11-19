﻿using System.Collections.Immutable;
using System.Linq.Expressions;
using AccountsReceivable.API.Shared;
using AccountsReceivable.API.Shared.FluidValidation;
using AccountsReceivable.API.Shared.NewDataRowForm;
using AccountsReceivable.BL.Models.Application;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Customers : EditableDataGridPage<Customer>
{
    [Parameter] public override bool Removable { get; set; } = false; 
    
    [Parameter]
    public bool ShowIsActive { get; set; } = true;
    
    private List<PayMonthlyPlan> _payMonthlyPlans = new();
    
    protected override List<BreadcrumbItem> Breadcrumb { get; set; } = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Customers", null, true)
    };
    
    protected override Task OnInitializedAsync()
    {
        Validator = new CustomerFluentValidator(DbContext);
        return base.OnInitializedAsync();
    }

    protected override IQueryable<Customer> BuildFullQuery()
    {
        _payMonthlyPlans = DbContext.PayMonthlyPlans.ToList();
        return DbContext.Customers
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
                    "Customer Name" => customer => customer.CustomerName,
                    "Domain Name" => customer => customer.DomainName ?? string.Empty,
                    "Xero Contact Name" => customer => customer.XeroContactName,
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
                "CustomerName" => customer => customer.CustomerName,
                "DomainName" => customer => customer.DomainName ?? string.Empty,
                "DomainUuid" => customer => customer.DomainUuid ?? Guid.Empty,
                "XeroContactName" => customer => customer.XeroContactName,
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

    protected override async Task OnAddButtonClicked()
    {
        var newDefaultCustomer = await BuildNewDefaultRow();
        var parameters = new DialogParameters<AddNewCustomerForm>()
        {
            { dialog => dialog.Validator, Validator },
            { dialog => dialog.NewDataRow, newDefaultCustomer },
            { dialog => dialog.PayMonthlyPlans, DbContext.PayMonthlyPlans.ToImmutableList()}
        };

        var options = new DialogOptions()
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Large,
            FullWidth = true
        };
        
        var dialog = await DialogService.ShowAsync<AddNewCustomerForm>("Add new customer", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            await AddRow((Customer) result.Data);
        }
    }

    protected override void ReadOnlyRowClicked(DataGridRowClickEventArgs<Customer> args)
    {
        Navigation.NavigateTo($"customers/{args.Item.Id}"); 
    }

    protected override async Task<Customer> BuildNewDefaultRow()
    {
        return new Customer();
    }
    
    
}
