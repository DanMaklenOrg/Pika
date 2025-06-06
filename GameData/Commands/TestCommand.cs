using Cocona;
using Pika.Model;
using Pika.Repository;

namespace Pika.GameData.Commands;

public static class TestCommand
{
    private class TestFailureException(Game game, string details) : Exception($"Game {game.Id} ({game.Name}) failed the test: {details}");

    public static void AddTestCommand(this CoconaApp app)
    {
        app.AddCommand("test", Test);
    }

    private static async Task Test(IGameRepo gameRepo, IUserStatsRepo userStatsRepo)
    {
        await foreach (var game in GetAllGames(gameRepo))
        {
            Console.WriteLine($"Testing {game.Id} ({game.Name})...");
            TestGame(game);
            var userStat = await userStatsRepo.Get("DanMaklen", game.Id);
            if (userStat is not null) TestUserStat(game, userStat.Value);
            Console.WriteLine($"Testing {game.Id} ({game.Name})... Passed!");
        }

        Console.WriteLine("All Games passed the test!");
    }

    private static void TestGame(Game game)
    {
    }

    private static void TestUserStat(Game game, UserStats userStats)
    {
    }

    private static async IAsyncEnumerable<Game> GetAllGames(IGameRepo gameRepo)
    {
        var games = await gameRepo.GetAll();
        foreach (var d in games)
        {
            yield return (await gameRepo.Get(d.Id))!;
        }
    }
}
