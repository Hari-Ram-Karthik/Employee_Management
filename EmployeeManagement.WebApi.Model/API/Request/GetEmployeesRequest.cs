using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.Model.API.Request
{
    public class GetEmployeesRequest
    {
        /// <summary>
        /// Ids of the employee to be viewed
        /// </summary>
        public IList<int> EmployeeIds { get; set; }
    }
}
