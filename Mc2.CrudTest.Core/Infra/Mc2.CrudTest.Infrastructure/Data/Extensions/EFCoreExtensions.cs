using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Infrastructure.Data.Extensions
{
    public static class EFCoreExtensions
    {
        public static IApplicationBuilder UseMigration<TContext>(this IApplicationBuilder app, bool isDevelopmentEnvironment) where TContext : DbContext
        {
            MigrateDatabaseAsync<TContext>(app.ApplicationServices).GetAwaiter().GetResult();

            return app;
        }

        private static async Task MigrateDatabaseAsync<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
        {
            using IServiceScope scope = serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<TContext>().Database.MigrateAsync();
        }
    }
}
