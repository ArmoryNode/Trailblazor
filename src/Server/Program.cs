using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models.ODataErrors;
using System.Net.Http.Headers;
using System.Security.Claims;
using Trailblazor.Infrastructure.Identity;
using Trailblazor.Infrastructure.Persistence;
using Trailblazor.Server.Infrastructure;
using Trailblazor.Shared.Infrastructure;

using static Trailblazor.Server.Infrastructure.Constants.Authentication;
using static Trailblazor.Shared.Infrastructure.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var applicationDbConnectionString = builder.Configuration.GetConnectionString("ApplicationDbConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(applicationDbConnectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure and register Trailblazor DbContext
var cosmosDbConnectionString = builder.Configuration.GetConnectionString("CosmosDbConnection");
var cosmosDbOptions = builder.Configuration.GetRequiredSection(nameof(CosmosDbSettings)).Get<CosmosDbSettings>();
builder.Services.AddDbContext<TrailblazorDbContext>(options =>
    options.UseCosmos(cosmosDbConnectionString, cosmosDbOptions.DatabaseName));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddClaimsPrincipalFactory<CustomClaimsPrincipalFactory>();

builder.Services.AddIdentityServer()
    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
    {
        // Configure user image claim and identity resource.
        options.ApiResources.Single().UserClaims.Add(CustomClaimTypes.Image);
        options.IdentityResources["openid"].UserClaims.Add(CustomClaimTypes.Image);
    });

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

        // Microsoft accounts do not include the photo URL claim, so we need to get the user's photo from the Microsoft Graph.
        microsoftOptions.Events.OnCreatingTicket = async (context) =>
        {
            if (context.Identity is ClaimsIdentity identity)
            {
                try
                {
                    // Create an http client and pass the user's access token.
                    // We'll need this to make a request to the Microsoft Graph on behalf of the user.
                    using var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(OidcConstants.AuthenticationSchemes.AuthorizationHeaderBearer, context.AccessToken);

                    // Create a new Graph client to make getting resources from the Microsoft Graph easier.
                    var graphClient = new Microsoft.Graph.GraphServiceClient(httpClient);

                    // Get a stream of the user's profile image.
                    using var photoStream = await graphClient.Me.Photos["120x120"].Content.GetAsync(cancellationToken: context.HttpContext.RequestAborted);
                    context.Principal.AddBase64ImageClaim(photoStream);
                }
                catch (ODataError)
                {
                    // If there is an error getting the profile image from the Microsoft Graph, then use the placeholder.
                    using var photoStream = File.OpenRead(Path.Combine(builder.Environment.WebRootPath, "img/placeholder-profile-image.svg"));
                    context.Principal.AddBase64ImageClaim(photoStream);
                }
            }
        };

        microsoftOptions.SaveTokens = true;
    });

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

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
    var authenticationSection = builder.Configuration.GetSection(nameof(Constants.Authentication))
                                                     .GetSection(provider);

    options.ClientId = authenticationSection.GetValue<string>(Credentials.ClientId);
    options.ClientSecret = authenticationSection.GetValue<string>(Credentials.ClientSecret);
}