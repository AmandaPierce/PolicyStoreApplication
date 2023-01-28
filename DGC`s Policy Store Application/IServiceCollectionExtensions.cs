using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using PolicyStoreApplication.Configuration;
using PolicyStoreApplication.Factories;
using PolicyStoreApplication.Services;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PolicyStoreApplication
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAndConfigureApiVersioning(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        public static IServiceCollection AddAndConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "PolicyStoreApplication", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        /// <summary>
        ///     Adds and configures the database service. 
        /// </summary>
        /// <returns></returns>
        public static IServiceCollection AddAndConfigureDatabaseFactoryAndSettings(this IServiceCollection services, IConfigurationSection mongoDBConfiguration)
        {
            services.Configure<MongoDBConfiguration>(mongoDBConfiguration);
            var configuration = mongoDBConfiguration.Get<MongoDBConfiguration>();

            var mongoClient = new MongoClient(configuration.ConnectionString);


            var mongoDbCollectionFactory = new MongoDbCollectionFactory(mongoClient, configuration.DatabaseName, new List<string> { configuration.PolicyCollectionName });

            services.AddSingleton<IMongoDbCollectionFactory>(mongoDbCollectionFactory);

            return services;
        }
    }
}
