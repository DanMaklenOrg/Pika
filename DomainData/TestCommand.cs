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
        foreach (var c in domain.Classes)
        {
            foreach (var sid in c.Stats)
                if (sid.Domain.FullyQualifiedId != domain.Id.FullyQualifiedId)
                    throw new TestFailureException(domain, $"class {c.Id} has a stat ({sid}) from an external domain");
            foreach (var tid in c.Tags)
                if (tid.Domain.FullyQualifiedId != domain.Id.FullyQualifiedId)
                    throw new TestFailureException(domain, $"class {c.Id} has a tag ({tid}) from an external domain");
        }

        foreach (var e in domain.Entities)
        {
            if(e.Class.Domain.FullyQualifiedId != domain.Id.FullyQualifiedId)
                throw new TestFailureException(domain, $"entity {e.Id} has a class ({e.Class}) from an external domain");
            foreach (var sid in e.Stats)
                if (sid.Domain.FullyQualifiedId != domain.Id.FullyQualifiedId)
                    throw new TestFailureException(domain, $"entity {e.Id} has a stat ({sid}) from an external domain");
            foreach (var tid in e.Tags)
                if (tid.Domain.FullyQualifiedId != domain.Id.FullyQualifiedId)
                    throw new TestFailureException(domain, $"entity {e.Id} has a tag ({tid}) from an external domain");
        }

        foreach (var p in domain.Projects)
        {
            foreach (var o in p.Objectives)
            {
                foreach (var r in o.Requirements)
                {
                    if(r.Class.Domain.FullyQualifiedId != domain.Id.FullyQualifiedId)
                        throw new TestFailureException(domain, $"Project Objective Requirement {p.Id}.{o.Title} has a class ({r.Class}) from an external domain");
                    if(r.Stat.Domain.FullyQualifiedId != domain.Id.FullyQualifiedId)
                        throw new TestFailureException(domain, $"Project Objective Requirement {p.Id}.{o.Title} has a stat ({r.Stat}) from an external domain");
                }
            }
        }
    }

    private static void TestUserStat(Domain domain, UserStats userStats)
    {
        foreach (var es in userStats.EntityStats)
        {
            if(es.EntityId.Domain.FullyQualifiedId != domain.Id.FullyQualifiedId)
                throw new TestFailureException(domain, $"Entity Stat {es.EntityId} - {es.StatId} has an entity ({es.EntityId}) from an external domain");
            if(es.StatId.Domain.FullyQualifiedId != domain.Id.FullyQualifiedId)
                throw new TestFailureException(domain, $"Entity Stat {es.EntityId} - {es.StatId} has a stat ({es.StatId}) from an external domain");
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
