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
        var domain = ReadPikaFile(parser, domainId);
        await RunScrapersAndUpdateDomain(domain, scrappers);
        await domainRepo.Create(domain);
    }

    private static Domain ReadPikaFile(PikaParser parser, string domainId)
    {
        var file = $"Domains/{domainId}.pika";
        Console.WriteLine($"Parsing File {file}...");
        TextReader stream = new StreamReader(file);
        return parser.Parse(stream);
    }

    private static async Task RunScrapersAndUpdateDomain(Domain domain, IEnumerable<IScrapper> scrappers)
    {
        foreach (var scrapper in scrappers.Where(s => s.DomainId == domain.Id))
        {
            Console.WriteLine($"Scrapping {scrapper.DomainId}...");
            await scrapper.ScrapeInto(domain);
        }
    }
}
