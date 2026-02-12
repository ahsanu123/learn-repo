using Microsoft.Data.Sqlite;
using Dapper;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using Migration2026;

namespace TestMigrations;

public class MigrationFixture : IDisposable
{
    public static string DataSource = "Data Source=test_db";

    public IServiceProvider ServiceProvider { get; private set; }
    public IMigrationRunner Runner { get; private set; }
    public SqliteConnection Conn { get; set; }

    public MigrationFixture()
    {
        Conn = new SqliteConnection(MigrationFixture.DataSource);
        Conn.Open();

        var services = new ServiceCollection()
            .AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                .AddSQLite()
                .WithGlobalConnectionString(DataSource)
                .ScanIn(typeof(CreatePriceTable).Assembly).For.Migrations())
            .AddLogging(lb => lb.AddFluentMigratorConsole());

        ServiceProvider = services.BuildServiceProvider(false);
        Runner = ServiceProvider.GetRequiredService<IMigrationRunner>();
    }

    public void Dispose()
    {
        Conn.Dispose();
        // ServiceProvider?.Dispose();
    }

}

public class TestUntilEmitenIdInPriceTable : IClassFixture<MigrationFixture>
{

    private MigrationFixture _fixture;

    public TestUntilEmitenIdInPriceTable(MigrationFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void TestMigrateUp()
    {
        _fixture.Runner.MigrateUp();

        var result = _fixture.Conn.Query("SELECT COUNT(*) from price;");
        Console.WriteLine(JsonSerializer.Serialize(result, new JsonSerializerOptions { WriteIndented = true }));
    }
}
