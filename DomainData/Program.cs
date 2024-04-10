using Amazon.DynamoDBv2;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Pika.Converter;
using Pika.DataLayer.Dao;
using Pika.DataLayer.Repository;
using Pika.DomainData;
using Pika.DomainData.Scrapper;
using Pika.DomainData.Scrapper.MinecraftATM9;
using Pika.DomainData.Scrapper.VampireSurvivors;
using Pika.Repository;

var builder = CoconaApp.CreateBuilder(args);

// Minecraft
// builder.Services.AddTransient<IScrapper, IronSpellsNSpellbooksScrapper>();
// builder.Services.AddTransient<IScrapper, MinecraftScrapper>();
// builder.Services.AddTransient<IScrapper, IntegratedDynamicsScrapper>();

// Vampire Survivors
builder.Services.AddTransient<IScrapper, VampireSurvivorsScrapper>();
builder.Services.AddTransient<PikaConverter>();

builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
builder.Services.AddTransient<IDomainRepo, DomainRepo>();
builder.Services.AddTransient<IDomainDao, DomainDao>();

var app = builder.Build();

app.AddCommand("scrape", async (IEnumerable<IScrapper> scrappers, PikaConverter converter) =>
{
    foreach (var s in scrappers)
    {
        Console.WriteLine($"Scraping {s.DomainId}");
        var domain = await s.Scrape();
        TextWriter stream = new StreamWriter($"Domains/{s.OutputDirectory}/{s.DomainId}.scraped.yaml");
        converter.Write(domain, stream);
        await stream.FlushAsync();
    }
});

app.AddSyncCommand();

app.Run();
