using EmployeeManagement.WebApi;
using EmployeeManagement.WebApi.Domain;
using EmployeeManagement.WebApi.Infrastructure.Mappers;
using EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using MongoDB.Driver;
using System.Reflection;
using Microsoft.AspNetCore.Builder;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});


builder.Services.AddSingleton<IMappingCoordinator, MappingCoordinator>();

builder.Services.AddTransient<IEmployeeDomainService,EmployeeDomainService>();

builder.Services
                .AddSingleton<IEmployeeRepository, MongoEmployeeRepository>()
                .AddSingleton<MongoClientBase, MongoClient>((serviceProvider) => new MongoClient(Environment.GetEnvironmentVariable("MongoConnectionString")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
