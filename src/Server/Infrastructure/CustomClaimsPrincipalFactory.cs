﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using Trailblazor.Infrastructure.Identity;
using static Trailblazor.Shared.Infrastructure.Authentication;

namespace Trailblazor.Server.Infrastructure
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public CustomClaimsPrincipalFactory(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }

        protected async override Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            if (!string.IsNullOrWhiteSpace(user.ImageUrl))
            {
                var imageUrlClaim = identity.FindFirst(c => c.Type == CustomClaimTypes.Image);

                if (imageUrlClaim is not null)
                    identity.RemoveClaim(imageUrlClaim);

                identity.AddClaim(new(CustomClaimTypes.Image, user.ImageUrl));
            }

            if (!string.IsNullOrWhiteSpace(user.FirstName))
            {
                var firstNameClaim = identity.FindFirst(c => c.Type == ClaimTypes.GivenName);

                if (firstNameClaim is not null)
                    identity.RemoveClaim(firstNameClaim);

                identity.AddClaim(new(CustomClaimTypes.FirstName, user.FirstName));
            }

            return identity;
        }
    }
}
