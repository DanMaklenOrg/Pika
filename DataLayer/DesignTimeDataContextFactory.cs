using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pika.DataLayer;

public class DesignTimeDataContextFactory : IDesignTimeDbContextFactory<PikaDataContext>
{
    public PikaDataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder();
        PikaDataContext.SetOptions(optionsBuilder);
        return new PikaDataContext(optionsBuilder.Options);
    }
}
