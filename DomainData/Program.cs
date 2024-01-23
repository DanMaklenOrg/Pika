using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Pika.Converter;
using Pika.DomainData;
using Pika.DomainData.Scrapper;
using Pika.DomainData.Scrapper.MinecraftATM9;

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

app.AddSyncCommand();

app.Run();
