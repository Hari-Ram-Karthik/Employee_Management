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
                employee.UpdatedAt = DateTime.UtcNow;
            });

            IEnumerable<EmployeeModel> employeeModels = await _employeeRepository.InsertEmployeesAsync(employees);
            return employeeModels;
        }
    }
}
