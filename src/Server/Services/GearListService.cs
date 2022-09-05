using Microsoft.EntityFrameworkCore;
using Trailblazor.Domain.Common;
using Trailblazor.Domain.Entities.Gear;
using Trailblazor.Infrastructure.Persistence;
using Trailblazor.Shared.Models;
using Trailblazor.Shared.ViewModels;

namespace Trailblazor.Server.Services
{
    public class GearListService : IDataService<GearListViewModel>
    {
        private readonly TrailblazorDbContext _dbContext;

        public GearListService(TrailblazorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Save(GearListViewModel viewModel, CancellationToken cancellationToken)
        {
            var gearList = new GearList(viewModel);

            _dbContext.Add(gearList);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<GearListViewModel>> GetAll(BaseQueryOptions queryOptions, CancellationToken cancellationToken)
        {
            var query = _dbContext.GearLists.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(queryOptions.SearchText))
                query = query.Where(g => g.SearchText.Contains(queryOptions.SearchText));

            return await (from entity in query
                          select new GearListViewModel
                          {
                              Id = entity.Identifier,
                              Name = entity.Name,
                              Description = entity.Description,
                              Favorite = entity.Favorite,
                              Order = entity.Order,
                              GearCollections = (List<GearCollection>)entity.GearCollections
                          }).ToListAsync(cancellationToken);
        }

        public async Task<GearListViewModel?> GetById(Guid entityId, CancellationToken cancellationToken)
        {
            var query = from entity in _dbContext.GearLists.AsNoTracking()
                        where entity.Identifier == entityId
                        select new GearListViewModel
                        {
                            Id = entity.Identifier,
                            Name = entity.Name,
                            Description = entity.Description,
                            Favorite = entity.Favorite,
                            Order = entity.Order,
                            GearCollections = (List<GearCollection>)entity.GearCollections
                        };

            return await query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool?> Delete(Guid entityId, CancellationToken cancellationToken)
        {
            try
            {
                var entityToRemove = await _dbContext.GearLists.FindAsync(entityId, cancellationToken);

                if (entityToRemove is null)
                    return null;

                _dbContext.GearLists.Remove(entityToRemove);

                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch (Exception ex)
            {
                switch (ex)
                {
                    case DbUpdateConcurrencyException:
                    case DbUpdateException:
                    case TaskCanceledException:
                        return false;
                    default:
                        throw;
                }
            }
        }
    }
}
