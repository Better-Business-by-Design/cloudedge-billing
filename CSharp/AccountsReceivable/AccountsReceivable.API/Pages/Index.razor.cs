
using AccountsReceivable.BL.Models.Application;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Index
{

    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", null, true)
    };

    private static IQueryable<Customer> StaticIsActiveFilter(IQueryable<Customer> customers) 
    {
        return customers.Where(c => c.IsActive);
    }
    
}