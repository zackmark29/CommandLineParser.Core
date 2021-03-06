﻿using MatthiWare.CommandLine.Abstractions;
using MatthiWare.CommandLine.Abstractions.Command;
using MatthiWare.CommandLine.Abstractions.Models;
using MatthiWare.CommandLine.Abstractions.Parsing;
using MatthiWare.CommandLine.Abstractions.Parsing.Collections;
using MatthiWare.CommandLine.Abstractions.Usage;
using MatthiWare.CommandLine.Abstractions.Validations;
using MatthiWare.CommandLine.Core.Command;
using MatthiWare.CommandLine.Core.Models;
using MatthiWare.CommandLine.Core.Parsing;
using MatthiWare.CommandLine.Core.Parsing.Resolvers;
using MatthiWare.CommandLine.Core.Parsing.Resolvers.Collections;
using MatthiWare.CommandLine.Core.Usage;
using MatthiWare.CommandLine.Core.Validations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace MatthiWare.CommandLine.Core
{
    /// <summary>
    /// Extension methods to allow DI services to be registered.
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Adds internal services used by <see cref="CommandLineParser"/>. 
        /// This won't overwrite existing services.
        /// </summary>
        /// <typeparam name="TOption">Base option type</typeparam>
        /// <param name="services">Current service collection</param>
        /// <param name="parser">Current instance reference</param>
        /// <param name="options">Current options reference</param>
        /// <returns>Input service collection to allow method chaining</returns>
        public static IServiceCollection AddInternalCommandLineParserServices<TOption>(this IServiceCollection services, CommandLineParser<TOption> parser, CommandLineParserOptions options)
            where TOption : class, new()
        {
            return services
                .AddCommandLineParser(parser)
                .AddArgumentManager()
                .AddValidatorContainer()
                .AddParserOptions(options)
                .AddDefaultResolvers()
                .AddCLIPrinters()
                .AddCommandDiscoverer()
                .AddEnvironmentVariables()
                .AddDefaultLogger()
                .AddModelInitializer()
                .AddSuggestionProvider();
        }

        internal static IServiceCollection AddArgumentManager(this IServiceCollection services)
        {
            services.TryAddScoped<IArgumentManager, ArgumentManager>();

            return services;
        }

        private static IServiceCollection AddParserOptions(this IServiceCollection services, CommandLineParserOptions options)
        {
            services.AddSingleton(options);

            return services;
        }

        private static IServiceCollection AddCommandLineParser<TOption>(this IServiceCollection services, CommandLineParser<TOption> parser)
             where TOption : class, new()
        {
            services.AddSingleton<ICommandLineCommandContainer>(parser);
            services.AddSingleton<ICommandLineParser<TOption>>(parser);

            return services;
        }

        private static IServiceCollection AddValidatorContainer(this IServiceCollection services)
        {
            services.AddSingleton<IValidatorsContainer, ValidatorsContainer>();

            return services;
        }

        private static IServiceCollection AddSuggestionProvider(this IServiceCollection services)
        {
            services.AddSingleton<ISuggestionProvider, DamerauLevenshteinSuggestionProvider>();

            return services;
        }

        internal static IServiceCollection AddDefaultResolvers(this IServiceCollection services)
        {
            services.TryAddScoped<IArgumentResolver<bool>, BoolResolver>();
            services.TryAddScoped<IArgumentResolver<double>, DoubleResolver>();
            services.TryAddScoped<IArgumentResolver<decimal>, DecimalResolver>();
            services.TryAddScoped<IArgumentResolver<float>, FloatResolver>();
            services.TryAddScoped<IArgumentResolver<int>, IntResolver>();
            services.TryAddScoped<IArgumentResolver<string>, StringResolver>();

            services.TryAddScoped(typeof(IArgumentResolver<>), typeof(DefaultResolver<>));
            services.TryAddScoped(typeof(IArrayResolver<>), typeof(ArrayResolver<>));
            services.TryAddScoped(typeof(IListResolver<>), typeof(ListResolver<>));
            services.TryAddScoped(typeof(ISetResolver<>), typeof(SetResolver<>));

            return services;
        }

        private static IServiceCollection AddCLIPrinters(this IServiceCollection services)
        {
            services.TryAddScoped<IUsageBuilder, UsageBuilder>();
            services.TryAddScoped<IUsagePrinter, UsagePrinter>();
            services.TryAddScoped<IConsole, SystemConsole>();

            return services;
        }

        private static IServiceCollection AddCommandDiscoverer(this IServiceCollection services)
        {
            services.TryAddScoped<ICommandDiscoverer, CommandDiscoverer>();

            return services;
        }

        private static IServiceCollection AddEnvironmentVariables(this IServiceCollection services)
        {
            services.TryAddScoped<IEnvironmentVariablesService, EnvironmentVariableService>();

            return services;
        }

        private static IServiceCollection AddDefaultLogger(this IServiceCollection services)
        {
            services.TryAddSingleton(typeof(ILogger), (_) => NullLogger.Instance);
            services.TryAddSingleton(typeof(ILogger<CommandLineParser>), (_) => NullLogger<CommandLineParser>.Instance);

            return services;
        }

        private static IServiceCollection AddModelInitializer(this IServiceCollection services)
        {
            services.TryAddScoped<IModelInitializer, ModelInitializer>();

            return services;
        }
    }
}
