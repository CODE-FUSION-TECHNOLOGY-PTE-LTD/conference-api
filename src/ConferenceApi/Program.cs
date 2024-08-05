using CommonLib.Models;
using CommonLib.MongoDB;
using ConferenceApi;
using ConferenceApi.Client;
using ConferenceApi.Entity;
using ConferenceApi.Services;
using MassTransit;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMongo().AddMongoRepositotry<Conference>("Conference").AddMongoRepositotry<Register>("Conference-Register").AddMongoRepositotry<User>("User");
builder.Services.AddScoped<IConferenceRepository, ConferenceRepository>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ReceiveEndpoint("Account_register", e =>
         {
             e.ConfigureConsumer<UserRegisterService>(context);
         });
    });
    x.AddConsumer<UserRegisterService>();
});

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
