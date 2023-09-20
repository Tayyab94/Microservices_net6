using Ocelot.Middleware;
using Ocelot.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
var d = builder.Environment.ContentRootPath;
    builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ocelot.json", optional: false,reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddOcelot(builder.Configuration);
var app = builder.Build();
await app.UseOcelot();
//app.MapGet("/", () => "Hello World!");

app.Run();
