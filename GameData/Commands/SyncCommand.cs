using Cocona;
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

    private static async Task Sync([Argument] string gameId, PikaParser parser, IGameRepo gameRepo, IEnumerable<IScrapper> scrappers)
    {
        // List<string> list =
        // [
        //     "crosscells",
        //     "hacknet",
        //     "hexcells_plus",
        //     "nodebuster",
        //     "squarecells",
        //     "dungeon_souls",
        //     "hades",
        //     "middle_earth_shadow_of_mordor",
        //     "palworld",
        //     "time_clickers",
        //     "dwarfs",
        //     "hexcells",
        //     "middle_earth_shadow_of_war",
        //     "shapez",
        //     "to_the_core",
        //     "factorio",
        //     "hexcells_infinite",
        //     "minecraft_border_hoarder",
        //     "spec_ops_the_line",
        //     "vampire_survivors",
        // ];
        //
        // foreach (var g in list)
        // {
        //     gameId = g;
        var game = ReadPikaFile(parser, gameId);
        await RunScrapersAndUpdateGame(game, scrappers);
        await gameRepo.Create(game);
        // }
        Console.WriteLine("Sync complete");
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
