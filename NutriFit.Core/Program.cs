using NutriFit.ServiceDefaults;
using Microsoft.EntityFrameworkCore;
using NutriFit.Core.Databases;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.AddNpgsqlDbContext<NutriFitWriteDbContext>("nutriFit-write");
builder.AddNpgsqlDbContext<NutriFitReadDbContext>("nutrifit-read");

var app = builder.Build();
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
    app.MapOpenApi();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
