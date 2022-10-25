using Microsoft.EntityFrameworkCore;
using Persistence;
using MediatR;
using System.Collections.Generic;
using Application.Activities;
using AutoMapper;
using Application.Core;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServices(builder.Configuration);
var app = builder.Build();

var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetService<DataContext>();
if (context != null)
{
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
