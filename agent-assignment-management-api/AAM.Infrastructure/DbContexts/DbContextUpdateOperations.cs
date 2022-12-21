using AAM.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;

namespace AAM.Infrastructure.DbContexts
{
    /// <summary>
    /// Contains a set of automatic operations against Database
    /// </summary>
    internal static class DbContextUpdateOperations
    {
        public static void UpdateDates(IEnumerable<EntityEntry<AuditableEntity>> changes)
        {
            DateTime now = DateTime.Now;
            foreach (var change in changes)
            {
                switch (change.State)
                {
                    case EntityState.Added:
                        change.Entity.DateCreated = now;
                        change.Entity.DateModified = now;
                        break;

                    case EntityState.Modified:
                        change.Entity.DateModified = now;
                        break;
                }
            }
        }

        public static void UpdateModifier(IEnumerable<EntityEntry<AuditableEntity>> changes, IHttpContextAccessor accessor)
        {
            var userId = accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? null;
            if (userId == null) return;
            foreach (var change in changes)
            {
                change.Entity.ModifiedBy = userId;
            }
        }
    }
}