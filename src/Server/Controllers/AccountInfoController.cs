using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System.Net.Http.Headers;
using System.Net.Mime;
using Trailblazor.Shared.Infrastructure;
using static IdentityModel.OidcConstants;
using static Trailblazor.Server.Infrastructure.Constants;

namespace Trailblazor.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/account")]
    public class AccountInfoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _environment;

        public AccountInfoController(IHttpClientFactory httpClientFactory, IWebHostEnvironment environment)
        {
            _httpClientFactory = httpClientFactory;
            _environment = environment;
        }

        [HttpGet("photo")]
        public async ValueTask<IActionResult> Photo(Guid userId, CancellationToken cancellationToken)
        {
            var imageClaimValue = User.Image();
            string contentType = string.Empty;
            Stream? image;

            // If there is no image claim, we assume that the user is authenticated using a Microsoft account.
            // This logic may change in the future, depending on how newly added external providers handle profile images.
            if (string.IsNullOrWhiteSpace(imageClaimValue))
            {
                // Get the authenticated user's access token so we can authenticated with the Microsoft Graph.
                var userToken = await HttpContext.GetTokenAsync(TokenTypes.AccessToken);
                var authProvider = new DelegateAuthenticationProvider((message) =>
                {
                    message.Headers.Authorization = new AuthenticationHeaderValue(AuthenticationSchemes.AuthorizationHeaderBearer, userToken);
                    return Task.CompletedTask;
                });

                // Create the HTTP client and Graph clients.
                var httpClient = GraphClientFactory.Create(authProvider);
                var graphClient = new GraphServiceClient(httpClient);

                // Get the result from the user photo endpoint.
                var response = await graphClient
                    .Me
                    .Photo
                    .Content
                    .Request().GetResponseAsync(cancellationToken);

                if (response.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    // If the image is not found, then return the placeholder image
                    var userPlaceholderImage = Path.Combine(_environment.WebRootPath, "img/user_placeholder.svg");
                    image = System.IO.File.OpenRead(userPlaceholderImage);
                    contentType = "image/svg+xml";
                }
                else
                {
                    // Get the image content as a stream.
                    image = response.Content.ReadAsStream(cancellationToken);

                    // Microsoft Graph always returns images as JPEGs.
                    contentType = "image/jpeg";
                }

            }
            else
            {
                var httpClient = _httpClientFactory.CreateClient(HttpClientNames.UserImageClient);
                image = await httpClient.GetStreamAsync(imageClaimValue, cancellationToken);
                contentType = "image/png";
            }

            return File(image, contentType);
        }
    }
}
