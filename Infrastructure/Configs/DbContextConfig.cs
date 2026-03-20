using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace Infrastructure.Configs
{
    public static class DbContextConfig
    {
        public static void ConfigureSqLite(this DbContextOptionsBuilder options)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.db");
            options.UseSqlite($"Data Source={dbPath}");
        }

        public static void ConfigureInMemory(this DbContextOptionsBuilder options, SqliteConnection connection)
        {
            options.UseSqlite(connection);
        }

        public static void ConfigureSqlServer(this DbContextOptionsBuilder options, string connectionString)
        {
            options.UseSqlServer(connectionString);
        }
    }
}
