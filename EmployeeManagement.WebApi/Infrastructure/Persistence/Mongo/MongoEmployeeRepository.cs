using AutoMapper;
using EmployeeManagement.WebApi.Domain;
using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo.Entity;
using EmployeeManagement.WebApi.Model.API.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using MongoDB.Driver.Linq;

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


        public async Task<IEnumerable<EmployeeModel>> InsertEmployeesAsync(IEnumerable<EmployeeModel> employeesToBeCreated)
        {
            IEnumerable<EmployeeEntity> employeeEntities =
                _mappingCoordinator.Map<EmployeeModel, EmployeeEntity>(employeesToBeCreated);
            try
            {
                await EmployeeCollection.InsertManyAsync(
                    employeeEntities,new InsertManyOptions() { IsOrdered=false});

                return _mappingCoordinator.Map<EmployeeEntity, EmployeeModel>(employeeEntities);
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

        public async Task<IEnumerable<EmployeeModel>> GetEmployeesAsync()
        {
            try
           {
                IEnumerable<EmployeeEntity> employees = await EmployeeCollection.Find(new BsonDocument()).ToListAsync();

                return _mappingCoordinator.Map<EmployeeEntity, EmployeeModel>(employees);
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

        public async Task<IEnumerable<EmployeeModel>> GetEmployeesByIdAsync(List<int> employeeIds)
        {
            try
            {
                var getFliter = Builders<EmployeeEntity>.Filter.In("EmployeeID", employeeIds);
                IEnumerable<EmployeeEntity> employees = await EmployeeCollection.Find(getFliter).ToListAsync();

                return _mappingCoordinator.Map<EmployeeEntity, EmployeeModel>(employees);
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

        public async Task<IEnumerable<EmployeeModel>> EditEmployeeAsync(IEnumerable<EmployeeModel> employeeToBeEdited)
        {
            IEnumerable<EmployeeEntity> employee =  _mappingCoordinator.Map<EmployeeModel, EmployeeEntity>(employeeToBeEdited);
            try
            {
                IEnumerable<EmployeeEntity> oldEmployeeDetails = await EmployeeCollection.Find(x => x.EmployeeID == employeeToBeEdited.First().EmployeeID).ToListAsync();
                if (oldEmployeeDetails.Count() == 0)
                {
                    return Enumerable.Empty<EmployeeModel>();
                }
                employee.First()._id=oldEmployeeDetails.First()._id;
                employee.First().UpdatedAt=DateTime.UtcNow;
                employee.First().CreatedAt=oldEmployeeDetails.First().CreatedAt;
                await EmployeeCollection.FindOneAndReplaceAsync(x=>x.EmployeeID== employeeToBeEdited.First().EmployeeID, employee.First());
                return _mappingCoordinator.Map<EmployeeEntity, EmployeeModel>(employee);
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
        public async Task<IEnumerable<EmployeeModel>> DeleteEmployeeAsync(IEnumerable<DeleteEmployeeRequestModel> employeeIdToBeDeleted)
        {
            try
            {
                IEnumerable<EmployeeEntity> EmployeeDetails = await EmployeeCollection.Find(x => x.EmployeeID == employeeIdToBeDeleted.First().EmployeeId).ToListAsync();
                if (EmployeeDetails.Count() == 0)
                {
                    return Enumerable.Empty<EmployeeModel>();
                }
                await EmployeeCollection.DeleteOneAsync(x=>x.EmployeeID==EmployeeDetails.First().EmployeeID);

                return (IEnumerable<EmployeeModel>)_mappingCoordinator.Map<EmployeeEntity, EmployeeModel>(EmployeeDetails.First());
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
