using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Pika.DataLayer;

[SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This class is used by dotnet-ef tools for migrations without Dependency Injection")]
public class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<PikaDataContext>
{
    public PikaDataContext CreateDbContext(string[] args)
    {
        DatabaseConfig config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName)
            .AddJsonFile("Service/dev.config.json", false, true)
            .Build()
            .GetRequiredSection("database")
            .Get<DatabaseConfig>();

        return new PikaDataContext(new OptionsWrapper<DatabaseConfig>(config));
    }
}
