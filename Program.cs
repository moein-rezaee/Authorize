using System.Reflection;
using Authorize.Context;
using Authorize.Interfaces;
using Authorize.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var defName = builder.Configuration["Db:Name"];
var defHost = builder.Configuration["Db:Host"];
var defPass = builder.Configuration["Db:Pass"];
var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? defHost;
var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? defName;
var dbPass = Environment.GetEnvironmentVariable("DB_SA_PASSWORD") ?? defPass;
var connectionString = $"Server={dbHost}; Persist Security Info=False; TrustServerCertificate=true; User ID=sa;Password={dbPass};Initial Catalog={dbName};";
builder.Services.AddDbContext<AuthorizeContextDb>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
