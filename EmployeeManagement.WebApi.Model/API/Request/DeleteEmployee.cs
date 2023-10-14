using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.Model.API.Request
{
    /// <summary>
    /// Details of the employee to be deleted
    /// </summary>
    public class DeleteEmployee
    {
        /// <summary>
        /// Id of the employee to be deleted
        /// </summary>
        public string EmployeeId { get; set; }
    }
}
