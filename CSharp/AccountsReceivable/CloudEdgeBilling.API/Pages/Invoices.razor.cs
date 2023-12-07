using System.Linq.Expressions;
using CloudEdgeBilling.API.Shared;
using CloudEdgeBilling.BL.Models.Application;
using MudBlazor;

namespace CloudEdgeBilling.API.Pages;

public partial class Invoices : DataGridPage<Invoice>
{
    private readonly Dictionary<string, SortDefinition<Invoice>> _sortDefinitions =
        new()
        {
            {
                "Date", new SortDefinition<Invoice>(
                    "DateTime",
                    true,
                    0,
                    x => x.DateTime)
            }
        };

    protected override List<BreadcrumbItem> Breadcrumb { get; set; } = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Invoices", null, true)
    };

    protected override IQueryable<Invoice> BuildFullQuery()
    {
        return DbContext.Invoices;
    }

    protected override IQueryable<Invoice> FilterFullQuery(IQueryable<Invoice> fullQuery,
        IEnumerable<IFilterDefinition<Invoice>> filterDefinitions)
    {
        var filteredQuery = fullQuery;
        foreach (var filterDefinition in filterDefinitions)
        {
            if (filterDefinition.Operator is null) continue;
            var logicOperator = filterDefinition.Operator!;

            if (filterDefinition.Value is null &&
                filterDefinition.Operator is not ("is empty" or "is not empty")) continue;

            Expression<Func<Invoice, bool>> fullPredicate;

            // Annoying that this can't be a switch
            if (filterDefinition.FieldType.IsString)
            {
                var value = filterDefinition.Value?.ToString() ?? string.Empty;

                Expression<Func<Invoice, string>> selectPredicate = filterDefinition.Title switch
                {
                    "Customer" => invoice => invoice.CustomerName,
                    "Xero Contact Name" => invoice => invoice.XeroContactName,
                    "Plan" => invoice => invoice.PlanName ?? string.Empty,
                    _ => throw new NotImplementedException(
                        $"{filterDefinition.Title} not implemented in String filters.")
                };

                var logicPredicate = GenerateStringLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            }
            else if (filterDefinition.FieldType.IsBoolean)
            {
                throw new NotImplementedException(
                    $"No boolean filtering implemented for Invoices table, including not for {filterDefinition.Title}");
            }
            else if (filterDefinition.FieldType.IsEnum)
            {
                throw new NotImplementedException(
                    $"No enum filtering implemented for Invoices table, including not for {filterDefinition.Title}");
            }
            else if (filterDefinition.FieldType.IsGuid)
            {
                var value = Guid.Parse(filterDefinition.Value?.ToString() ??
                                       throw new ArgumentNullException(nameof(filterDefinitions)));

                Expression<Func<Invoice, Guid>> selectPredicate = filterDefinition.Title switch
                {
                    "Domain UUID" => invoice => invoice.DomainUuid,
                    _ => throw new NotImplementedException($"{filterDefinition.Title} not implemented in Guid filters.")
                };

                var logicPredicate = GenerateGuidLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            }
            else if (filterDefinition.FieldType.IsNumber)
            {
                var value = Convert.ToDecimal(filterDefinition.Value ?? 0);

                Expression<Func<Invoice, decimal>> selectPredicate = filterDefinition.Title switch
                {
                    "Customer Id" => invoice => Convert.ToDecimal(invoice.CustomerId),
                    "Landline Mobile" => invoice => invoice.LandlineMobileSumCost,
                    "Landline National" => invoice => invoice.LandlineNationalSumCost,
                    "Landline International" => invoice => invoice.LandlineInternationalSumCost,
                    "Toll Free Landline" => invoice => invoice.TollFreeFromLandlineSumCost,
                    "Toll Free Mobile" => invoice => invoice.TollFreeFromMobileSumCost,
                    "Total VOIP Cost" => invoice => invoice.TotalVoipCost,
                    "Total Toll Free Cost" => invoice => invoice.TotalTollFreeCost,
                    "Total Cost" => invoice => invoice.TotalCost,
                    _ => throw new NotImplementedException()
                };

                var logicPredicate = GenerateDecimalLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            }
            else if (filterDefinition.FieldType.IsDateTime)
            {
                var value = Convert.ToDateTime(filterDefinition.Value ?? new DateTime());

                Expression<Func<Invoice, DateTime>> selectPredicate = filterDefinition.Title switch
                {
                    "Date" => invoice => invoice.DateTime,
                    _ => throw new NotImplementedException(
                        $"{filterDefinition.Title} not implemented in String filters.")
                };

                var logicPredicate = GenerateDateTimeLogicPredicate(logicOperator, value);

                fullPredicate = selectPredicate.Compose(logicPredicate);
            }
            else
            {
                throw new NotImplementedException(
                    $"No {filterDefinition.FieldType} filtering implemented for Invoices table.");
            }

            filteredQuery = filteredQuery.Where(fullPredicate);
        }

        return filteredQuery;
    }

    protected override IOrderedQueryable<Invoice> OrderFilteredQuery(IQueryable<Invoice> filteredQuery,
        IEnumerable<SortDefinition<Invoice>> sortDefinitions)
    {
        var orderedQuery = filteredQuery.OrderBy(invoice => true);
        // ReSharper disable once LoopCanBeConvertedToQuery
        foreach (var sortDefinition in sortDefinitions)
        {
            Expression<Func<Invoice, object>> keySelector = sortDefinition.SortBy switch
            {
                "CustomerId" => invoice => invoice.CustomerId,
                "DomainUuid" => invoice => invoice.DomainUuid,
                "CustomerName" => invoice => invoice.CustomerName,
                "XeroContactName" => invoice => invoice.XeroContactName,
                "DateTime" => invoice => invoice.DateTime,
                "LandlineMobileSumCost" => invoice => invoice.LandlineMobileSumCost,
                "LandlineNationalSumCost" => invoice => invoice.LandlineNationalSumCost,
                "LandlineInternationalSumCost" => invoice => invoice.LandlineInternationalSumCost,
                "TollFreeFromLandlineSumCost" => invoice => invoice.TollFreeFromLandlineSumCost,
                "TollFreeFromMobileSumCost" => invoice => invoice.TollFreeFromMobileSumCost,
                "TotalVoipCost" => invoice => invoice.TotalVoipCost,
                "TotalTollFreeCost" => invoice => invoice.TotalTollFreeCost,
                "TotalCost" => invoice => invoice.TotalCost,
                "PlanName" => invoice => invoice.PlanName ?? string.Empty,
                "PlanPrice" => invoice => invoice.PlanPrice ?? 0M,
                _ => throw new NotImplementedException(
                    $"Sorting not implemented for {sortDefinition.SortBy} column in Invoices table.")
            };

            orderedQuery = sortDefinition.Descending
                ? orderedQuery.ThenByDescending(keySelector)
                : orderedQuery.ThenBy(keySelector);
        }

        return orderedQuery;
    }
}