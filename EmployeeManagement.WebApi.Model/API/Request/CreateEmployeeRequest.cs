namespace EmployeeManagement.WebApi.Model.API.Request
{
    /// <summary>
    /// Employee(s) to be created.
    /// </summary>
    public class CreateEmployeeRequest
    {
        /// <summary>
        /// List of employee to be created.
        /// </summary>
        public IList<CreateEmployeeRequestObject> Employee { get; set; }
    }
}
