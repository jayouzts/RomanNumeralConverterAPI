using RomanNumeralConverterAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services via Startup
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure middleware via Startup
startup.Configure(app, builder.Environment);

app.Run();
