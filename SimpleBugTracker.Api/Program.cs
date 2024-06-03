using SimpleBugTracker.Application;
using SimpleBugTracker.API.Endpoints;
using SimpleBugTracker.Infrastructure;
using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => {
    options.AddDefaultPolicy(
        policyBuilder =>
        {
            policyBuilder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
var app = builder.Build();

var useSwagger = builder.Configuration.GetValue<bool>("UseSwagger");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || useSwagger)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.AddApiEndpoints();

app.Run();
