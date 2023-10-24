using EmployeeManagement.WebApi;
using EmployeeManagement.WebApi.Infrastructure.Bootstrap;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ISettingsProvider settings = new ApplicationSettingsProvider(builder.Configuration);

WebApplication application = builder
    .AddLogging(settings)
    .AddServices(settings)
    .Build();

await application.Configure().InitializeAsync();

application.Run();

/// <summary>
/// Defining partial class for creating in-memory server in component tests
/// </summary>
#pragma warning disable CA1050 // Declare types in namespaces
public partial class Program { }
#pragma warning restore CA1050 // Declare types in namespaces