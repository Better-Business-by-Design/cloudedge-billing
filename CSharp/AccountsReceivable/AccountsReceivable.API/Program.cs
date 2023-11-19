using System.Security.Authentication;
using System.Text.Json.Serialization;
using AccountsReceivable.BAL.Data;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using MudBlazor.Services;
using Newtonsoft.Json;

namespace AccountsReceivable.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Azure AD Authentication
        builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApp(
            options =>
            { builder.Configuration.Bind("AzureAd", options); });
        builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();
        builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });
        
        builder.Services.AddMvc().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
        
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
}