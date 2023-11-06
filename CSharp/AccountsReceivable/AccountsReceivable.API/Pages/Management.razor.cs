using System.Security.Authentication;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Account;
using AccountsReceivable.BL.Models.Application;
using AccountsReceivable.BL.Models.Enum;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using MudBlazor;

namespace AccountsReceivable.API.Pages;

partial class Management
{
    [CascadingParameter]
    private User? User { get; set; }
    
    private User[] Users { get; set; } = Array.Empty<User>();
    
    

    private readonly Func<AnimalType, string> _animalTypeConverter = m => m?.DisplayName ?? string.Empty;
    private ManagementPage _managementPage = ManagementPage.Users;
    private readonly List<BreadcrumbItem> _breadcrumb = new()
    {
        new BreadcrumbItem("Home", ""),
        new BreadcrumbItem("Management", null, true)
    };

    private readonly Func<Meatwork, string> _meatworkConverter = m => m?.Name ?? string.Empty;

    [Inject]
    protected virtual ISnackbar Snackbar { get; set; } = default!;
    
    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        Users = await DbContext.Set<User>().ToArrayAsync();
    }
    
    private async Task SaveChanges()
    {
        if (User is not { RoleId: RoleId.Administrator }) throw new AuthenticationException();
        
        await DbContext.SaveChangesAsync();
        
        Snackbar.Clear();
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomCenter;
        Snackbar.Add("Your changes have been saved!");
    }
}

internal enum ManagementPage
{
    Users,
    Upcharge,
    Premiums,
    Deductions
}