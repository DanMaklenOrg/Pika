using Amazon.DynamoDBv2;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Pika.Converter;
using Pika.DataLayer.Dao;
using Pika.DataLayer.Repository;
using Pika.DomainData;
using Pika.DomainData.Scrapper;
using Pika.DomainData.Scrapper.DungeonSouls;
using Pika.DomainData.Scrapper.Hades;
using Pika.DomainData.Scrapper.Palworld;
using Pika.DomainData.Scrapper.ShapezIo;
using Pika.DomainData.Scrapper.VampireSurvivors;
using Pika.PikaLang;
using Pika.Repository;

var builder = CoconaApp.CreateBuilder(args);

builder.Services.AddTransient<IScrapper, VampireSurvivorsScrapper>();
builder.Services.AddTransient<IScrapper, VampireSurvivorsAchievements>();
builder.Services.AddTransient<IScrapper, VampireSurvivorsSecrets>();
builder.Services.AddTransient<IScrapper, PalworldPalsScrapper>();
builder.Services.AddTransient<IScrapper, PalworldAchievements>();
builder.Services.AddTransient<IScrapper, DungeonSoulsScrapper>();
builder.Services.AddTransient<IScrapper, ShapezScrapper>();
builder.Services.AddTransient<IScrapper, HadesScrapper>();
builder.Services.AddTransient<IScrapper, HadesKeepsakes>();
builder.Services.AddTransient<IScrapper, HadesProphecies>();

builder.Services.AddTransient<SteamClient>();
builder.Services.AddTransient<SteamScrapper>();
builder.Services.AddScoped<EntityNameContainer>();
builder.Services.AddTransient<PikaConverter>();
builder.Services.AddPikaParser();

builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
builder.Services.AddTransient<IDomainRepo, DomainRepo>();
builder.Services.AddTransient<IDomainDao, DomainDao>();

var app = builder.Build();

app.AddCommand("scrape", async ([Argument] string domainId, IEnumerable<IScrapper> scrappers, PikaConverter converter) =>
{
    foreach (var s in scrappers.Where(s => s.DomainId.FullyQualifiedId == domainId))
    {
        Console.WriteLine($"Scraping {s.DomainId} ({s.FileName})");
        var domain = await s.Scrape();
        var directory = $"Domains/{s.OutputDirectory}";
        Directory.CreateDirectory(directory);
        TextWriter stream = new StreamWriter($"{directory}/{s.FileName}.scraped.yaml");
        converter.Write(domain, stream);
        await stream.FlushAsync();
    }
});

app.AddSyncCommand();

app.Run();
