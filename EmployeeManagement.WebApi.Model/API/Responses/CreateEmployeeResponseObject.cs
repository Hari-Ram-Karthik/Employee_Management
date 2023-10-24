using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.WebApi.Model.API.Responses
{
    public class CreateEmployeeResponseObject
    {
        /// <summary>
        /// Id of the employee.
        /// </summary>
        /// <example>1721</example>
        [Required]
        public int? EmployeeID { get; set; }

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
        /// ISO-8601 formatted timestamp indicating when the employee was created.
        /// </summary>
        /// <example>2018-05-09T15:07:42.527921Z</example>
        public DateTime CreatedAt { get; set; }
    }
}
