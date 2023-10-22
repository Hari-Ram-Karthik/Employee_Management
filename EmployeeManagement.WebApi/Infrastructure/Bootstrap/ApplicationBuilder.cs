using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.WebApi.Infrastructure.Bootstrap
{
    /// <summary>
    /// Web application builder
    /// </summary>
    public static class ApplicationBuilder
    {
        private const string UserServicesName = "userservices";

        /// <summary>
        /// Registers the logging setup to be used
        /// </summary>
        /// <param name="builder">Web application builder object</param>
        /// <param name="settings">Settings for the application</param>
        /// <returns>Modified web application builder object</returns>
        public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder, ISettingsProvider settings)
        {
            builder.Services.AddLogging();
            //builder.Logging.ClearProviders();
            //builder.Logging.AddConsole();

            return builder;
        }

        /// <summary>
        /// Adds all the required services for the web application
        /// </summary>
        /// <param name="builder">Web application builder object</param>
        /// <param name="settings">Settings for the application</param>
        /// <returns>Modified web application builder object</returns>
        public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder, ISettingsProvider settings)
        {
            builder.Services
                .AddServices(settings)
                .AddSwagger()
                .AddControllers();
            
            return builder;
        }

        /// <summary>
        /// Configures the request pipeline for the web application
        /// </summary>
        /// <param name="application">Web application builder object</param>
        /// <returns>Modified web application builder object</returns>
        public static WebApplication Configure(this WebApplication application)
        {
            application.UseSwagger();
            application.UseSwaggerUI();
            application.MapControllers();
            return application;
        }

        /// <summary>
        /// Initializes the required items before the start of the application
        /// </summary>
        /// <param name="application">Web application builder object</param>
        public static async Task InitializeAsync(this WebApplication application)
        {
            application.Logger.LogInformation("Employee Management Service is starting");
        }
    }
}
