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
builder.Services.AddMongo().AddMongoRepositotry<Conference>("Conference")
.AddMongoRepositotry<Register>("Conference-Register")
.AddMongoRepositotry<UserConf>("User")
.AddMongoRepositotry<Config>("Config")
.AddMongoRepositotry<AcceptPolicy>("Accept-Policy");
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


var app = builder.Build();
// Use CORS middleware
app.UseCors("AllowSpecificOrigin");
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
