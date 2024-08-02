using CommonLib.MongoDB;
using Microsoft.EntityFrameworkCore;
using Payment.Api.Data;
using Payment.Api.Models;
using Payment.Api.Models.Entity;
using Payment.Api.services;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//email
builder.Services.AddScoped<EmailService>(sp =>
        new EmailService(
            builder.Configuration["Email:SmtpServer"],
            builder.Configuration.GetValue<int>("Email:SmtpPort"),
            builder.Configuration["Email:SmtpUser"],
            builder.Configuration["Email:SmtpPass"]!
        ));
builder.Services.Configure<StripeModel>(builder.Configuration.GetSection("Stripe"));
builder.Services.AddMongo().AddMongoRepositotry<OrderModel>("Order");
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ChargeService>();
builder.Services.AddScoped<EmailService>();
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
