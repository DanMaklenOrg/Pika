WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// DI Registry
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

WebApplication app = builder.Build();

// Middleware Pipeline
app.UseHttpsRedirection();
app.UseHealthChecks("/health");
app.MapControllers();

app.Run();
