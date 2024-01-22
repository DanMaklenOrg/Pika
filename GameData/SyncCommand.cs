using Cocona;
using Pika.Converter;
using Pika.Model;

namespace Pika.GameData;

public static class SyncCommand
{
    public static void AddSyncCommand(this CoconaApp app)
    {
        app.AddCommand("sync", Sync);
    }

    private static void Sync(PikaConverter converter)
    {
        var fileDomains = ReadAllDomains(converter);
        var subDomains = MergeScrappedDomains(fileDomains);
        var domains = MergeSubDomains(subDomains);
    }

    private static List<Domain> ReadAllDomains(PikaConverter converter)
    {
        List<Domain> domains = new();
        foreach (var file in Directory.EnumerateFiles("Domains/", "*.yaml", SearchOption.AllDirectories))
        {
            Console.WriteLine($"Parsing File {file}...");
            TextReader stream = new StreamReader(file);
            var domainId = Path.GetFileNameWithoutExtension(file).Replace(".scraped", string.Empty);
            var d = converter.Read(stream, domainId);
            Console.WriteLine($"Parsed Domain Id: {d.Id}, Name: {d.Name}.");
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
            Entities = a.Entities.UnionBy(b.Entities, e => e.Id.FullyQualifiedId).ToList(),
            Stats = a.Stats.UnionBy(b.Stats, e => e.Id.FullyQualifiedId).ToList(),
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
