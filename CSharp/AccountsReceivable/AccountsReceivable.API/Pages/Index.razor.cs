using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Index
{

    private Customers _customersDataGrid;

    private int _numCustomers;

    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", null, true)
    };

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    protected virtual NavigationManager Navigation { get; set; } = default!;
    
    protected async Task CustomersServerReloaded(int totalItems)
    {
        _numCustomers = totalItems;
    }
    
}