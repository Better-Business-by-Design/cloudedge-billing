using System.Linq.Expressions;
using AccountsReceivable.API.Shared;
using AccountsReceivable.BL.Models.Application;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Pricing : DataGridPage<Schedule>
{
    
    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Pricing Schedules", null, true)
    };

    protected override IQueryable<Schedule> BuildFullQuery()
    {
        return DbContext.Schedules
            .AsNoTracking()
            .Include(schedule => schedule.Status)
            .Include(schedule => schedule.Meatwork);
    }

    protected override IQueryable<Schedule> FilterFullQuery(
        IQueryable<Schedule> fullQuery, 
        IEnumerable<IFilterDefinition<Schedule>> filterDefinitions)
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
            
            Expression<Func<Schedule, bool>> fullPredicate;
            
            if (filterDefinition.FieldType.IsString)
            {
                var value = filterDefinition.Value?.ToString() ?? string.Empty;
                
                Expression<Func<Schedule, string>> selectPredicate = filterDefinition.Title switch
                {
                    "Works" => schedule => schedule.Meatwork.Name,
                    _ => throw new NotImplementedException()
                };
                
                fullPredicate = selectPredicate.Compose(GenerateStringLogicPredicate(logicOperator, value));
            } 
            else if (filterDefinition.FieldType.IsBoolean)
            {
                throw new NotImplementedException();
            } 
            else if (filterDefinition.FieldType.IsEnum)
            {
                var value = filterDefinition.Value!;
                Expression<Func<Schedule, Enum>> selectPredicate = filterDefinition.Title switch
                {
                    "Status" => schedule => schedule.Status.Id,
                    _ => throw new NotImplementedException()
                };
                
                fullPredicate = selectPredicate.Compose(GenerateEnumLogicPredicate(logicOperator, value));
            } 
            else if (filterDefinition.FieldType.IsGuid)
            {
                throw new NotImplementedException();
            } 
            else if (filterDefinition.FieldType.IsNumber)
            {
                throw new NotImplementedException();
            } 
            else if (filterDefinition.FieldType.IsDateTime)
            {
                var value = Convert.ToDateTime(filterDefinition.Value ?? new DateTime());
                Expression<Func<Schedule, DateTime>> selectPredicate = filterDefinition.Title switch
                {
                    "Start Date" => schedule => schedule.StartDate,
                    "End Date" => schedule => schedule.EndDate,
                    _ => throw new NotImplementedException()
                };
                fullPredicate = selectPredicate.Compose(GenerateDateTimeLogicPredicate(logicOperator, value));
            }
            else
            {
                throw new NotImplementedException();
            }
            
            filteredQuery = filteredQuery.Where(fullPredicate);
        }
        return filteredQuery;
    }

    protected override IOrderedQueryable<Schedule> OrderFilteredQuery(
        IQueryable<Schedule> filteredQuery, 
        IEnumerable<SortDefinition<Schedule>> sortDefinitions)
    {
        var orderedQuery = filteredQuery.OrderBy(schedule => true);
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var sortDefinition in sortDefinitions)
        {
            Expression<Func<Schedule, object>> keySelector = sortDefinition.SortBy switch
            {
                "StartDate" => schedule => schedule.StartDate,
                "EndDate" => schedule => schedule.EndDate,
                "Meatwork.Name" => schedule => schedule.Meatwork.Name,
                "StatusId" => schedule => schedule.Status.Id,
                _ => throw new NotImplementedException()
            };

            orderedQuery = sortDefinition.Descending
                ? orderedQuery.ThenByDescending(keySelector)
                : orderedQuery.ThenBy(keySelector);
        }

        return orderedQuery;
    }

    protected override void RowClicked(DataGridRowClickEventArgs<Schedule> args)
    {
        Navigation.NavigateTo($"pricing/{args.Item.Id}");
    }
}