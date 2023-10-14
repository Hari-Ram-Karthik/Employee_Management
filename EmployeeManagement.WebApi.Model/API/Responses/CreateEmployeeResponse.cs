using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.Model.API.Responses
{
    /// <summary>
    /// Information about the created employee.
    /// </summary>
    public class CreateEmployeeResponse
    {
        /// <summary>
        /// Information about the created employee.
        /// </summary>
        public CreateEmployeeResponseObject CreatedEmployee { get; set; }
    }
}
