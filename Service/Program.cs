using Mapster;
using Pika.DataLayer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Config
builder.Configuration.AddSystemsManager("/pika");
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("dev.config.json", true, true);
}

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
app.UseHttpsRedirection();
app.UseHealthChecks("/health");
app.MapControllers();

app.Run();
