using Mc2.CrudTest.Infrastructure.Data;
using Mc2.CrudTest.Infrastructure.Data.Extensions;

namespace Mc2.CrudTest.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void InitializeDatabase(this WebApplication webApplication, IWebHostEnvironment environment)
    {
        try
        {
            webApplication.UseMigration<ApplicationDbContext>(environment.IsDevelopment());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
