using Blog.Application.Helpers;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Blog.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        IUserContext _userContext;
        public AuditableEntityInterceptor(IUserContext userContext)
        {
            _userContext = userContext;
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? context)
        {
            if (context == null) return;

            var currentUser = _userContext.GetUser();

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = currentUser.UserId;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added ||
                    entry.State == EntityState.Modified ||
                    entry.HasChangedOwnedEntities())
                {
                    entry.Entity.LastModifiedBy = currentUser.UserId;
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }
    }

    public static class Entensions
    {
        public static bool HasChangedOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
                r.TargetEntry != null &&
                r.TargetEntry.Metadata.IsOwned() &&
                (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));
    }
}
