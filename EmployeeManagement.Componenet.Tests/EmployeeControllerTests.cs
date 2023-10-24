using EmployeeManagement.WebApi.Model.API;
using EmployeeManagement.WebApi.Model.API.Request;
using EmployeeManagement.WebApi.Model.API.Responses;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace EmployeeManagement.Componenet.Tests
{
    public class EmployeeControllerTests :IntegrationTesting
    {
        [Fact]
        public async Task ValidEmployees_CreateEmployeeAsync_ReturnsCreatedResponseWithEmployeeData()
        {
            int numberOfEmployeesToBeCreated = 3;
            CreateEmployeeRequest createEmployeeRequest = CreateValidEmployeeRequest(numberOfEmployeesToBeCreated);
            IEnumerable<CreateEmployeeResponseObject> expectedCreatedEmployees =
                CreateValidCreateEmployeeResponseObjects(createEmployeeRequest.Employee);

            var response = await _httpClient.PostAsJsonAsync("api/Employee/CreateEmployee", createEmployeeRequest);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task ValidEmployees_GetEmployeeAsync_ReturnsResponseWithEmployeeData()
        {
            var response = await _httpClient.GetAsync("api/Employee/Employee");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ValidEmployees_GetEmployeeByIdAsync_ReturnsResponseWithEmployeeData()
        {
            List<int> employeeIds = new List<int>();
            employeeIds.Add(123);
            var response = await _httpClient.PostAsJsonAsync("api/Employee/EmployeeByIds",employeeIds);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task ValidEmployees_EditEmployeeAsync_ReturnsCreatedResponseWithEmployeeData()
        {
            EmployeeBase employee = new EmployeeBase()
            {
                EmployeeID = 1721,
                Name = "Test",
                Gender = Gender.Male,
                NickName = "UT",
                City = "Coimbatore",
                State = "Tamil Nadu",
            };
            EditEmployeeRequest EditEmployeeRequest = new EditEmployeeRequest()
            {
                EmployeeID = 123,
                Name = "TestNewName",
                Gender = Gender.Male,
                NickName = "UT",
                City = "Coimbatore",
                State = "Tamil Nadu",
            };

            var response = await _httpClient.PutAsJsonAsync("api/Employee/EditEmployee", EditEmployeeRequest);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        private CreateEmployeeRequest CreateValidEmployeeRequest(int numberOfEmployeesToBeCreated)
        {
            List<CreateEmployeeRequestObject> employeesToBeCreated = new();
            for (int iterator = 0; iterator < numberOfEmployeesToBeCreated; iterator++)
            {
                employeesToBeCreated.Add(
                    new CreateEmployeeRequestObject()
                    {
                        EmployeeID = 123,
                        Name = "Test",
                        Gender = Gender.Male,
                        NickName = "UT",
                        City = "Coimbatore",
                        State = "Tamil Nadu",
                    });
            }

            return new CreateEmployeeRequest()
            {
                Employee = employeesToBeCreated
            };
        }

        private IEnumerable<CreateEmployeeResponseObject> CreateValidCreateEmployeeResponseObjects(
        IList<CreateEmployeeRequestObject> requestObjects)
        {
            DateTime currentTime = DateTime.UtcNow;
            List<CreateEmployeeResponseObject> createdEmployees = new();
            for (int iterator = 0; iterator < requestObjects.Count; iterator++)
            {
                createdEmployees.Add(new CreateEmployeeResponseObject()
                {
                    CreatedAt = currentTime
                });
            }

            return createdEmployees;
        }
    }
}
