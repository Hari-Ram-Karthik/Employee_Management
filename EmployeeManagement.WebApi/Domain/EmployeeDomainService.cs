using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Infrastructure.Mappers;
using EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo.Entity;
using EmployeeManagement.WebApi.Model.API.Request;
using EmployeeManagement.WebApi.Model.API.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;

namespace EmployeeManagement.WebApi.Domain
{
    internal class EmployeeDomainService : IEmployeeDomainService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMappingCoordinator _mappingCoordinator;

        public EmployeeDomainService(IEmployeeRepository employeeRepository, IMappingCoordinator mappingCoordinator)
        {
            _employeeRepository = employeeRepository;
            _mappingCoordinator = mappingCoordinator;
        }

        public async Task<IEnumerable<EmployeeModel>> CreateEmployeeAsync(IEnumerable<CreateEmployeeRequestModel> employeesToBeCreated)
        {
            List<CreateEmployeeRequestModel> employeesToBeCreatedList = employeesToBeCreated.ToList();
            List<EmployeeModel> employees = _mappingCoordinator.Map<CreateEmployeeRequestModel, EmployeeModel>(employeesToBeCreatedList).ToList();
            employees.ForEach(employee =>
            {
                employee.CreatedAt = DateTime.UtcNow;
            });

            IEnumerable<EmployeeModel> employeeModels = await _employeeRepository.InsertEmployeesAsync(employees);
            return employeeModels;
        }

        public async Task<IEnumerable<EmployeeModel>> EditEmployee(IEnumerable<EditEmployeeRequestModel> employee)
        {
            IEnumerable<EmployeeModel> employeeModel = _mappingCoordinator.Map<EditEmployeeRequestModel, EmployeeModel>(employee);
            IEnumerable<EmployeeModel> employeeResponse = await _employeeRepository.EditEmployeeAsync(employeeModel);

            return employeeResponse;
        }
        
        public async Task<IEnumerable<EmployeeModel>> DeleteEmployee(IEnumerable<DeleteEmployeeRequestModel> employeeId)
        {
            IEnumerable<EmployeeModel> employeeResponse = await _employeeRepository.DeleteEmployeeAsync(employeeId);

            return employeeResponse;
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployees()
        {
            IEnumerable<EmployeeModel> employees = await _employeeRepository.GetEmployeesAsync();
            return employees;
        }

        public async Task<IEnumerable<EmployeeModel>> GetEmployeesById(List<int> employeeIds)
        {
            IEnumerable<EmployeeModel> employees = await _employeeRepository.GetEmployeesByIdAsync(employeeIds);
            return employees;
        }
    }
}
