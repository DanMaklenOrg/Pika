using Mapster;
using Pika.DataLayer;
using Pika.DataLayer.Model;
using Pika.Service;
using Pika.Service.Dto.Common;

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
TypeAdapterConfig<string, Guid>.ForType().MapWith(guidStr => string.IsNullOrWhiteSpace(guidStr) ? Guid.Empty : Guid.Parse(guidStr));

TypeAdapterConfig<string, EntryDbModel>.ForType().MapWith(id => new EntryDbModel { Id = id.Adapt<Guid>() });
TypeAdapterConfig<EntryDbModel, string>.ForType().MapWith(entry => entry.Id.Adapt<string>());

TypeAdapterConfig<ObjectiveDto, ObjectiveDbModel>.ForType()
    .TwoWays()
    .Map(model => model.Targets, dto => dto.EntriesId);

TypeAdapterConfig<string, ObjectiveDbModel>.ForType().MapWith(id => new ObjectiveDbModel { Id = id.Adapt<Guid>() });
TypeAdapterConfig<ObjectiveDbModel, string>.ForType().MapWith(entry => entry.Id.Adapt<string>());

TypeAdapterConfig<ProgressDto, ProgressDbModel>.ForType()
    .TwoWays()
    .Map(model => model.Target, dto => dto.TargetId)
    .Map(model => model.Objective, dto => dto.ObjectiveId);

// DB
builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("database"));
builder.Services.AddDbContext<PikaDataContext>();

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
