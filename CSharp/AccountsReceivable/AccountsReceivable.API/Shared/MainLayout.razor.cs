using System.Security.Authentication;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Account;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace AccountsReceivable.API.Shared;

partial class MainLayout
{
    private User? User { get; set; }

    [Inject]
    protected virtual ApplicationDbContext DbContext { get; set; } = default!;

    [Inject]
    protected virtual AuthenticationStateProvider Authentication { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        var userClaims = (await Authentication.GetAuthenticationStateAsync()).User;
        User = await DbContext.Set<User>()
            .FindAsync(userClaims.Claims.FirstOrDefault(c => c.Type.Equals("preferred_username"))?.Value);

        if (User is null) throw new AuthenticationException();

        await base.OnInitializedAsync();
    }
}