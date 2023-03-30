using Microsoft.EntityFrameworkCore;
using odata.models;

namespace odata.repository.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : BaseModel, new()
    {
        public Repository(EducationContext educationContext)
        {
            Context = educationContext;
        }

        protected EducationContext Context { get; }

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            entity.Created = DateTime.UtcNow;
            entity.Updated = DateTime.UtcNow;

            Context.Add(entity);

            await Context.SaveChangesAsync(cancellationToken);
        }

        public virtual async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await Context.Set<T>().FindAsync(new object[] { id }, cancellationToken);

            if (entity is null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            entity.IsDeleted = true;
            entity.Updated = DateTime.UtcNow;

            await Context.SaveChangesAsync(cancellationToken);
        }

        public virtual Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        {
            return Context.Set<T>().AsNoTracking().Where(x => x.Id == id).AnyAsync(cancellationToken);
        }

        public virtual IQueryable<T> Get(Guid id)
        {
            return Context.Set<T>().AsNoTracking().Where(x => x.Id == id);
        }

        public virtual IQueryable<T> Get()
        {
            return Context.Set<T>().AsNoTracking();
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            var originalEntity = await Context.Set<T>().FindAsync(new object[] { entity.Id }, cancellationToken);

            if (originalEntity is null)
            {
                throw new ArgumentNullException(nameof(entity.Id));
            }

            entity.Created = originalEntity.Created;
            entity.Updated = DateTime.UtcNow;

            Context.Entry(entity).State = EntityState.Modified;

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}