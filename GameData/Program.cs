using Amazon.DynamoDBv2;
using Cocona;
using Microsoft.Extensions.DependencyInjection;
using Pika.DataLayer.Dao;
using Pika.DataLayer.Repository;
using Pika.GameData;
using Pika.GameData.Commands;
using Pika.GameData.GameScrapper;
using Pika.GameData.ScrapperHelpers;
using Pika.PikaLang;
using Pika.Repository;

var builder = CoconaApp.CreateBuilder(args);

builder.Services.AddTransient<IScrapper, VampireSurvivorsScrapper>();
builder.Services.AddTransient<IScrapper, PalworldPalsScrapper>();
builder.Services.AddTransient<IScrapper, HadesScrapper>();
builder.Services.AddTransient<IScrapper, ToTheCoreScrapper>();
builder.Services.AddTransient<IScrapper, TimeClickersScrapper>();
builder.Services.AddTransient<IScrapper, SquareCellsScrapper>();
builder.Services.AddTransient<IScrapper, SpecOpsTheLineScrapper>();
builder.Services.AddTransient<IScrapper, MiddleEarthShadowOfWarScrapper>();

builder.Services.AddTransient<SteamClient>();
builder.Services.AddTransient<SteamScrapper>();
builder.Services.AddTransient<SteamScrapperHelper>();
builder.Services.AddPikaParser();

builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
builder.Services.AddTransient<IGameRepo, GameRepo>();
builder.Services.AddTransient<IGameDao, GameDao>();
builder.Services.AddTransient<IGameProgressRepo, GameProgressRepo>();
builder.Services.AddTransient<IGameProgressDao, GameProgressDao>();

var app = builder.Build();

app.AddSyncCommand();
app.AddTestCommand();
app.AddMigrateCommand();

app.Run();
