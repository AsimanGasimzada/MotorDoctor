using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace MotorDoctor.DataAccess.Interceptors;
public class BaseEntityInterceptor : SaveChangesInterceptor
{
    private readonly IHttpContextAccessor _contextAccessor;

    public BaseEntityInterceptor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntity(eventData.Context);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntity(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    public void UpdateEntity(DbContext? context)
    {
        if (context is null) return;

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State is EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.UpdatedAt = DateTime.UtcNow;
                entry.Entity.CreatedBy = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "undifined";
                entry.Entity.UpdatedBy = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "undifined"; ;
                entry.Entity.IsDeleted = false;
            }
            if (entry.State is EntityState.Modified)
            {
                entry.Entity.UpdatedBy = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "undifined";
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
            if (entry.State is EntityState.Deleted)
            {
                entry.State = EntityState.Modified; // soft delete
                entry.Entity.IsDeleted = true;
                entry.Entity.UpdatedBy = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value ?? "undifined";
                entry.Entity.UpdatedAt = DateTime.UtcNow;

            }
        }
    }
}
