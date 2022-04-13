using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pika.DataLayer.Model;

namespace Pika.DataLayer;

public class PikaDataContext : DbContext
{
    internal PikaDataContext(DbContextOptions options) : base(options)
    {
    }

    internal DbSet<DomainDbModel> Domains { get; set; } = null!;

    internal DbSet<EntryDbModel> Entries { get; set; } = null!;

    internal DbSet<ObjectiveDbModel> Objectives { get; set; } = null!;

    public static void RegisterService(IServiceCollection services)
    {
        services.AddDbContext<PikaDataContext>(SetOptions);
    }

    internal static void SetOptions(DbContextOptionsBuilder options)
    {
        options.UseNpgsql("User ID=postgres;Password=mysecretpassword;Host=localhost;Port=55401;Database=Pika");
    }
}
