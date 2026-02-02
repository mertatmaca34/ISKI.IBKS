using ISKI.IBKS.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ISKI.IBKS.Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    private static readonly ConcurrentDictionary<Type, LambdaExpression> SoftDeleteFilterCache = new();

    public static void ApplySoftDeleteQueryFilter(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            if (!typeof(ISoftDeletable).IsAssignableFrom(clrType))
                continue;

            var filter = SoftDeleteFilterCache.GetOrAdd(clrType, CreateSoftDeleteFilter);

            modelBuilder.Entity(clrType).HasQueryFilter(filter);
        }
    }

    private static LambdaExpression CreateSoftDeleteFilter(Type entityClrType)
    {
        var parameter = Expression.Parameter(entityClrType, "e");

        var isDeletedProperty = Expression.Call(
            typeof(EF),
            nameof(EF.Property),
            [typeof(bool)],
            parameter,
            Expression.Constant(nameof(ISoftDeletable.IsDeleted)));

        var body = Expression.Not(isDeletedProperty);

        return Expression.Lambda(body, parameter);
    }
}

