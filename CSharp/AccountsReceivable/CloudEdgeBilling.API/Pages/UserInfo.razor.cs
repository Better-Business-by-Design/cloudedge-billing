using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CloudEdgeBilling.API.Pages;

partial class UserInfo
{
    private string? authMessage;
    private IEnumerable<Claim> claims = Enumerable.Empty<Claim>();
    private string? surname;
    private ClaimsPrincipal? User;

    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    private async Task GetClaimsPrincipalData()
    {
        var authState = await AuthenticationStateProvider
            .GetAuthenticationStateAsync();
        User = authState.User;

        if (User.Identity is not null && User.Identity.IsAuthenticated)
        {
            authMessage = $"{User.Identity.Name} is authenticated.";
            claims = User.Claims;
            surname = User.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value;
        }
        else
        {
            authMessage = "The user is NOT authenticated.";
        }

        StateHasChanged();
    }
}