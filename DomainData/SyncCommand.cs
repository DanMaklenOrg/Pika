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
}
