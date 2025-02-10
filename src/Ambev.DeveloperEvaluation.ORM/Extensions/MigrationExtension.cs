using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.ORM.Extensions;

/// <summary>
/// Provides extension methods for applying database migrations.
/// </summary>
public static class MigrationExtensions
{
    /// <summary>
    /// Applies any pending database migrations.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance.</param>
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
        var pendingMigrations = dbContext.Database.GetPendingMigrations();

        if (pendingMigrations.Any())
        {
            dbContext.Database.Migrate();
        }
    }
}
