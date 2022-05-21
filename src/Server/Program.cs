using Azure.Identity;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Security.Claims;
using Trailblazor.Server.Data;
using Trailblazor.Server.Infrastructure;
using Trailblazor.Server.Models;

using static Trailblazor.Server.Infrastructure.Constants;
using static Trailblazor.Server.Infrastructure.Constants.Authentication;
using static Trailblazor.Shared.Infrastructure.Authentication;

using Graph = Microsoft.Graph;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

builder.Services.AddAuthentication()
    .AddIdentityServerJwt()
    .AddGoogle(googleOptions =>
    {
        ConfigureExternalProvider(builder, googleOptions, ExternalProviders.Google);

        // Map inbound user claims https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/additional-claims#map-user-data-keys-and-create-claims
        googleOptions.ClaimActions.MapJsonKey(CustomClaimTypes.Image, JwtClaimTypes.Picture, "url");

        googleOptions.SaveTokens = true;
    })
    .AddMicrosoftAccount(microsoftOptions =>
    {
        ConfigureExternalProvider(builder, microsoftOptions, ExternalProviders.Microsoft);

        microsoftOptions.ClaimActions.MapJsonKey(CustomClaimTypes.Image, JwtClaimTypes.Picture, "url");

        microsoftOptions.SaveTokens = true;
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpClient(HttpClientNames.UserImageClient);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();

static void ConfigureExternalProvider(WebApplicationBuilder builder, OAuthOptions options, string provider)
{
    var authenticationSection = builder.Configuration.GetSection(nameof(Authentication))
                                                     .GetSection(provider);

    options.ClientId = authenticationSection.GetValue<string>(Credentials.ClientId);
    options.ClientSecret = authenticationSection.GetValue<string>(Credentials.ClientSecret);
}