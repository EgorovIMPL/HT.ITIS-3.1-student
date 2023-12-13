using Dotnet.Homeworks.MainProject.Configuration;
using MassTransit;

namespace Dotnet.Homeworks.MainProject.ServicesExtensions.Masstransit;

public static class ServiceCollectionExtensions
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
        });

        return services;
    }
}