namespace EmployeeManagement.WebApi.Model.API.Request
{
    /// <summary>
    /// Details of the employee to be deleted
    /// </summary>
    public class DeleteEmployeeRequest
    {
        /// <summary>
        /// Id of the employee to be deleted
        /// </summary>
        public int EmployeeId { get; set; }
    }
}
