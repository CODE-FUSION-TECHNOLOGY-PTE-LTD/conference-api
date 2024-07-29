
using CommonLib.Models;
using CommonLib.MongoDB;
using CommonLib.MySql;
using ConferenceApi.Entity;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RegisterApi.fileUpload.services;




var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v2", new OpenApiInfo { Title = "Register API", Version = "v1" });
    c.CustomSchemaIds(type => type.FullName);
});

builder.Services.AddMySqlDbContext<MySqlDbContext>(option =>
{
    option.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 29)));
});
builder.Services.AddMySqlRepository<User, MySqlDbContext>();
builder.Services.AddScoped<ManageFile>();
builder.Services.AddMongo().AddMongoRepositotry<User>("Users");




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
