using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Infrastructure.Persistence.Mongo.Entity;

namespace EmployeeManagement.WebApi.Domain
{
    /// <summary>
    /// Interface with definitions to perform database operations to handle employee.
    /// </summary>
    public interface IEmployeeRepository
    {

        /// <summary>
        /// Insert the given employee to database.
        /// </summary>
        /// <param name="employeeToBeCreated">Employee to be created</param>
        Task<IEnumerable<EmployeeEntity>> InsertEmployeesAsync(IEnumerable<EmployeeModel> employeeToBeCreated);
    }
}
