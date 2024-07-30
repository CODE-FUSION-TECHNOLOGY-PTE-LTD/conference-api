using CommonLib.Models;
using CommonLib.MongoDB;
using ConferenceApi;
using ConferenceApi.Entity;
using ConferenceApi.Services;
using MassTransit;
using testCommon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongo().AddMongoRepositotry<Conference>("Conference").AddMongoRepositotry<Register>("Conference-Register");
builder.Services.AddScoped<IConferenceRepository, ConferenceRepository>();

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
