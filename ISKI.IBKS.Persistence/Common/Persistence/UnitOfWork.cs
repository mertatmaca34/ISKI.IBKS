using ISKI.IBKS.Application.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ISKI.IBKS.Persistence.Common.Persistence;

public sealed class UnitOfWork(DbContext db) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct) => db.SaveChangesAsync(ct);
}
