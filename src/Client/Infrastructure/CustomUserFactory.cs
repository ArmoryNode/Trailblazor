using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Security.Principal;
using static Trailblazor.Shared.Infrastructure.Authentication;

namespace Trailblazor.Client.Infrastructure
{
    public class CustomUserFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public CustomUserFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
        {
        }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);

            if (user.Identity is ClaimsIdentity identity && identity.IsAuthenticated)
            {
                string? value = identity!.FindFirst(CustomClaimTypes.Image)?.Value;
            }

            return user;
        }
    }
}
