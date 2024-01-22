using System.Text.Json;
using Amazon.DynamoDBv2;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Pika.Converter;
using Pika.DataLayer.Repositories;
using Pika.GameData;
using Pika.GameData.Planner;
using Pika.GameData.SchemaFetchers;
using Pika.GameData.Scrapper;
using Pika.GameData.Scrapper.MinecraftATM9;

var builder = CoconaApp.CreateBuilder(args);

builder.Services.AddTransient<IScrapper, IronSpellsNSpellbooksScrapper>();
builder.Services.AddTransient<PikaConverter>();

var app = builder.Build();

app.AddCommand("scrape", async (IEnumerable<IScrapper> scrappers, PikaConverter converter) =>
{
    foreach (var s in scrappers)
    {
        var domain = await s.Scrape();
        TextWriter stream = new StreamWriter($"Domains/{s.OutputDirectory}/{s.DomainId}.scraped.yaml");
        converter.Write(domain, stream);
        await stream.FlushAsync();
    }
});

app.AddCommand("sync", (PikaConverter converter) =>
{
    foreach (var file in Directory.EnumerateFiles("Domains/", "*.yaml", SearchOption.AllDirectories))
    {
        Console.WriteLine(file);
        TextReader stream = new StreamReader(file);
        string domainId = Path.GetFileNameWithoutExtension(file).Replace(".scraped", string.Empty);
        var domain = converter.Read(stream, domainId);
        Console.WriteLine(domain.Name);
    }
});

app.AddCommand("daveTheDiver", async () =>
{
    IAmazonDynamoDB db = new AmazonDynamoDBClient();
    var gameRepo = new GameRepo(db);
    var achievementRepo = new AchievementRepo(db);
    var pikaSchemaBuilder = new PikaSchemaBuilder(gameRepo, achievementRepo);
    var steamClient = new SteamClient();
    var scrapper = new DaveTheDiverSchemaFetcher(steamClient);
    var planManager = new PlanManager();

    var pikaGameId = Guid.Parse("7c1c7ad0-b274-4b04-a63c-240f3f2631bf");

    var sourceSchema = await scrapper.FetchSchema(pikaGameId);
    var pikaSchema = await pikaSchemaBuilder.BuildSchema(pikaGameId);

    var plan = planManager.ConstructPlan(pikaSchema, sourceSchema);

    Console.WriteLine($"Plan: {JsonSerializer.Serialize(plan)}");

    foreach (var achievement in plan.NewAchievements)
    {
        await achievementRepo.Create(achievement);
    }

    Console.WriteLine($"Found Conflicts in {plan.ConflictAchievements.Count} Entries. Ignoring them...");
    Console.WriteLine($"Found Deleted {plan.DeleteAchievements.Count} Entries. Ignoring them...");

    Console.WriteLine("Done...");
});

app.Run();
