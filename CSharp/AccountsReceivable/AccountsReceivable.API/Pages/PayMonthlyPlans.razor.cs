using System.Linq.Expressions;
using AccountsReceivable.API.Shared;
using AccountsReceivable.BL.Models.Application;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class PayMonthlyPlans : EditableDataGridPage<PayMonthlyPlan>
{
    
    protected override List<BreadcrumbItem> Breadcrumb { get; set; } = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Pay Monthly Plans", null, true)
    };

    protected override IQueryable<PayMonthlyPlan> BuildFullQuery()
    {
        return DbContext.PayMonthlyPlans;
    }
    
    protected override IQueryable<PayMonthlyPlan> FilterFullQuery(
        IQueryable<PayMonthlyPlan> fullQuery, 
        IEnumerable<IFilterDefinition<PayMonthlyPlan>> filterDefinitions)
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
            
            Expression<Func<PayMonthlyPlan, bool>> fullPredicate;
            
            // Annoying that this can't be a switch
            if (filterDefinition.FieldType.IsString)
            {
                var value = filterDefinition.Value?.ToString() ?? string.Empty;
                
                Expression<Func<PayMonthlyPlan, string>> selectPredicate = filterDefinition.Title switch
                {
                    "Name" => plan => plan.PlanName,
                    _ => throw new NotImplementedException($"{filterDefinition.Title} not implemented in String filters.")
                };

                var logicPredicate = GenerateStringLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsBoolean)
            {
                throw new NotImplementedException(
                    $"No boolean filtering implemented for PayMonthlyPlans table, including not for {filterDefinition.Title}");
            } 
            else if (filterDefinition.FieldType.IsEnum)
            {
                throw new NotImplementedException(
                    $"No enum filtering implemented for PayMonthlyPlans table, including not for {filterDefinition.Title}");
            } 
            else if (filterDefinition.FieldType.IsGuid)
            {
                throw new NotImplementedException(
                    $"No guid filtering implemented for PayMonthlyPlans table, including not for {filterDefinition.Title}");
            } 
            else if (filterDefinition.FieldType.IsNumber)
            {
                var value = Convert.ToDecimal(filterDefinition.Value ?? 0);
                
                Expression<Func<PayMonthlyPlan, decimal>> selectPredicate = filterDefinition.Title switch
                {
                    "ID" => plan => plan.PlanId,
                    "Local Size" => plan => Convert.ToDecimal(plan.LocalSize ?? 0),
                    "National Size" => plan => Convert.ToDecimal(plan.NationalSize ?? 0),
                    "Mobile Size" => plan => Convert.ToDecimal(plan.MobileSize ?? 0),
                    "International Size" => plan => Convert.ToDecimal(plan.InternationalSize ?? 0),
                    "Toll Free Landline Size" => plan => Convert.ToDecimal(plan.TollFreeLandlineSize ?? 0),
                    "Toll Free Mobile Size" => plan => Convert.ToDecimal(plan.TollFreeMobileSize ?? 0),
                    "Price" => plan => plan.Price,
                    "Min Price" => plan => plan.MinPrice ?? 0m,
                    _ => throw new NotImplementedException()
                };

                var logicPredicate = GenerateDecimalLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            } 
            else if (filterDefinition.FieldType.IsDateTime)
            {
                throw new NotImplementedException(
                    $"No datetime filtering implemented for PayMonthlyPlans table, including not for {filterDefinition.Title}");
            }
            else
            {
                throw new NotImplementedException(
                    $"No {filterDefinition.FieldType} filtering implemented for PayMonthlyPlans table.");
            }
            filteredQuery = filteredQuery.Where(fullPredicate);
        }

        return filteredQuery;
    }

    protected override IOrderedQueryable<PayMonthlyPlan> OrderFilteredQuery(
        IQueryable<PayMonthlyPlan> filteredQuery, 
        IEnumerable<SortDefinition<PayMonthlyPlan>> sortDefinitions)
    {
        var orderedQuery = filteredQuery.OrderBy(plan => true);
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var sortDefinition in sortDefinitions)
        {
            Expression<Func<PayMonthlyPlan, object>> keySelector = sortDefinition.SortBy switch
            {
                "PlanId" => plan => plan.PlanId,
                "PlanName" => plan => plan.PlanName,
                "LocalSize" => plan => plan.LocalSize ?? 0,
                "NationalSize" => plan => plan.NationalSize ?? 0,
                "MobileSize" => plan => plan.MobileSize ?? 0,
                "InternationalSize" => plan => plan.InternationalSize ?? 0,
                "TollFreeLandlineSize" => plan => plan.TollFreeLandlineSize ?? 0,
                "TollFreeMobileSize" => plan => plan.TollFreeMobileSize ?? 0,
                "Price" => plan => plan.Price,
                "Min Price" => plan => plan.MinPrice ?? 0,
                _ => throw new NotImplementedException(
                    $"Sorting not implemented for {sortDefinition.SortBy} column in PayMonthlyPlans table.")
            };

            orderedQuery = sortDefinition.Descending
                ? orderedQuery.ThenByDescending(keySelector)
                : orderedQuery.ThenBy(keySelector);
        }

        return orderedQuery;
    }

    protected override Task OnAddButtonClicked()
    {
        throw new NotImplementedException();
    }

    protected override void ReadOnlyRowClicked(DataGridRowClickEventArgs<PayMonthlyPlan> args)
    {
        Console.WriteLine("Pay Monthly Plan row clicked!");
    }

    protected override async Task<PayMonthlyPlan> BuildNewDefaultRow()
    {
        return new PayMonthlyPlan();
    }
}
