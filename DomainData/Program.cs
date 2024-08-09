using Amazon.DynamoDBv2;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Pika.DataLayer.Dao;
using Pika.DataLayer.Repository;
using Pika.DomainData;
using Pika.DomainData.DomainScrapper;
using Pika.DomainData.DomainScrapper.ShapezIo;
using Pika.DomainData.DomainScrapper.VampireSurvivors;
using Pika.DomainData.ScrapperHelpers;
using Pika.PikaLang;
using Pika.Repository;

var builder = CoconaApp.CreateBuilder(args);

builder.Services.AddTransient<IScrapperOld, VampireSurvivorsScrapper>();
builder.Services.AddTransient<IScrapperOld, VampireSurvivorsAchievements>();
builder.Services.AddTransient<IScrapperOld, VampireSurvivorsSecrets>();
builder.Services.AddTransient<IScrapper, PalworldPalsScrapper>();
builder.Services.AddTransient<IScrapper, DungeonSoulsScrapper>();
builder.Services.AddTransient<IScrapperOld, ShapezScrapper>();
builder.Services.AddTransient<IScrapper, HadesScrapper>();

builder.Services.AddTransient<SteamClient>();
builder.Services.AddTransient<SteamScrapperHelper>();
builder.Services.AddScoped<EntityNameContainer>();
builder.Services.AddPikaParser();

builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
builder.Services.AddTransient<IDomainRepo, DomainRepo>();
builder.Services.AddTransient<IDomainDao, DomainDao>();
builder.Services.AddTransient<IUserStatsRepo, UserStatsRepo>();
builder.Services.AddTransient<IUserStatsDao, UserStatsDao>();

var app = builder.Build();

app.AddSyncCommand();
app.AddTestCommand();

app.Run();
