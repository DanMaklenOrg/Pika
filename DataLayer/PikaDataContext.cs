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

    // public DbSet<ObjectiveTargetDbModel> ObjectiveTargets { get; set; } = default!;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ObjectiveDbModel>()
            .HasMany(model => model.Targets)
            .WithMany(entry => entry.Objectives)
            .UsingEntity(
                o => o.HasOne(typeof(EntryDbModel)).WithMany().HasForeignKey("TargetId"),
                o => o.HasOne(typeof(ObjectiveDbModel)).WithMany().HasForeignKey("ObjectiveId"),
                o => o.ToTable("ObjectiveTargets"));
    }
}
