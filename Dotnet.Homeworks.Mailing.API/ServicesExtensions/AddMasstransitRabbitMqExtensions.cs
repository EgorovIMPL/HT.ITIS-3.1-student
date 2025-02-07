using Dotnet.Homeworks.Mailing.API.Configuration;
using Dotnet.Homeworks.Mailing.API.Consumers;
using MassTransit;

namespace Dotnet.Homeworks.Mailing.API.ServicesExtensions;

public static class AddMasstransitRabbitMqExtensions
{
    public static IServiceCollection AddMasstransitRabbitMq(this IServiceCollection services,
        RabbitMqConfig rabbitConfiguration)
    {
        var host = $"amqp://{rabbitConfiguration.Username}:{rabbitConfiguration.Password}@{rabbitConfiguration.Hostname}:{rabbitConfiguration.Port}";
        services.AddMassTransit(options =>
        {
            options.UsingRabbitMq((context, configuration) =>
            {
                configuration.ConfigureEndpoints(context);
                configuration.Host(host);
            });
            options.AddConsumer<EmailConsumer>();
        });

        return services;
    }
}