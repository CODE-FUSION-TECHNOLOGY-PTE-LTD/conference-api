using AuthManager;
using CommonLib;
using CommonLib.Models;
using CommonLib.MySql;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RegisterApi.fileUpload.services;
using RegisterApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Register API", Version = "v1" });
    c.CustomSchemaIds(type => type.FullName);

});
//email
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
builder.Services.AddMySqlDbContext<MySqlDbContext>(option =>
{
    option.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 29)));
});
builder.Services.AddMySqlRepository<User, MySqlDbContext>();
builder.Services.AddScoped<MySqlRepository<User>>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped<ManageFile>();
builder.Services.AddScoped<JwtTokenHandler>();
builder.Services.AddScoped<EmailService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000") // Adjust this based on where your frontend is hosted
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
    });
});




var app = builder.Build();

// Use CORS middleware
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Register API v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
