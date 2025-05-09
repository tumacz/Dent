using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TheApp.Infrastructure.Persistence;

public class TheAppDbContextFactory : IDesignTimeDbContextFactory<TheAppDbContext>
{
	public TheAppDbContext CreateDbContext(string[] args)
	{
		var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

		var basePath = Path.Combine(Directory.GetCurrentDirectory(), "../TheApp.MVC");

		Console.WriteLine($"🔧 Ładowanie appsettings.json z: {basePath} ({environment})");

		var configuration = new ConfigurationBuilder()
			.SetBasePath(basePath)
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
			.Build();

		var connectionString = configuration.GetConnectionString("TheAppCS");

		if (string.IsNullOrWhiteSpace(connectionString))
		{
			throw new InvalidOperationException("❌ Brakuje connection stringa 'TheAppCS' w plikach konfiguracyjnych.");
		}

		var optionsBuilder = new DbContextOptionsBuilder<TheAppDbContext>();
		optionsBuilder.UseSqlServer(connectionString);

		return new TheAppDbContext(optionsBuilder.Options);
	}
}
