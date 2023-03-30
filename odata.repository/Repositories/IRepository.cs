using odata.models;

namespace odata.repository.Repositories
{
    public interface IRepository<T>
        where T : BaseModel
    {
        public Task AddAsync(T entity, CancellationToken cancellationToken);

        public Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);

        public IQueryable<T> Get(Guid id);

        public IQueryable<T> Get();

        public Task UpdateAsync(T entity, CancellationToken cancellationToken);
    }
}