

using System.Collections.Immutable;
using System.Linq.Expressions;
using CloudEdgeBilling.API.Shared;
using CloudEdgeBilling.API.Shared.FluidValidation;
using CloudEdgeBilling.API.Shared.NewDataRowForm;
using CloudEdgeBilling.BAL.Data;
using CloudEdgeBilling.BL.Models.Application;
using CloudEdgeBilling.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace CloudEdgeBilling.API.Pages;

public partial class CustomerDetail : EditableDataGridPage<LineItem>
{
     protected override string StateKey => "CustomerDetailStateKey" + Id;

    [Parameter]
    public string? Id { get; set; }

    private Customer? _customer;

    private ImmutableList<Account> _accounts = null!;
    private ImmutableList<Business> _businesses = null!;

    protected override async Task OnInitializedAsync()
    {
        if (Id is null)
        {
            Console.WriteLine(Crayon.Output.Red("Received null value for Customer Detail Id!"));
            Navigation.NavigateTo("/customers");
        }
        else
        {
            Validator = new LineItemFluentValidator();
            await using var dbContext = await DbContextFactory.CreateDbContextAsync();
            _customer ??= dbContext.Customers.FirstOrDefault(customer => customer.Id == Convert.ToInt32(Id ?? "0"));
            _accounts = dbContext.Accounts.ToImmutableList();
            _businesses = dbContext.Businesses.ToImmutableList();
        }
        await base.OnInitializedAsync();
    }


    protected override List<BreadcrumbItem> Breadcrumb { get; set; } = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Customers", "customers"),
        new BreadcrumbItem("Line Items", null, true)
    };

    protected override IQueryable<LineItem> BuildFullQuery(ApplicationDbContext dbContext)
    {
        return dbContext.LineItems
            .Where(item => item.CustomerId == Convert.ToInt32(Id))
            .Include(item => item.Business)
            .Include(item => item.Account);
    }

    protected override IQueryable<LineItem> FilterFullQuery(
        IQueryable<LineItem> fullQuery,
        IEnumerable<IFilterDefinition<LineItem>> filterDefinitions)
    {
        var filteredQuery = fullQuery;
        foreach (var filterDefinition in filterDefinitions)
        {
            if (filterDefinition.Operator is null)
            {
                continue;
            }
            var logicOperator = filterDefinition.Operator!;

            if (filterDefinition.Value is null && filterDefinition.Operator is not ("is empty" or "is not empty"))
            {
                continue;
            }

            Expression<Func<LineItem, bool>> fullPredicate;

            // Annoying that this can't be a switch
            if (filterDefinition.FieldType.IsString)
            {
                var value = filterDefinition.Value?.ToString() ?? string.Empty;

                Expression<Func<LineItem, string>> selectPredicate = filterDefinition.Title switch
                {
                    "Description" => item => item.Description,
                    "Account" => item => item.Account.Name,
                    "Business" => item => item.Business.Name,
                    _ => throw new NotImplementedException($"{filterDefinition.Title} not implemented in string filters.")
                    };

                var logicPredicate = GenerateStringLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            }
            else if (filterDefinition.FieldType.IsBoolean)
            {
                throw new NotImplementedException(
                    $"No boolean filtering implemented for Line Items table, including not for {filterDefinition.Title}");
            }
            else if (filterDefinition.FieldType.IsEnum)
            {
                var value = filterDefinition.Value!;
                Expression<Func<LineItem, Enum>> selectPredicate = filterDefinition.Title switch
                {
                    "Business" => user => user.BusinessId,
                    _ => throw new NotImplementedException($"{filterDefinition.Title} not implemented in boolean filters.")
                    };

                var logicPredicate =
                    GenerateEnumLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            }
            else if (filterDefinition.FieldType.IsGuid)
            {
                throw new NotImplementedException(
                    $"No Guid filtering implemented for Line Items table, including not for {filterDefinition.Title}");
            }
            else if (filterDefinition.FieldType.IsNumber)
            {
                var value = Convert.ToDecimal(filterDefinition.Value ?? 0);
                Expression<Func<LineItem, decimal>> selectPredicate = filterDefinition.Title switch
                {
                    "ID" => item => item.Id,
                    "Quantity" => item => Convert.ToDecimal(item.Quantity),
                    "Unit Price" => item => item.UnitPrice,
                    "Discount" => item => item.Discount,
                    _ => throw new NotImplementedException($"{filterDefinition.Title} not implemented in decimal filters.")
                    };

                var logicPredicate = GenerateDecimalLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            }
            else if (filterDefinition.FieldType.IsDateTime)
            {
                throw new NotImplementedException(
                    $"No datetime filtering implemented for Line Items table, including not for {filterDefinition.Title}");
            }
            else
            {
                throw new NotImplementedException(
                    $"No {filterDefinition.FieldType} filtering implemented for Line Items table.");
            }
            filteredQuery = filteredQuery.Where(fullPredicate);
        }

        return filteredQuery;
    }

    protected override IOrderedQueryable<LineItem> OrderFilteredQuery(IQueryable<LineItem> filteredQuery, IEnumerable<SortDefinition<LineItem>> sortDefinitions)
    {
        var orderedQuery = filteredQuery.OrderBy(item => true);
    // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var sortDefinition in sortDefinitions)
        {
            Expression<Func<LineItem, object>> keySelector = sortDefinition.SortBy switch
            {
                "Id" => item => item.Id,
                "Description" => item => item.Description,
                "Quantity" => item => item.Quantity,
                "UnitPrice" => item => item.UnitPrice, 
                "Discount" => item => item.Discount,
                "Account.Name" => item => item.Account.Name,
                "Business.Name" => item => item.Business.Name,
                _ => throw new NotImplementedException(
                    $"Sorting not implemented for {sortDefinition.SortBy} column in Line Items table.")
                };

            orderedQuery = sortDefinition.Descending
                ? orderedQuery.ThenByDescending(keySelector)
                : orderedQuery.ThenBy(keySelector);
        }

        return orderedQuery;
    }


    protected override void ReadOnlyRowClicked(DataGridRowClickEventArgs<LineItem> args)
    {
        Console.WriteLine("Customer Detail Row Clicked");
    }

    protected override LineItem BuildNewDefaultRow()
    {
        var customer = _customer ?? throw new ArgumentNullException(nameof(_customer), "Unable to add new line item to null customer.");

        return new LineItem
        {
            CustomerId = customer.Id,
            Quantity = 1,
            AccountId = Guid.Parse("29592A50-15B1-4F5C-A0FC-87AB8FAA11FF"),
            BusinessId = BusinessId.None
        };
    }

    protected override async Task OnAddButtonClicked()
    {
        var newDefaultLineItem = BuildNewDefaultRow();
        var parameters = new DialogParameters<AddNewLineItemForm>
        {
            { dialog => dialog.Validator, Validator },
            { dialog => dialog.Accounts, _accounts },
            { dialog => dialog.Businesses, _businesses},
            { dialog => dialog.NewDataRow, newDefaultLineItem }
        };

        var options = new DialogOptions
        {
            CloseButton = true,
            MaxWidth = MaxWidth.Large,
            FullWidth = true
        };

        var dialog = await DialogService.ShowAsync<AddNewLineItemForm>("Add new line item", parameters, options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            await AddRow((LineItem)result.Data);
        }
    }

}