﻿using EmployeeManagement.WebApi.Model.API;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.WebApi.Domain.Model
{
    public class CreateEmployeeRequestModel
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
    }
}
