using EmployeeManagement.WebApi.Domain;
using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Infrastructure.Mappers;
using EmployeeManagement.WebApi.Model.API.Request;
using EmployeeManagement.WebApi.Model.API.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
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
        /// asjdgi
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost]
        [ProducesResponseType(typeof(CreateEmployeeResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotImplemented)]
        public async Task<IActionResult> CreateEmployeeAsync(CreateEmployeeRequest request)
        {
            IEnumerable<CreateEmployeeRequestModel> employeeToBeCreated= _mappingCoordinator.Map<CreateEmployeeRequestObject, CreateEmployeeRequestModel>(request.Employee);
            await _employeeDomainService.CreateEmployeeAsync(employeeToBeCreated);
            return Ok(employeeToBeCreated);
        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/<EmployeeController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
