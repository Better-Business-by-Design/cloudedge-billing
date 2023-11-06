using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Index
{
    private Invoices _invoicesDataGrid;
    private Pricing _pricingDataGrid;

    private int _numInvoices;
    private int _numPricingSchedules;

    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", null, true)
    };


    private string _table = "documents";

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    protected virtual NavigationManager Navigation { get; set; } = default!;

    private void SwitchTable(string table)
    {
        _table = table;
        StateHasChanged();
    }

    protected async Task InvoicesStaticFilter(IQueryable<Document> fullQuery)
    {
        fullQuery = fullQuery.Where(document => document.StatusId == StatusId.Pending);
    }
    
    protected async Task PricingStaticFilter(IQueryable<Schedule> fullQuery)
    {
        fullQuery = fullQuery.Where(schedule => schedule.StatusId == StatusId.Pending);
    }
    
    protected async Task InvoicesServerReloaded(int totalItems)
    {
        _numInvoices = totalItems;
    }

    protected async Task PricingServerReloaded(int totalItems)
    {
        _numPricingSchedules = totalItems;
    }
    
}