using System.Security.Claims;
using CloudEdgeBilling.BAL.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CloudEdgeBilling.API.Shared;

partial class MainLayout
{
    private const string BaseUrl = "MicrosoftIdentity/Account";
    
    private ClaimsPrincipal _user = null!;

    [Inject] protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject] protected virtual AuthenticationStateProvider Authentication { get; set; } = default!;
    
    [Inject] protected virtual NavigationManager Navigation { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var authenticationState = await Authentication.GetAuthenticationStateAsync();
        _user = authenticationState.User;
        await base.OnInitializedAsync();
    }

    private void Logout()
    {
        Navigation.NavigateTo($"{BaseUrl}/SignOut", forceLoad: true);
    }
}