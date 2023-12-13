using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.MainProject.Configuration;
using Dotnet.Homeworks.MainProject.Services;
using Dotnet.Homeworks.MainProject.ServicesExtensions.Masstransit;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var rabbitmq = new RabbitMqConfig
{
    Username = builder.Configuration["RabbitMqConfig:Username"]!,
    Password = builder.Configuration["RabbitMqConfig:Password"]!,
    Hostname = builder.Configuration["RabbitMqConfig:Hostname"]!,
    Port = int.Parse(builder.Configuration["RabbitMqConfig:Port"]!)
};

builder.Configuration.AddEnvironmentVariables();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddSingleton<IRegistrationService, RegistrationService>();
builder.Services.AddSingleton<ICommunicationService, CommunicationService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMasstransitRabbitMq(rabbitmq!);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapControllers();

app.Run();