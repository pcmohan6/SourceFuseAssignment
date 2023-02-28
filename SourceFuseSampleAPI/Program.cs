using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySql.EntityFrameworkCore.Infrastructure;
using SourceFuseSampleAPI.Middleware;
using SourceFuseSampleAPI.Models;

var builder = WebApplication.CreateBuilder(args);//Asp.net web application service intiated here

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseMySQL("host=127.0.0.1; port=3306; database=Customers; user=root; password=mysql;");
});

builder.Services.AddControllers();//dependency injection of all controllers
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();//All Api methods listed to user
builder.Services.AddSwaggerGen();// Swagger tool used to test the Api methods

var app = builder.Build(); //web application is built with preinitiated services

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyAuthentication>();// API key authentication added
app.UseAuthorization();

app.MapControllers();

app.Run();// Run the application
