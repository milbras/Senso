var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register Swagger generator
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable middleware to serve generated Swagger as JSON endpoint.
app.UseSwagger();

// Enable middleware to serve Swagger UI.
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Senso Game API v1");
    options.RoutePrefix = ""; // makes Swagger UI the root page
});

// Optional: Use HTTPS redirection
app.UseHttpsRedirection();

app.MapControllers();

app.Run();