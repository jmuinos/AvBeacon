﻿using System.Reflection;
using AvBeacon.Application._Core.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace AvBeacon.Application;

public static class DependencyInjection
{
    /// <summary>
    ///     Registers the necessary services with the DI framework.
    /// </summary>
    /// <param name="services"> The service collection. </param>
    /// <returns> The same service collection. </returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));

            cfg.AddOpenBehavior(typeof(TransactionBehaviour<,>));
        });

        return services;
    }
}