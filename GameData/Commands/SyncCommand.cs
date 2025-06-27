using Cocona;
using Pika.GameData.GameScrapper;
using Pika.Model;
using Pika.PikaLang;
using Pika.Repository;

namespace Pika.GameData.Commands;

public static class SyncCommand
{
    public static void AddSyncCommand(this CoconaApp app)
    {
        app.AddCommand("sync", Sync);
    }

    private static async Task Sync([Argument] string gameId, PikaParser parser, IGameRepo gameRepo, SteamScrapper steamScrapper, IEnumerable<IScrapper> scrappers)
    {
        var game = ReadPikaFile(parser, gameId);

        if (game.SteamAppId.HasValue) await ScrapeSteamAchievements(steamScrapper, game);

        await RunScrapersAndUpdateGame(game, scrappers);

        Console.WriteLine("Saving game...");
        await gameRepo.Create(game);
        Console.WriteLine("Sync complete!");
    }

    private static Game ReadPikaFile(PikaParser parser, string gameId)
    {
        var file = $"Games/{gameId}.pika";
        Console.WriteLine($"Parsing File {file}...");
        TextReader stream = new StreamReader(file);
        return parser.Parse(stream);
    }

    private static async Task ScrapeSteamAchievements(SteamScrapper scrapper, Game game)
    {
        ArgumentNullException.ThrowIfNull(game.SteamAppId);
        Console.WriteLine("Scrapping Steam Achievements...");
        await scrapper.ScrapeInto(game, game.SteamAppId.Value);
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
