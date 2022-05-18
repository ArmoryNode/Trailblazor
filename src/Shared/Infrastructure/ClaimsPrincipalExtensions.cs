﻿using IdentityModel;
using System.ComponentModel;
using System.Security.Claims;

using static Trailblazor.Shared.Infrastructure.Authentication;

#nullable disable

namespace Trailblazor.Shared.Infrastructure
{
    public static class ClaimsPrincipalExtensions
    {
        public static string UserId(this ClaimsPrincipal principal)
            => principal.GetUserId<string>();

        public static string Image(this ClaimsPrincipal principal)
            => principal.FindFirst(CustomClaimTypes.Image)?.Value;

        public static string FirstName(this ClaimsPrincipal principal)
            => principal.FindFirst(CustomClaimTypes.FirstName)?.Value;

        public static string Email(this ClaimsPrincipal principal)
            => principal.FindFirst(JwtClaimTypes.Name)?.Value;

        public static T GetUserId<T>(this ClaimsPrincipal principal)
        {
            if (principal?.Identity is null)
                throw new ArgumentNullException(nameof(principal));

            if (!principal.Identity.IsAuthenticated)
                throw new ArgumentException("Principal is not authenticated.", nameof(principal));

            var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId is null)
                throw new Exception($"`{ClaimTypes.NameIdentifier}` claim not found for user: {principal.Identity.Name}");

            if (typeof(T) == typeof(string) ||
                typeof(T) == typeof(int) ||
                typeof(T) == typeof(long) ||
                typeof(T) == typeof(Guid))
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));

                return (T)converter.ConvertFromInvariantString(userId);
            }

            throw new InvalidOperationException("User Id is invalid.");
        }

        public static bool TryGetUserId<T>(this ClaimsPrincipal principal, out T userId)
        {
            try
            {
                userId = principal.GetUserId<T>();
                return true;
            }
            catch (Exception)
            {
                userId = default;
                return false;
            }
        }
    }
}