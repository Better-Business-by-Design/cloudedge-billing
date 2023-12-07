using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CloudEdgeBilling.API.Pages;

partial class UserInfo
{
    private string? _authMessage;
    private IEnumerable<Claim> _claims = Enumerable.Empty<Claim>();
    private string? _surname;
    private ClaimsPrincipal? _user;

    [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    private async Task GetClaimsPrincipalData()
    {
        var authState = await AuthenticationStateProvider
            .GetAuthenticationStateAsync();
        _user = authState.User;

        if (_user.Identity is not null && _user.Identity.IsAuthenticated)
        {
            _authMessage = $"{_user.Identity.Name} is authenticated.";
            _claims = _user.Claims;
            _surname = _user.FindFirst(c => c.Type == ClaimTypes.Surname)?.Value;
        }
        else
        {
            _authMessage = "The user is NOT authenticated.";
        }

        StateHasChanged();
    }
}