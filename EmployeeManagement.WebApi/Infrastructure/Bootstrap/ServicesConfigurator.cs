using EmployeeManagement.WebApi.Infrastructure.Mappers;
using MongoDB.Driver;
using System.Reflection;
using EmployeeManagement.WebApi.Domain;
using EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo;
using EmployeeManagement.WebApi.Model.API.Request;

namespace EmployeeManagement.WebApi.Infrastructure.Bootstrap
{
    /// <summary>
    /// Application service collection configurator
    /// </summary>
    public static class ServicesConfigurator
    {
        /// <summary>
        /// Adds the services specific to Specification Management Service to the application
        /// </summary>
        /// <param name="services">Service collection object</param>
        /// <param name="settings">Settings for the application</param>
        /// <returns>Modified service collection object</returns>
        public static IServiceCollection AddServices(this IServiceCollection services, ISettingsProvider settings)
        {
            services
                .AddInfrastructure(settings)
                .AddDomain(settings)
                .AddMongoPersistence(settings);

            return services;
        }
        /// <summary>
        /// Adds the swagger related information
        /// </summary>
        /// <param name="services">Service collection object</param>
        /// <returns>Modified service collection object</returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services
                .AddSwaggerGen(options =>
                {                    
                    options.UseInlineDefinitionsForEnums();

                    // Includes documentation about the derived types.
                    options.UseAllOfForInheritance();
                    options.UseOneOfForPolymorphism();

                    // Set the comments path for the Swagger JSON and UI.
                    string xmlPathForApi = GetSwaggerDocumentationFilePath(Assembly.GetExecutingAssembly());
                    options.IncludeXmlComments(xmlPathForApi);

                    string xmlPathForModels = GetSwaggerDocumentationFilePath(typeof(CreateEmployeeRequestObject).Assembly);
                    options.IncludeXmlComments(xmlPathForModels);
                });
        }
        private static string GetSwaggerDocumentationFilePath(Assembly assembly)
        {
            var commentsFileName = assembly.GetName().Name + ".xml";
            var baseDirectory = AppContext.BaseDirectory;
            return Path.Combine(baseDirectory, commentsFileName);
        }

        private static IServiceCollection AddInfrastructure(this IServiceCollection services, ISettingsProvider settings)
        {
            services
                .AddSingleton<IMappingCoordinator, MappingCoordinator>()
                .AddSingleton(settings);
            return services;
        }

        private static IServiceCollection AddDomain(this IServiceCollection services, ISettingsProvider settings)
        {
            services
                .AddTransient<IEmployeeDomainService,EmployeeDomainService>();

            return services;
        }

        private static IServiceCollection AddMongoPersistence(this IServiceCollection services, ISettingsProvider settings)
        {
            services
                .AddSingleton<IEmployeeRepository, MongoEmployeeRepository>()
                .AddSingleton<MongoClientBase, MongoClient>((serviceProvider) => new MongoClient("mongodb://localhost:27017/"));

            return services;
        }
    }
}

