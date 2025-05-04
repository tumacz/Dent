using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheApp.Infrastructure.Persistence;

public class TheAppDbContextFactory : IDesignTimeDbContextFactory<TheAppDbContext>
{
    public TheAppDbContext CreateDbContext(string[] args)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

        var basePathCandidates = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), "../TheApp.MVC"),
            Path.Combine(Directory.GetCurrentDirectory(), "../TheApp.Infrastructure"),
            Path.Combine(Directory.GetCurrentDirectory(), "../../TheApp.MVC")
        };

        string? foundPath = basePathCandidates
            .FirstOrDefault(path => File.Exists(Path.Combine(path, "appsettings.json")));

        if (foundPath == null)
        {
            throw new FileNotFoundException("❌ Nie znaleziono appsettings.json.");
        }

        Console.WriteLine($"🔧 Ładowanie appsettings.json z: {foundPath} ({environment})");

        var configuration = new ConfigurationBuilder()
            .SetBasePath(foundPath)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .Build();

        var connectionString = configuration.GetConnectionString("TheAppCS");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException($"❌ Brakuje connection stringa 'TheAppCS' w appsettings.{environment}.json lub appsettings.json.");
        }

        var optionsBuilder = new DbContextOptionsBuilder<TheAppDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new TheAppDbContext(optionsBuilder.Options);
    }
}
