using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Domain.Configs
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
    }
}
