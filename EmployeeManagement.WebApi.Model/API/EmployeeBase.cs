using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EmployeeManagement.WebApi.Model.API
{
    public class EmployeeBase
    {
        /// <summary>
        /// Id of the employee.
        /// </summary>
        /// <example>1721</example>
        [Required]
        public int EmployeeID { get; set; }

        /// <summary>
        /// Employee Name
        /// </summary>
        /// <example>Hari</example>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gender of the employee.
        /// </summary>
        /// <example>Male</example>
        [Required]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Nickname for employee.
        /// </summary>
        /// <example>HRK</example>
        public string? NickName { get; set; }

        /// <summary>
        /// City of employee.
        /// </summary>
        /// <example>Coimbatore</example>
        public string? City { get; set; }

        /// <summary>
        /// State of employee.
        /// </summary>
        /// <example>Tamil Nadu</example>
        public string? State { get; set; }
    }
}
