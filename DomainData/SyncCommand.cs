using Cocona;
using Pika.Converter;
using Pika.Model;
using Pika.PikaLang;
using Pika.Repository;

namespace Pika.DomainData;

public static class SyncCommand
{
    public static void AddSyncCommand(this CoconaApp app)
    {
        app.AddCommand("sync", Sync);
        app.AddCommand("sync_old", SyncOld);
    }

    private static async Task Sync([Argument] string domainId, PikaParser parser, IDomainRepo domainRepo)
    {
        var domains = ReadAllPikaDomainFiles(parser, domainId);

        if (domains.Count != 1) throw new NotSupportedException();

        return; // Test Run
        foreach (var domain in domains)
        {
            await domainRepo.Create(domain);
        }
    }

    private static List<Domain> ReadAllPikaDomainFiles(PikaParser parser, string domainId)
    {
        List<Domain> domains = [];
        foreach (var file in Directory.EnumerateFiles($"Domains/{domainId}", "*.pika", SearchOption.AllDirectories))
        {
            Console.WriteLine($"Parsing File {file}...");
            TextReader stream = new StreamReader(file);
            var d = parser.Parse(stream);
            domains.Add(d);
        }

        return domains;
    }

    private static async Task SyncOld([Argument] string domainId, PikaConverter converter, IDomainRepo domainRepo)
    {
        var fileDomains = ReadAllDomains(converter, domainId);
        var subDomains = MergeScrappedDomains(fileDomains);
        var domains = MergeSubDomains(subDomains);
        foreach (var domain in domains)
        {
            await domainRepo.Create(domain);
        }
    }

    private static List<Domain> ReadAllDomains(PikaConverter converter, string domainId)
    {
        List<Domain> domains = [];
        foreach (var file in Directory.EnumerateFiles($"Domains/{domainId}", "*.yaml", SearchOption.AllDirectories))
        {
            Console.WriteLine($"Parsing File {file}...");
            TextReader stream = new StreamReader(file);
            var d = converter.Read(stream);
            domains.Add(d);
        }

        return domains;
    }

    private static List<Domain> MergeScrappedDomains(List<Domain> fileDomains)
    {
        return fileDomains.GroupBy(d => d.Id.FullyQualifiedId).Select(g => g.Aggregate((a, b) => new Domain
        {
            Id = a.Id,
            Name = string.IsNullOrEmpty(a.Name) ? b.Name : a.Name,
            Classes = a.Classes.UnionBy(b.Classes, e => e.Id.FullyQualifiedId).ToList(),
            Entities = a.Entities.UnionBy(b.Entities, e => e.Id.FullyQualifiedId).ToList(),
            Projects = a.Projects.UnionBy(b.Projects, e => e.Name).ToList(),
            Stats = a.Stats.UnionBy(b.Stats, e => e.Id.FullyQualifiedId).ToList(),
            Tags = a.Tags.UnionBy(b.Tags, e => e.Id.FullyQualifiedId).ToList(),
        })).ToList();
    }

    private static List<Domain> MergeSubDomains(List<Domain> subDomains)
    {
        var rootDomains = subDomains.Where(d => !d.Id.IsSubDomainId).ToList();
        foreach (var domain in rootDomains)
            domain.SubDomains.AddRange(subDomains.Where(d => d.Id.Id == domain.Id.Id && d.Id.IsSubDomainId));
        return rootDomains;
    }
}
