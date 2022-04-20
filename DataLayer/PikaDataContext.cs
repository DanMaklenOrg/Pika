using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using Pika.DataLayer.Model;

namespace Pika.DataLayer;

public class PikaDataContext : DbContext
{
    private readonly DatabaseConfig config;

    public PikaDataContext(IOptions<DatabaseConfig> config)
    {
        this.config = config.Value;
    }

    public DbSet<DomainDbModel> Domains { get; set; } = default!;

    public DbSet<EntryDbModel> Entries { get; set; } = default!;

    public DbSet<ObjectiveDbModel> Objectives { get; set; } = default!;

    public DbSet<ProjectDbModel> Projects { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Database = "Pika",
            Host = this.config.Host,
            Port = this.config.Port,
            Username = this.config.Username,
            Password = this.config.Password,
        }.ToString();

        optionsBuilder.UseNpgsql(connectionString);
    }
}
