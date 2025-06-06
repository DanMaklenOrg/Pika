using Cocona;
using Pika.Model;
using Pika.PikaLang;
using Pika.Repository;

namespace Pika.GameData;

public static class SyncCommand
{
    public static void AddSyncCommand(this CoconaApp app)
    {
        app.AddCommand("sync", Sync);
    }

    private static async Task Sync([Argument] string gameId, PikaParser parser, IGameRepo gameRepo, IEnumerable<IScrapper> scrappers)
    {
        var game = ReadPikaFile(parser, gameId);
        await RunScrapersAndUpdateGame(game, scrappers);
        await gameRepo.Create(game);
    }

    private static Game ReadPikaFile(PikaParser parser, string gameId)
    {
        var file = $"Games/{gameId}.pika";
        Console.WriteLine($"Parsing File {file}...");
        TextReader stream = new StreamReader(file);
        return parser.Parse(stream);
    }

    private static async Task RunScrapersAndUpdateGame(Game game, IEnumerable<IScrapper> scrappers)
    {
        foreach (var scrapper in scrappers.Where(s => s.GameId == game.Id))
        {
            Console.WriteLine($"Scrapping {scrapper.GameId}...");
            await scrapper.ScrapeInto(game);
        }
    }
}
