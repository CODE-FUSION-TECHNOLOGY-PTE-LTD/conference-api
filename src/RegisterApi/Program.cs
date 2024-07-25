
using common.Api;
using CommonLib.MassTransit;
using CommonLib.Models;
using CommonLib.MongoDB;
using CommonLib.MySql;
using Microsoft.EntityFrameworkCore;
using RegisterApi.fileUpload.services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMySqlDbContext<MySqlDbContext>(option =>
{
    option.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 29)));
});
builder.Services.AddMongo().AddMongoRepositotry<User>("Users");
builder.Services.AddMySqlRepository<User, MySqlDbContext>();
builder.Services.AddScoped<ManageFile>();



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
