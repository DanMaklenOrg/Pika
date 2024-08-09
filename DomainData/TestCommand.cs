using Cocona;
using Pika.DataLayer.Dao;
using Pika.Model;
using Pika.Repository;

namespace Pika.DomainData;

public static class TestCommand
{
    private class TestFailureException(Domain domain, string reason) : Exception($"Domain {domain.Id} ({domain.Name}) failed a test: {reason}");

    public static void AddTestCommand(this CoconaApp app)
    {
        app.AddCommand("test", Test);
    }

    private static async Task Test(IDomainRepo domainRepo, IUserStatsRepo userStatsRepo)
    {
        await foreach (var domain in GetAllDomains(domainRepo))
        {
            Console.WriteLine($"Testing {domain.Id} ({domain.Name})...");
            TestDomain(domain);
            var userStat = await userStatsRepo.Get("DanMaklen", domain.Id);
            if (userStat is not null) TestUserStat(domain, userStat.Value);
            Console.WriteLine($"Testing {domain.Id} ({domain.Name})... Passed!");
        }
        Console.Out.WriteLine("All Domains passed the test!");
    }

    private static void TestDomain(Domain domain)
    {
    }

    private static void TestUserStat(Domain domain, UserStats userStats)
    {
    }

    private static async IAsyncEnumerable<Domain> GetAllDomains(IDomainRepo domainRepo)
    {
        var domains = await domainRepo.GetAll();
        foreach (var d in domains)
        {
            yield return (await domainRepo.Get(d.Id))!;
        }
    }
}
