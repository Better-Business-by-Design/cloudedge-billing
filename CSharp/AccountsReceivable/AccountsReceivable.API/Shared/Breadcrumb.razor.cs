using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AccountsReceivable.API.Shared;

partial class Breadcrumb
{
    [Parameter] 
    public List<BreadcrumbItem>? Items { get; set; }

    protected override void OnInitialized()
    {
        StateHasChanged();
    }
}