using System.Security.Authentication;
using AccountsReceivable.BAL.Data;
using AccountsReceivable.BL.Models.Account;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;

namespace AccountsReceivable.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Azure AD Authentication
        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(
            options =>
            {
                builder.Configuration.Bind("AzureAd", options);
                options.Events ??= new OpenIdConnectEvents();
                options.Events.OnTokenValidated += OnTokenValidated;
            });
        builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();
        builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });

        // Service Dependencies
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
        builder.Services.AddMudServices();

        // Database Services
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options
                .UseSqlServer(builder.Configuration.GetConnectionString("Development") ?? string.Empty,
                    db => db.MigrationsAssembly("AccountsReceivable.BAL"));
        }, ServiceLifetime.Transient);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }

    private static async Task OnTokenValidated(TokenValidatedContext context)
    {
        var name = context.Principal?.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;
        var email = context.Principal?.Claims.FirstOrDefault(c => c.Type.Equals("preferred_username"))?.Value;

        if (name is null || email is null) throw new AuthenticationException();

        var dbContext = context.HttpContext.RequestServices.GetRequiredService<ApplicationDbContext>();
        var user = await dbContext
            .Set<User>()
            .FirstOrDefaultAsync(user => EF.Functions.Like(user.EmailAddress, email));

        if (user is null)
        {
            user = new User
            {
                EmailAddress = email,
                Name = name
            };

            await dbContext.Set<User>().AddAsync(user);
            await dbContext.SaveChangesAsync();
        }
    }
}