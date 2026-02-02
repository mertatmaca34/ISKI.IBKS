using ISKI.IBKS.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.Persistence.Interceptors;

public class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;

        if (dbContext == null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entries = dbContext.ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            var now = DateTime.UtcNow;

            if (entry.Entity is AuditableEntity<Guid> auditableUtc)
            {
                if (entry.State == EntityState.Added)
                {
                    auditableUtc.GetType().GetProperty("CreatedAt")?.SetValue(auditableUtc, now);
                }
                auditableUtc.GetType().GetProperty("ModifiedAt")?.SetValue(auditableUtc, now);
            }


        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
