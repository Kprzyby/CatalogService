using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using PlatformService.Data;
using PlatformService.Services;
using ServiceBusPublisher;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;

    if (env.IsProduction())
    {
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("K8SDBConnection")));
    }
    else
    {
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));
    }

    config.SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) //load base settings
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true) //load local settings
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true) //load environment settings
                .AddEnvironmentVariables();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<PlatformServ>();
builder.Services.AddScoped<PlatformRepo>();

//ServiceBus Dependency Injection

string serviceBusConnectionString = builder.Configuration.GetConnectionString("ServiceBusConnection");
string topicName = builder.Configuration.GetValue<string>("ServiceBus:TopicName");

ServiceBusClient serviceBusClient = new ServiceBusClient(serviceBusConnectionString);

builder.Services.AddSingleton<ServiceBusSender>(e =>
{
    return serviceBusClient.CreateSender(topicName);
});

builder.Services.AddSingleton<PublisherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    DBPrep.ApplyMigrations(app);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();