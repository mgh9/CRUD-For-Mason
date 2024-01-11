using Microsoft.Extensions.Configuration;

namespace Mc2.CrudTest.Infrastructure.Data.Options
{
    public sealed class DatabaseOptions
    {
        public static DatabaseOptions Create(IConfiguration configuration)
        {
            var databaseSettings = new DatabaseOptions();
            configuration.GetSection("Database").Bind(databaseSettings);
            return databaseSettings;
        }

        public string? SqlConnectionString { get; set; }
    }
}
