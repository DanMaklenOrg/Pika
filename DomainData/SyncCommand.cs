using Cocona;
using Pika.Converter;
using Pika.Model;
using Pika.Repository;

namespace Pika.DomainData;

public static class SyncCommand
{
    public static void AddSyncCommand(this CoconaApp app)
    {
        app.AddCommand("sync", Sync);
    }

    private static async Task Sync([Argument] string domainId, PikaConverter converter, IDomainRepo domainRepo)
    {
        var fileDomains = ReadAllDomains(converter, domainId);
        var domains = MergeScrappedDomains(fileDomains);
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
        return fileDomains.GroupBy(d => d.Id).Select(g => g.Aggregate((a, b) => new Domain
        {
            Id = a.Id,
            Name = string.IsNullOrEmpty(a.Name) ? b.Name : a.Name,
            Classes = a.Classes.UnionBy(b.Classes, e => e.Id).ToList(),
            Entities = a.Entities.UnionBy(b.Entities, e => e.Id).ToList(),
            Projects = a.Projects.UnionBy(b.Projects, e => e.Name).ToList(),
            Stats = a.Stats.UnionBy(b.Stats, e => e.Id).ToList(),
        })).ToList();
    }
}
