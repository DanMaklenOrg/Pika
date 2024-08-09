using Cocona;
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

    private static async Task Test(IDomainRepo domainRepo)
    {
        await foreach (var domain in GetAllDomains(domainRepo))
        {
            TestDomain(domain);
        }
        Console.Out.WriteLine("All Domains passed the test!");
    }

    private static void TestDomain(Domain domain)
    {
    }

    private static async IAsyncEnumerable<Domain> GetAllDomains(IDomainRepo domainRepo)
    {
        var domains = await domainRepo.GetAll();
        foreach (var d in domains)
        {
            yield return (await domainRepo.Get(d.Id.FullyQualifiedId))!.Value;
        }
    }
}
