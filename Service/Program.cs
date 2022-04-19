using Mapster;
using Pika.DataLayer;
using Pika.Service;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Config
builder.Configuration.AddSystemsManager("/pika");
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("dev.config.json", true, true);
}

builder.Services.Configure<ServiceConfig>(builder.Configuration);

// Mapping Config
TypeAdapterConfig.GlobalSettings.Default.EnumMappingStrategy(EnumMappingStrategy.ByName);
TypeAdapterConfig<Guid, string>.ForType().MapWith(guid => guid.ToString("N"));

// DB
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("database"));
builder.Services.AddDbContext<PikaDataContext>();

// APIs
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

// Middleware Pipeline
app.UseHealthChecks("/health");
app.MapControllers();

app.Run();
