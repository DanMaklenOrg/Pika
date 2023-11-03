using Amazon.DynamoDBv2;
using Pika.DataLayer.Repositories;
using Pika.Service;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Config
builder.Configuration.AddSystemsManager("/pika");

// DB
builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
builder.Services.AddTransient<IGameRepo, GameRepo>();
builder.Services.AddTransient<IAchievementRepo, AchievementRepo>();

// APIs
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

// Lambda Hosting
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.AddAnaAuth();

WebApplication app = builder.Build();

// Middleware Pipeline
app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseHealthChecks("/health");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
