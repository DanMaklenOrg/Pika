using Pika.DataLayer;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// DI Registry
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

PikaDataContext.RegisterService(builder.Services);

WebApplication app = builder.Build();

// Middleware Pipeline
app.UseHttpsRedirection();
app.UseHealthChecks("/health");
app.MapControllers();

app.Run();
