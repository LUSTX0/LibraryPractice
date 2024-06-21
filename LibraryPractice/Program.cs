using Logic.Services;
using Microsoft.EntityFrameworkCore;
using SQLcon.Models;
using SQLcon.Repositories;
//using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System.Data.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString =  builder.Configuration.GetConnectionString("DefaultConnection");
//builder.Services.AddMySql<ApplicationDbContext>("DefaultConnection",);
//var connectionString = builder.Configuration.GetConnectionString("mysqldb");

builder.Services.AddDbContextPool<ApplicationDbContext>(
    dbContextOptionsBuilder => dbContextOptionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23))));
//builder.enr<ApplicationDbContext>();
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//        options.UseMySql(Microsoft.Extensions.Configuration.c("DefaultConnection")));

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRentService, RentService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// dependecies
//builder.Services.AddSingleton<>();
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
