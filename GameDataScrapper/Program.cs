using System.Text.Json;
using Amazon.DynamoDBv2;
using Pika.DataLayer.Repositories;
using Pika.GameDataScrapper;
using Pika.GameDataScrapper.Planner;
using Pika.GameDataScrapper.SchemaFetchers;

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
