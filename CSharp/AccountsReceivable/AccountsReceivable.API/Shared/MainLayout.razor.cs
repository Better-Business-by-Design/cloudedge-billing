using System.Security.Authentication;
using AccountsReceivable.BAL.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AccountsReceivable.API.Shared;

partial class MainLayout
{

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    protected virtual AuthenticationStateProvider Authentication { get; set; } = default!;
    
}