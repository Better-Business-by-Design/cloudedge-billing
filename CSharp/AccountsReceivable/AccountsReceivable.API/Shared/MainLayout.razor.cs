using System.Security.Authentication;
using System.Security.Claims;
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

    private ClaimsPrincipal User;

    protected override async Task OnInitializedAsync()
    {
        var authenticationState = await Authentication.GetAuthenticationStateAsync();
        User = authenticationState.User;
        await base.OnInitializedAsync();
    }

}