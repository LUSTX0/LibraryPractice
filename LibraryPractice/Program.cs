using Logic.Services;
using Microsoft.EntityFrameworkCore;
using SQLcon.Models;
using SQLcon.Repositories;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data.Entity;
using LIBRARY2;
using System.CodeDom;
using Microsoft.OpenApi.Models;
using System;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Polly.CircuitBreaker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString =  builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContextPool<ApplicationDbContext>(
    dbContextOptionsBuilder => dbContextOptionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23))));

// dependecies
//builder.Services.AddScoped<IBookRepository, BookRepository>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IRentRepository, RentRepository>();
//builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IReportService, ReportService>();
//builder.Services.AddScoped<IRepository<Book>>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var currentVersion = ThisAssembly.AssemblyInformationalVersion;
currentVersion = VersionCheck.Check(currentVersion);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = currentVersion,
        Title = "Library API",
        Description = "This API is just a test task"
    });
});
// Логи
builder.Services.AddLogging();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Your API v{currentVersion}"));
}
else
{
    app.UseSwagger();  //временно
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"Your API v{currentVersion}"));
}

app.UseMiddleware<ExceptionHandlingMiddleware>(); //added 2
// Другие middleware
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
