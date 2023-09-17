using Microsoft.EntityFrameworkCore;
using ProductWebApis.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


//// Database Context dependency Injection
//var dbHost = "localhost";
//var dbName = "dms_product";
//var dbPassword = "tayyab!23";


// Database Context dependency Injection
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
//var dbrootuser = Environment.GetEnvironmentVariable("DB_root_User");
var connectionString = $"Server={dbHost}; port=3306; Database={dbName}; uid=root; Password={dbPassword};";
builder.Services.AddDbContext<ProductContext>(op =>
{
    op.UseMySQL(connectionString);
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
