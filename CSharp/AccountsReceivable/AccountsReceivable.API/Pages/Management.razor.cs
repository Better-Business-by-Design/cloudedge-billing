using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Management
{
    private readonly Func<AnimalType, string> _animalTypeConverter = m => m?.DisplayName ?? string.Empty;
    private ManagementPage _managementPage = ManagementPage.Users;
    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Management", null, true)
    };

    private readonly Func<Meatwork, string> _meatworkConverter = m => m?.Name ?? string.Empty;

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    private void LoadPage(ManagementPage managementPage)
    {
        _managementPage = managementPage;

        StateHasChanged();
    }
}

internal enum ManagementPage
{
    Users,
    Upcharge,
    Premiums,
    Deductions
}