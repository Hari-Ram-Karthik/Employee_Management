using EmployeeManagement.WebApi.Domain.Model;

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
        Task<IEnumerable<EmployeeModel>> InsertEmployeesAsync(IEnumerable<EmployeeModel> employeeToBeCreated);

        /// <summary>
        /// Get employees from datebase
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> GetEmployeesAsync();

        /// <summary>
        /// Get employees from datebase
        /// </summary>
        /// <param name="employeeIds">List of ids to get from database</param>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> GetEmployeesByIdAsync(List<int> employeeIds); 
        
        /// <summary>
        /// Edit employees in datebase
        /// </summary>
        /// <param name="employeeToBeEdited">Employee to edit</param>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> EditEmployeeAsync(IEnumerable<EmployeeModel> employeeToBeEdited);

        /// <summary>
        /// Delete employees in datebase
        /// </summary>
        /// <param name="employeeIdToBeDeleted">Employee id to delete</param>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> DeleteEmployeeAsync(IEnumerable<DeleteEmployeeRequestModel> employeeIdToBeDeleted);
    }
}
