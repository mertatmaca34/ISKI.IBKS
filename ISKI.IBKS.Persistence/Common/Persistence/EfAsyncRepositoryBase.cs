using ISKI.IBKS.Application.Common.Persistence;
using ISKI.IBKS.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Persistence.Common.Persistence;

public abstract class EfAsyncRepositoryBase<TEntity, TId, TContext>(TContext context)
    : IAsyncRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TContext : DbContext
{
    protected readonly TContext Context = context;
    protected readonly DbSet<TEntity> Set = context.Set<TEntity>();

    public Task AddAsync(TEntity entity, CancellationToken ct)
        => Set.AddAsync(entity, ct).AsTask();

    public Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken ct)
        => Set.AddRangeAsync(entities, ct);

    public void Update(TEntity entity) => Set.Update(entity);

    public void Remove(TEntity entity) => Set.Remove(entity);

    public Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct)
        => Set.FindAsync([id], ct).AsTask();

    public Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        => Set.AsNoTracking().FirstOrDefaultAsync(predicate, ct);

    public Task<List<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
        => Set.AsNoTracking().Where(predicate).ToListAsync(ct);
}
