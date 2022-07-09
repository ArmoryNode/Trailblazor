using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Trailblazor.Infrastructure.Identity;

namespace Trailblazor.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}