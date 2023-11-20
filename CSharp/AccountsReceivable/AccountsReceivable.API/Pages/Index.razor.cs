
using AccountsReceivable.BL.Models.Application;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Index
{

    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", null, true)
    };

    private static IQueryable<Invoice> StaticLastMonthFilter(IQueryable<Invoice> invoices) 
    {
        return invoices.Where(i => i.DateTime.Month == (DateTime.Now.Month - 1));
    }
    
}