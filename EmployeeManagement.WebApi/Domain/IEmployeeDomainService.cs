using EmployeeManagement.WebApi.Domain.Model;
using EmployeeManagement.WebApi.Model.API.Request;
using System.Diagnostics.Tracing;

namespace EmployeeManagement.WebApi.Domain
{
    /// <summary>
    /// Interface with definition for managing employees
    /// </summary>
    public interface IEmployeeDomainService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeRequest">Employee to be created in request format</param>
        /// <returns>A <see cref="Task"/> to await the create operation</returns>
        Task<IEnumerable<EmployeeModel>> CreateEmployeeAsync(IEnumerable<CreateEmployeeRequestModel> employeeRequest);
    }
}
