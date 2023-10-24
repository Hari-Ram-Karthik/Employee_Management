using EmployeeManagement.WebApi.Domain;
using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Model.API.Request;
using EmployeeManagement.WebApi.Model.API.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployeeDomainService _employeeDomainService;
        private readonly IMappingCoordinator _mappingCoordinator;

        /// <summary>
        /// Constructor for Employee controller
        /// </summary>
        /// <param name="logger">Dependency injection for logger</param>
        /// <param name="employeeDomainService">Dependency injection for employee domain service</param>
        /// <param name="mappingCoordinator">Dependency injection for mapper</param>
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeDomainService employeeDomainService, IMappingCoordinator mappingCoordinator)
        {
            _logger = logger;
            _employeeDomainService = employeeDomainService;
            _mappingCoordinator = mappingCoordinator;
        }

        /// <summary>
        /// Creates one or more employee
        /// </summary>
        /// <param name="request">Information to create employee</param>
        /// <response code="201">Create Employee Success Response</response>
        [HttpPost("CreateEmployee")]
        [ProducesResponseType(typeof(CreateEmployeeResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotImplemented)]
        public async Task<IActionResult> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            IEnumerable<CreateEmployeeRequestModel> employeeToBeCreated= _mappingCoordinator.Map<CreateEmployeeRequestObject, CreateEmployeeRequestModel>(request.Employee);
            IEnumerable < EmployeeModel > employeesCreated = await _employeeDomainService.CreateEmployeeAsync(employeeToBeCreated);

            CreateEmployeeResponse response = new()
            {
                CreatedEmployee = _mappingCoordinator.Map<EmployeeModel, CreateEmployeeResponseObject>((employeesCreated.ToList())).ToList()
            };

            return Json(response,HttpStatusCode.Created);
        }

        /// <summary>
        /// Get employees details
        /// </summary>
        /// <response code="200">Displayed Employees detail Success Response</response>
        [HttpGet("Employee")]
        [ProducesResponseType(typeof(GetEmployeeRespose), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotImplemented)]
        public async Task<IActionResult> GetEmployees()
        {
            IEnumerable<EmployeeModel> employees = await _employeeDomainService.GetEmployees();
            GetEmployeeRespose response = new()
            {
                employeeResponse = _mappingCoordinator.Map<EmployeeModel, GetEmployeeResponseObject>((employees.ToList())).ToList()
            };

            return Json(response, HttpStatusCode.OK);
        }

        /// <summary>
        /// Get employees details
        /// </summary>
        /// <response code="200">Displayed Employees detail Success Response</response>
        [HttpPost("EmployeeByIds")]
        [ProducesResponseType(typeof(GetEmployeeRespose), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotImplemented)]
        public async Task<IActionResult> GetEmployeesById(List<int> employeeIds)
        {
            IEnumerable<EmployeeModel> employees = await _employeeDomainService.GetEmployeesById(employeeIds);
            GetEmployeeRespose response = new()
            {
                employeeResponse = _mappingCoordinator.Map<EmployeeModel, GetEmployeeResponseObject>((employees.ToList())).ToList()
            };

            return Json(response, HttpStatusCode.OK);
        }

        /// <summary>
        /// Get employees details
        /// </summary>
        /// <response code="200">Displayed Employees detail Success Response</response>
        /// <response code="404">Employees not found Response</response>
        [HttpPut("EditEmployee")]
        [ProducesResponseType(typeof(GetEmployeeRespose), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string),(int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> EditEmployee(IEnumerable<EditEmployeeRequest> employee)
        {
            IEnumerable<EditEmployeeRequestModel> employeeToBeEdited = _mappingCoordinator.Map<EditEmployeeRequest, EditEmployeeRequestModel>(employee);
            IEnumerable<EmployeeModel> employees = await _employeeDomainService.EditEmployee(employeeToBeEdited);
            if (employees.Count() == 0)
            {
                return Json("Employee not found",HttpStatusCode.NotFound);
            }
            IEnumerable<EditEmployeeRespose> response = _mappingCoordinator.Map<EmployeeModel, EditEmployeeRespose>(employees);

            return Json(response, HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete employee by Ids
        /// </summary>
        /// <param name="employeeId">Id of employee to delete</param>
        /// <returns>Displayed Employees detail Success Response</returns>
        /// <response code="200">Employees deleted Success Response</response>
        /// <response code="404">Employees not found Response</response>
        [HttpDelete("DeleteEmployeeById")]
        [ProducesResponseType(typeof(DeleteEmployeeResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteEmployee(IEnumerable<DeleteEmployeeRequest> employeeId)
        {
            IEnumerable<DeleteEmployeeRequestModel> employeeToBeDeleted = _mappingCoordinator.Map<DeleteEmployeeRequest, DeleteEmployeeRequestModel>(employeeId);
            IEnumerable<EmployeeModel> employeeDetails = await _employeeDomainService.DeleteEmployee(employeeToBeDeleted);
            if(employeeDetails.Count() == 0)
            {
                return Json("Employee not found", HttpStatusCode.NotFound);
            }
            IEnumerable<DeleteEmployeeResponse> response = _mappingCoordinator.Map<EmployeeModel, DeleteEmployeeResponse>(employeeDetails);

            return Json(response, HttpStatusCode.OK);
        }

        private JsonResult Json(object value, HttpStatusCode httpStatusCode)
        {
            return new JsonResult(value)
            {
                StatusCode = (int)httpStatusCode
            };
        }
    }
}
