using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pika.DataLayer;

[SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This class is used by dotnet-ef tools for migrations without Dependency Injection")]
public class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<PikaDataContext>
{
    public PikaDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        PikaDataContext.SetOptions(optionsBuilder);
        return new PikaDataContext(optionsBuilder.Options);
    }
}
