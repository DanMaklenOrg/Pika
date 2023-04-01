using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

    public DbSet<AchievementDbModel> Achievements { get; set; } = default!;

    public DbSet<EntityDbModel> Entities { get; set; } = default!;

    public DbSet<TagDbModel> Tags { get; set; } = default!;

    public DbSet<ActionDbModel> Actions { get; set; } = default!;

    public DbSet<UserAchievementDbModel> UserAchievements { get; set; } = default!;

    public DbSet<EntryDbModel> Entries { get; set; } = default!;

    public DbSet<ObjectiveDbModel> Objectives { get; set; } = default!;

    public DbSet<ProjectDbModel> Projects { get; set; } = default!;

    public DbSet<ObjectiveTargetDbModel> ObjectiveTargets { get; set; } = default!;

    public DbSet<ProgressDbModel> Progress { get; set; } = default!;

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

        modelBuilder.Entity<EntityDbModel>()
            .HasMany(model => model.Tags)
            .WithMany(model => model.Entities)
            .UsingEntity(builder => builder.ToTable("EntityTag"));

        modelBuilder.Entity<TagDbModel>()
            .HasMany(model => model.Actions)
            .WithMany(model => model.Tags)
            .UsingEntity(builder => builder.ToTable("TagAction"));

        modelBuilder.Entity<UserAchievementDbModel>()
            .HasKey(model => new { model.UserId, model.AchievementId });

        modelBuilder.Entity<ObjectiveDbModel>()
            .HasMany(model => model.Targets)
            .WithMany(entry => entry.Objectives)
            .UsingEntity<ObjectiveTargetDbModel>();

        modelBuilder.Entity<ProgressDbModel>()
            .HasOne<ObjectiveTargetDbModel>()
            .WithMany()
            .HasForeignKey("ObjectiveId", "TargetId");
        modelBuilder.Entity<ProgressDbModel>()
            .HasKey("UserId", "ObjectiveId", "TargetId");
    }
}
