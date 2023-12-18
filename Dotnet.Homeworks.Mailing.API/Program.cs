using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Services;
using Dotnet.Homeworks.Mailing.API.ServicesExtensions;

var builder = WebApplication.CreateBuilder(args);
var rabbitmq = new RabbitMqConfig
{
    Username = builder.Configuration["RabbitMqConfig:Username"]!,
    Password = builder.Configuration["RabbitMqConfig:Password"]!,
    Hostname = builder.Configuration["RabbitMqConfig:Hostname"]!,
    Port = int.Parse(builder.Configuration["RabbitMqConfig:Port"]!)
};

builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("EmailConfig"));
builder.Services.AddMasstransitRabbitMq(rabbitmq);
builder.Services.AddScoped<IMailingService, MailingService>();

var app = builder.Build();

app.Run();