using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Pika.DataLayer;

[SuppressMessage("ReSharper", "UnusedType.Global", Justification = "This class is used by dotnet-ef tools for migrations without Dependency Injection")]
public class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<PikaDataContext>
{
    public PikaDataContext CreateDbContext(string[] args)
    {
        if (args.Length != 4) throw new ArgumentException("Expected 4 arguments. <Host> <Port> <Username> <Password>");
        var config = new DatabaseConfig
        {
            Host = args[0],
            Port = int.Parse(args[1]),
            Username = args[2],
            Password = args[3],
        };

        return new PikaDataContext(new OptionsWrapper<DatabaseConfig>(config));
    }
}
