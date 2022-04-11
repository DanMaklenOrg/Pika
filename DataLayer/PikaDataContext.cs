using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace Pika.DataLayer;

public class PikaDataContext : DbContext
{
    public PikaDataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<DomainModel> Domain { get; set; }

    public static void RegisterService(IServiceCollection services)
    {
        services.AddDbContext<PikaDataContext>(SetOptions);
    }

    internal static void SetOptions(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("User ID=postgres;Password=mysecretpassword;Host=localhost;Port=55401;Database=Pika");
    }

}
