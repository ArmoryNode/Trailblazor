using Trailblazor.Domain.Common;

namespace Trailblazor.Server.Services
{
    public interface IDataService<TViewModel>
    {
        Task<TViewModel> GetById(Guid entityId, CancellationToken cancellationToken);
        Task<TViewModel[]> GetAll(BaseQueryOptions queryOptions, CancellationToken cancellationToken);
        Task Delete(Guid entityId, CancellationToken cancellationToken);
        Task Save(TViewModel viewModel, CancellationToken cancellationToken);
    }
}
