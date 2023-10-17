using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.Model.API.Responses
{
    /// <summary>
    /// List of Employee(s)
    /// </summary>
    public class GetEmployeeRespose
    {
        public IList<GetEmployeeResponseObject> employeeResponse { get; set; }
    }
}
