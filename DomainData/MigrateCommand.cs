using Cocona;
using Pika.DataLayer.Dao;
using Pika.Model;
using Pika.Repository;

namespace Pika.DomainData;

public static class MigrateCommand
{
    public static void AddMigrateCommand(this CoconaApp app)
    {
        app.AddCommand("migrate", Migrate);
    }

    private static async Task Migrate(IDomainRepo domainRepo, IUserStatsRepo userStatsRepo)
    {
        await foreach (var domain in GetAllDomains(domainRepo))
        {
            MigrateDomain(domain);
            await domainRepo.Create(domain);
            var stats = await userStatsRepo.Get("DanMaklen", domain.Id);
            if (stats is not null) MigrateUserStat(domain, stats.Value);
        }
        Console.Out.WriteLine("All Domains migrated successfully!");
    }

    private static void MigrateDomain(Domain domain)
    {
    }

    private static void MigrateUserStat(Domain domain, UserStats stats)
    {
        foreach (var stat in stats.EntityStats)
        {
            stat.StatId = stat.StatId.FullyQualifiedId switch
            {
                "_/owned" => "owned",
                "_/unlocked" => "unlocked",
                "_/achieved" => "achieved",
                _ => stat.StatId,
            };
        }
    }

    private static async IAsyncEnumerable<Domain> GetAllDomains(IDomainRepo domainRepo)
    {
        var domains = await domainRepo.GetAll();
        foreach (var d in domains)
        {
            yield return (await domainRepo.Get(d.Id.FullyQualifiedId))!;
        }
    }
}
