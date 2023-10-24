using EmployeeManagement.WebApi;
using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Infrastructure.Mappers;
using EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo;
using EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo.Entity;
using EmployeeManagement.WebApi.Model.API;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using System.Text;
using Xunit;

namespace EmployeeManagement.Unit.Tests.WebApi.Infrastructure.Persistence.Mongo
{
    public class MongoEmployeeRepositoryTests
    {
        private Mock<MongoClientBase> _mockClient;
        private Mock<IMongoDatabase> _mockDatabase;
        private Mock<IMongoCollection<EmployeeEntity>> _mockCollection;
        private Mock<ISettingsProvider> _mockSettingsProvider;
        private IMappingCoordinator _mappingCoordinator;
        private Mock<ILogger<MongoEmployeeRepository>> _mockLogger;

        [Fact]
        public async Task ValidEmployees_InsertEmployeesAsync_ReturnsInsertedEmployee()
        {
            MongoEmployeeRepository mongoEmployeeRepository = CreateSut();
            int numberOfEmployeeToBeCreated = 3;
            IEnumerable<EmployeeModel> employeeToBeCreated = CreateEmployeeToBeCreated(numberOfEmployeeToBeCreated);
            IEnumerable<EmployeeModel> expectedEmployee = CreateEmployeeModels(numberOfEmployeeToBeCreated);

            IEnumerable<EmployeeModel> result=await mongoEmployeeRepository.InsertEmployeesAsync(employeeToBeCreated);

            Assert.Equal(expectedEmployee, result);
        }

        [Fact]
        public async Task NoEmployee_GetEmployeesByIdAsync_ReturnEmptyResponse()
        {
            MongoEmployeeRepository mongoEmployeeRepository = CreateSut();

            IEnumerable<EmployeeModel> result =await mongoEmployeeRepository.GetEmployeesByIdAsync(new List<int>() { 121, 122 });

            Assert.Empty(result);
        }

        [Fact]
        public async Task NoEmployee_GetEmployeesAsync_ReturnEmptyResponse()
        {
            MongoEmployeeRepository mongoEmployeeRepository = CreateSut();

            IEnumerable<EmployeeModel> result = await mongoEmployeeRepository.GetEmployeesAsync();

            Assert.Empty(result);
        }

        [Fact]
        public async Task UnknownDatabaseError_InsertSpecificationsAsync_ReturnsMongoException()
        {
            MongoEmployeeRepository mongoEmployeeRepository = CreateSut();
            IEnumerable<EmployeeModel> employeeToBeCreated = CreateEmployeeToBeCreated(5);
            MongoException mockException = new MongoException("Unknown error");
            _mockCollection.Setup(m => m.InsertManyAsync(It.IsAny<IEnumerable<EmployeeEntity>>(), It.IsAny<InsertManyOptions>(), It.IsAny<CancellationToken>()))
                .Throws(mockException);

            var actualException = await Assert.ThrowsAsync<NotImplementedException>(
                () => mongoEmployeeRepository.InsertEmployeesAsync(employeeToBeCreated));

            actualException.GetBaseException().GetType();
            Assert.Equal("System.NotImplementedException", actualException.GetType().ToString());
        }

        [Fact]
        public async Task DatabaseOperationTimeout_EditEmployeeAsync_ReturnsTimeoutException()
        {
            MongoEmployeeRepository mongoEmployeeRepository = CreateSut();
            IEnumerable<EmployeeModel> employeeToBeCreated = CreateEmployeeToBeCreated(1);
            _mockCollection.Setup(m => m.FindAsync(It.IsAny<FilterDefinition<EmployeeEntity>>(), It.IsAny<FindOptions<EmployeeEntity>>(), It.IsAny<CancellationToken>()))
                .Throws(new Mock<TimeoutException>().Object);

            var actualException = await Assert.ThrowsAsync<TimeoutException>(
                () => mongoEmployeeRepository.EditEmployeeAsync(employeeToBeCreated));

            Assert.Equal("System.TimeoutException", actualException.GetType().ToString());
        }

        private IEnumerable<EmployeeModel> CreateEmployeeToBeCreated(int numberOfEmployeeToBeCreated)
        {
            List<EmployeeModel> employeesToBeCreated = new();
            for (int iterator = 0; iterator < numberOfEmployeeToBeCreated; iterator++)
            {
                employeesToBeCreated.Add(CreateValidEmployee());
            }

            return employeesToBeCreated;
        }

        private IEnumerable<EmployeeModel> CreateEmployeeModels(int numberOfSpecificationsToBeCreated)
        {
            IEnumerable<EmployeeModel> employeeModels = CreateEmployeeToBeCreated(numberOfSpecificationsToBeCreated)
                .Select((Employee, index) =>
                {
                    return Employee;
                });

            return employeeModels;
        }


        private EmployeeModel CreateValidEmployee()
        {
            return new EmployeeModel()
            {
                EmployeeID = 123,
                Name = "Test",
                Gender = Gender.Male,
                NickName = "UT",
                City = "Coimbatore",
                State = "Tamil Nadu",
            };
        }

        //private void AssignMockDatabaseIdForInsertedEntities(
        //   IEnumerable<EmployeeEntity> insertedSpecifications)
        //{
        //    foreach ((EmployeeEntity entity, int index) in insertedSpecifications.Select((entity, index) => (entity, index)))
        //    {
        //        entity._id = ObjectId.Parse(ConvertIntegerTo24DigitHexString(index));
        //    }
        //}

        private string ConvertIntegerTo24DigitHexString(int value)
        {
            int desiredHexLength = 24;
            byte[] bytes = Encoding.UTF8.GetBytes(value.ToString());
            string hexString = BitConverter.ToString(bytes).Replace("-", string.Empty);

            if (hexString.Length < desiredHexLength)
            {
                int paddingLength = desiredHexLength - hexString.Length;
                hexString = hexString.PadLeft(hexString.Length + paddingLength, '0');
            }
            if (hexString.Length > desiredHexLength)
            {
                hexString = hexString[..desiredHexLength];
            }

            return hexString;
        }

        private MongoEmployeeRepository CreateSut()
        {
            return new MongoEmployeeRepository(
                CreateMockSettingsProvider(),
                CreateMockLogger(),
                CreateMappingCoordinator(),
                CreateMockMongoClientBase());
        }

        private ISettingsProvider CreateMockSettingsProvider()
        {
            _mockSettingsProvider = new Mock<ISettingsProvider>();
            _mockSettingsProvider.SetupGet(m => m.MongoConnectionString)
                .Returns("mongodb://mongodb:27017/employee");
            return _mockSettingsProvider.Object;
        }

        private ILogger<MongoEmployeeRepository> CreateMockLogger()
        {
            _mockLogger = new Mock<ILogger<MongoEmployeeRepository>>();
            return _mockLogger.Object;
        }

        private IMappingCoordinator CreateMappingCoordinator()
        {
            _mappingCoordinator = new MappingCoordinator();
            return _mappingCoordinator;
        }

        private MongoClientBase CreateMockMongoClientBase()
        {
            _mockCollection = new Mock<IMongoCollection<EmployeeEntity>>();

            _mockDatabase = new Mock<IMongoDatabase>();
            _mockDatabase.Setup(m => m.GetCollection<EmployeeEntity>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                .Returns(_mockCollection.Object);

            _mockClient = new Mock<MongoClientBase>();
            _mockClient.Setup(m => m.GetDatabase(It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>()))
                .Returns(_mockDatabase.Object);
            return _mockClient.Object;
        }
    }
}
