using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Server_SUB;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment, app.Lifetime, app.Services);

app.Run();