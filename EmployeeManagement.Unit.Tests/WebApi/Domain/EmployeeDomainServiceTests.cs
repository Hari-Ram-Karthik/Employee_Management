using EmployeeManagement.WebApi;
using EmployeeManagement.WebApi.Domain;
using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Infrastructure.Mappers;
using EmployeeManagement.WebApi.Model.API;
using Moq;
using Xunit;

namespace EmployeeManagement.Unit.Tests.WebApi.Domain
{
    public class EmployeeDomainServiceTests
    {
        private Mock<IEmployeeRepository> _mockEmployeeRepository;
        private IMappingCoordinator _mappingCoordinator;

        [Fact]
        public async Task ValidEmployees_CreateEmployees_ReturnsCreatedEmployees()
        {
            EmployeeDomainService employeeDomainService = CreateSut();
            int numberOfEmployeeToBeCreated = 5;
            List<CreateEmployeeRequestModel> employeesToBeCreated = CreateValidEmployeesToBeCreated(numberOfEmployeeToBeCreated);
            IEnumerable<EmployeeModel> expectedResult = _mappingCoordinator.Map<CreateEmployeeRequestModel, EmployeeModel>(employeesToBeCreated);
            _mockEmployeeRepository.Setup(m => m.InsertEmployeesAsync(
                 It.IsAny<IEnumerable<EmployeeModel>>()))
                 .ReturnsAsync(expectedResult);

            IEnumerable < EmployeeModel> actualResult = await employeeDomainService.CreateEmployeeAsync(employeesToBeCreated);

            _mockEmployeeRepository.Verify(m => m.InsertEmployeesAsync(
                It.IsAny<IEnumerable<EmployeeModel>>()), Times.Once);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task ValidEmployees_GetEmployees_ReturnsEmployees()
        {
            EmployeeDomainService employeeDomainService = CreateSut();
            int numberOfEmployeeToBeCreated = 5;
            List<CreateEmployeeRequestModel> employeesToBedisplayed = CreateValidEmployeesToBeCreated(numberOfEmployeeToBeCreated);
            IEnumerable<EmployeeModel> expectedResult = _mappingCoordinator.Map<CreateEmployeeRequestModel, EmployeeModel>(employeesToBedisplayed);
            _mockEmployeeRepository.Setup(m => m.GetEmployeesAsync()).ReturnsAsync(expectedResult);

            IEnumerable<EmployeeModel> actualResult = await employeeDomainService.GetEmployees();

            _mockEmployeeRepository.Verify(m => m.GetEmployeesAsync(), Times.Once);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task InValidEmployees_GetEmployeesById_ReturnsEmptyEmployees()
        {
            EmployeeDomainService employeeDomainService = CreateSut();
            List < int > employeeIds= new();
            employeeIds.Add(1721);
            employeeIds.Add(1710);


            IEnumerable<EmployeeModel> expectedResult = Enumerable.Empty<EmployeeModel>();
            _mockEmployeeRepository.Setup(m => m.GetEmployeesByIdAsync(
                 It.IsAny<List<int>>()))
                 .ReturnsAsync(expectedResult); ;

            IEnumerable<EmployeeModel> actualResult = await employeeDomainService.GetEmployeesById(employeeIds);

            _mockEmployeeRepository.Verify(m => m.GetEmployeesByIdAsync(
                It.IsAny<List<int>>()), Times.Once);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task InValidEmployees_EditEmployees_ReturnsEmptyEmployee()
        {
            EmployeeDomainService employeeDomainService = CreateSut();
            List<EditEmployeeRequestModel> employeeToBeEdited = new();
            employeeToBeEdited.Add(new EditEmployeeRequestModel()
            {
                EmployeeID = 123,
                Name = "Test",
                Gender = Gender.Male,
                NickName = "UT",
                City = "Coimbatore",
                State = "Tamil Nadu",
            });
            IEnumerable<EmployeeModel> expectedResult = Enumerable.Empty<EmployeeModel>();
            _mockEmployeeRepository.Setup(m => m.EditEmployeeAsync(
                 It.IsAny<IEnumerable<EmployeeModel>>()))
                 .ReturnsAsync(expectedResult);

            IEnumerable<EmployeeModel> actualResult = await employeeDomainService.EditEmployee(employeeToBeEdited);

            _mockEmployeeRepository.Verify(m => m.EditEmployeeAsync(
                It.IsAny<IEnumerable<EmployeeModel>>()), Times.Once);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task InValidEmployees_DeleteEmployees_ReturnsEmptyEmployee()
        {
            EmployeeDomainService employeeDomainService = CreateSut();
            List<DeleteEmployeeRequestModel> employeesToBeDeleted = new();
            employeesToBeDeleted.Add(new DeleteEmployeeRequestModel()
            {
                EmployeeId=1721
            });
            IEnumerable<EmployeeModel> expectedResult = Enumerable.Empty<EmployeeModel>();
            _mockEmployeeRepository.Setup(m => m.DeleteEmployeeAsync(
                 It.IsAny<IEnumerable<DeleteEmployeeRequestModel>>()))
                 .ReturnsAsync(expectedResult);

            IEnumerable<EmployeeModel> actualResult = await employeeDomainService.DeleteEmployee(employeesToBeDeleted);

            _mockEmployeeRepository.Verify(m => m.DeleteEmployeeAsync(
                It.IsAny<IEnumerable<DeleteEmployeeRequestModel>>()), Times.Once);
            Assert.Equal(expectedResult, actualResult);
        }

        private List<CreateEmployeeRequestModel> CreateValidEmployeesToBeCreated(int numberOfEmployeeToBeCreated)
        {
            List<CreateEmployeeRequestModel> employeesToBeCreated = new();
            for (int iterator = 0; iterator < numberOfEmployeeToBeCreated; iterator++)
            {
                employeesToBeCreated.Add(CreateValidEmployeeRequestModel());
            }

            return employeesToBeCreated;
        }

        private CreateEmployeeRequestModel CreateValidEmployeeRequestModel()
        {
            return new CreateEmployeeRequestModel()
            {
                EmployeeID = 123,
                Name = "Test",
                Gender = Gender.Male,
                NickName = "UT",
                City = "Coimbatore",
                State = "Tamil Nadu",
            };
        }

        private EmployeeDomainService CreateSut()
        {
            return new EmployeeDomainService(
                CreateMockEmployeeRepository(),
                CreateMappingCoordinator()
                );
        }

        private IEmployeeRepository CreateMockEmployeeRepository()
        {
            _mockEmployeeRepository = new Mock<IEmployeeRepository>();
            return _mockEmployeeRepository.Object;
        }

        private IMappingCoordinator CreateMappingCoordinator()
        {
            _mappingCoordinator = new MappingCoordinator();
            return _mappingCoordinator;
        }
    }
}
