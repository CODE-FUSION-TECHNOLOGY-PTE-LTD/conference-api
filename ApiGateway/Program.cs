
using AuthManager;
using DotNetEnv;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
// Load environment variables from .env file
Env.Load();
// Configure Ocelot with the settings from ocelot.json
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelet.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddCustomeAuthentication();


// Read environment variables
string baseUrl = Environment.GetEnvironmentVariable("BASE_URL") ?? "http://localhost:5000";
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();
// Use CORS middleware
app.UseCors("AllowSpecificOrigin");
await app.UseOcelot();
app.Run();
