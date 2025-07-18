using Dapper;
using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioAEVO.Infrastructure.Migrations
{
    public class DatabaseMigration
    {
        public static void Migrate(string connectionString, IServiceProvider serviceProvider)
        {
            EnsureDatabaseExists(connectionString);
            MigrationDatabase(serviceProvider);
        }

        private static void EnsureDatabaseExists(string connectionString)
        {
            var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            var dbName = connectionStringBuilder.InitialCatalog;

            connectionStringBuilder.Remove("Database");

            using var connection = new SqlConnection(connectionStringBuilder.ConnectionString);

            var parameters = new DynamicParameters();
            parameters.Add("Name", dbName);

            var records = connection.Query("SELECT * FROM sys.databases WHERE name = @Name", parameters);

            if (records.Any() == false)
            {
                connection.Execute($"CREATE DATABASE [{dbName}]");
            }

        }

        private static void MigrationDatabase(IServiceProvider serviceProvider)
        {
            serviceProvider.GetRequiredService<IMigrationRunner>();

            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.ListMigrations();
            runner.MigrateUp();
        }
    }
}
