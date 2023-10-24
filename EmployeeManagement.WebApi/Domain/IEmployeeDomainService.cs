using EmployeeManagement.WebApi.Domain.Model;

namespace EmployeeManagement.WebApi.Domain
{
    /// <summary>
    /// Interface with definition for managing employees
    /// </summary>
    public interface IEmployeeDomainService
    {
        /// <summary>
        /// Create the employee
        /// </summary>
        /// <param name="employeeRequest">Employee to be created in request format</param>
        /// <returns>A <see cref="Task"/> to await the create operation</returns>
        Task<IEnumerable<EmployeeModel>> CreateEmployeeAsync(IEnumerable<CreateEmployeeRequestModel> employeeRequest);

        /// <summary>
        /// Get employee details
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> GetEmployees();

        /// <summary>
        /// Get employee details
        /// </summary>
        /// <param name="employeeIds">list of employee id to get from server</param>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> GetEmployeesById(List<int> employeeIds);
        
        /// <summary>
        /// Edit employee
        /// </summary>
        /// <param name="employeeIds">list of employee id to get from server</param>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> EditEmployee(IEnumerable<EditEmployeeRequestModel> employee);
        
        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="employeeId">Employee id to delete from server</param>
        /// <returns></returns>
        Task<IEnumerable<EmployeeModel>> DeleteEmployee(IEnumerable<DeleteEmployeeRequestModel> employeeId);
    }
}
