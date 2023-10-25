using Microsoft.AspNetCore.Components;

namespace AccountsReceivable.API.Shared;

partial class RedirectToLogin
{
    protected readonly string BaseURL = "MicrosoftIdentity/Account";

    [Inject]
    protected virtual NavigationManager Navigation { get; set; } = default!;

    protected override void OnInitialized()
    {
        Navigation.NavigateTo($"{BaseURL}/SignIn");
    }
}