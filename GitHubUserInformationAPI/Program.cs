global using GitHubUserInformationAPI.Data;
global using GitHubUserInformationAPI.Model;
global using GitHubUserInformationAPI.Services;
global using GitHubUserInformationAPI.Repositories;
global using GitHubUserInformationAPI.Managers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IGitHubService, GitHubService>();
builder.Services.AddScoped<IGitHubUserRepository, GitHubUserRepository>();
builder.Services.AddScoped<IGitHubUserManager, GitHubUserManager>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
