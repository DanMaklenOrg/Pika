using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.DynamoDBv2;
using Pika.DataLayer.Dao;
using Pika.DataLayer.Repository;
using Pika.Repository;
using Pika.Service;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Config
builder.Configuration.AddSystemsManager("/pika");

// DB
builder.Services.AddSingleton<IAmazonDynamoDB, AmazonDynamoDBClient>();
builder.Services.AddTransient<IDomainDao, DomainDao>();
builder.Services.AddTransient<IDomainRepo, DomainRepo>();
builder.Services.AddTransient<IUserStatsRepo, UserStatsRepo>();
builder.Services.AddTransient<IUserStatsDao, UserStatsDao>();

// APIs
// builder.Services.Configure<JsonSerializerOptions>(options => options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
});
builder.Services.AddHealthChecks();

// Lambda Hosting
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.AddAnaAuth();

WebApplication app = builder.Build();

app.UsePathBase("/pika");
app.UseCors(policyBuilder => policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseRouting();

app.UseHealthChecks("/health");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
