using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Scs.Domain.Entities.Common;
using Scs.Domain.Entities;

namespace Scs.Infrastructure.Persistence;

public sealed class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData.Context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        var context = eventData.Context;

        var entries = context.ChangeTracker
            .Entries<ISoftDelete>()
            .Where(e => e.State == EntityState.Deleted)
            .ToList();

        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;
            entry.Entity.DeletedOnUtc = DateTime.UtcNow;

            if (entry.Entity is Faculty || entry.Entity is Student)
            {
                var profileId = (Guid)entry.Property("Id").CurrentValue;

                context.Set<ApplicationUser>()
                    .Where(u => u.Id == profileId)
                    .ExecuteUpdate(setters => setters
                        .SetProperty(u => u.IsDeleted, true)
                        .SetProperty(u => u.IsActive, false)
                        .SetProperty(u => u.DeletedOnUtc, DateTime.UtcNow));
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}