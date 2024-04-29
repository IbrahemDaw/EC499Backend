using Ocelot.DependencyInjection;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddLogging(configure => configure.AddConsole());

builder
    .Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
    });

builder.Services.AddOcelot();
var app = builder.Build();
app.UseCors("AllowAll");
await app.UseOcelot();
app.Run();
