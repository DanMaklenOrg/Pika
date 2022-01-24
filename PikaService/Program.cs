WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// DI Registry
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

// Middleware Pipeline
app.UseHttpsRedirection();
app.MapControllers();
app.UseHealthChecks("/health");

app.Run();
