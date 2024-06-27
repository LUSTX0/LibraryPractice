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
//builder.Services.AddScoped< IRepository<T>, Repository<T>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Логи
builder.Services.AddLogging();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwagger();  //временно
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>(); //added 2
// Другие middleware
app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

//app.UseEndpoints(endpoints =>                  //added 4
//{
//    endpoints.MapControllers();
//});

app.MapControllers();

app.Run();
