using EmployeeManagement.WebApi.Model.API;

namespace EmployeeManagement.WebApi.Domain.Model
{
    public class EditEmployeeRequestModel
    {
        /// <summary>
        /// Id of the employee.
        /// </summary>
        /// <example>1721</example>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Employee Name
        /// </summary>
        /// <example>Hari</example>
        public string Name { get; set; }

        /// <summary>
        /// Gender of the employee.
        /// </summary>
        /// <example>Male</example>
        public Gender Gender { get; set; }

        /// <summary>
        /// Nickname for employee.
        /// </summary>
        /// <example>HRK</example>
        public string NickName { get; set; }

        /// <summary>
        /// City of employee.
        /// </summary>
        /// <example>Coimbatore</example>
        public string City { get; set; }

        /// <summary>
        /// State of employee.
        /// </summary>
        /// <example>Tamil Nadu</example>
        public string State { get; set; }

        /// <summary>
        /// ISO-8601 formatted timestamp indicating when the specification was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// ISO-8601 formatted timestamp indicating when the specification was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
