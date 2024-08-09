using System.Text.Json;
using System.Text.Json.Serialization;
using Cocona;
using Pika.Model;
using Pika.PikaLang;
using Pika.Repository;

namespace Pika.DomainData;

public static class SyncCommand
{
    public static void AddSyncCommand(this CoconaApp app)
    {
        app.AddCommand("sync", Sync);
    }

    private static async Task Sync([Argument] string domainId, PikaParser parser, IDomainRepo domainRepo, IEnumerable<IScrapper> scrappers)
    {
        var domains = ReadAllPikaDomainFiles(parser, domainId);
        var domain = domains.Single(); // TODO: Support multiple pika files

        await RunAllScrapersAndUpdateDomain(domain, scrappers);

        await domainRepo.Create(domain);
        DumpDomain(domain);
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

    private static async Task RunAllScrapersAndUpdateDomain(Domain domain, IEnumerable<IScrapper> scrappers)
    {
        foreach (var scrapper in scrappers.Where(s => s.DomainId == domain.Id))
        {
            Console.WriteLine($"Scrapping {scrapper.DomainId}...");
            await scrapper.ScrapeInto(domain);
        }
    }

    // Useful for noticing unintended changes. This dump is not used anywhere else.
    // The dump is pushed into VCS to simplify checking for diffs in IDE. It can be mostly ignored if the diff looks satisfying.
    private static void DumpDomain(Domain domain)
    {
        var filePath = $"DomainDump/{domain.Id}.dump.json";
        Console.WriteLine($"Dumping {domain.Id} ({domain.Name}) into {filePath}...");

        domain.Classes.Sort((a, b) => a.Id.ToString().CompareTo(b.Id));
        domain.Entities.Sort((a, b) => a.Id.ToString().CompareTo(b.Id));
        domain.Projects.Sort((a, b) => a.Name.ToString().CompareTo(b.Name));

        var json = JsonSerializer.Serialize(domain, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = true,
        });
        File.WriteAllText(filePath, json);
    }
}
