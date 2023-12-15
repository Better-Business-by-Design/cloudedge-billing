using Microsoft.AspNetCore.Components;

namespace CloudEdgeBilling.API.Shared;

partial class RedirectToLogin
{
    private const string BaseUrl = "MicrosoftIdentity/Account";

    [Inject] protected virtual NavigationManager Navigation { get; set; } = default!;

    protected override void OnInitialized()
    {
        Navigation.NavigateTo($"{BaseUrl}/SignIn");
    }
}