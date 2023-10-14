using EmployeeManagement.WebApi.Domain;
using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;

namespace EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo
{
    public class MongoEmployeeRepository : IEmployeeRepository
    {
        private const string DefaultEmployeeDatabaseName = "employee";
        private const string EmployeeCollectionName = "employee";
        private readonly IMongoDatabase _database;
        private readonly ILogger _logger;
        private readonly IMappingCoordinator _mappingCoordinator;

        /// <summary>
        /// Initializes a new instance of the <see cref="MongoEmployeeRepository"/> class.
        /// </summary>
        /// <param name="logger">Dependency injection for logger.</param>
        /// <param name="mappingCoordinator">Dependency injection for mapper.</param>
        /// <param name="client">Dependency injection for settings mongo client.</param>
        public MongoEmployeeRepository(
            //ILogger logger,
            IMappingCoordinator mappingCoordinator,
            MongoClientBase client)
        {
            _database = GetDatabase(client, "mongodb://localhost:27017");
            //_logger = logger;
            _mappingCoordinator = mappingCoordinator;
        }
        private IMongoDatabase GetDatabase(MongoClientBase client, string connectionString)
        {
            string databaseName = new ConnectionString(connectionString).DatabaseName
                ?? DefaultEmployeeDatabaseName;
            return client.GetDatabase(databaseName);
        }


        public async Task<IEnumerable<EmployeeEntity>> InsertEmployeesAsync(IEnumerable<EmployeeModel> employeesToBeCreated)
        {
            IEnumerable<EmployeeEntity> employeeEntities =
                _mappingCoordinator.Map<EmployeeModel, EmployeeEntity>(employeesToBeCreated);
            try
            {
                await EmployeeCollection.InsertManyAsync(
                    employeeEntities,new InsertManyOptions() { IsOrdered=false});

                return employeeEntities;
            }
            catch (MongoConnectionException ex)
            {
                _logger.LogError("Database connection was interrupted with error [{exceptionType}] {error}", ex.GetType().Name, ex.Message);
                throw new NotImplementedException();
            }
            catch (MongoException ex)
            {
                _logger.LogError("Unknown database error occurred [{exceptionType}] {error}", ex.GetType().Name, ex.Message);
                throw new NotImplementedException();
            }
            catch (TimeoutException ex)
            {
                _logger.LogError("Timeout occurred [{exceptionType}] {error}", ex.GetType().Name, ex.Message);
                throw new TimeoutException();
            }
        }

        private IMongoCollection<EmployeeEntity> EmployeeCollection =>
            _database.GetCollection<EmployeeEntity>(EmployeeCollectionName);
    }
}
