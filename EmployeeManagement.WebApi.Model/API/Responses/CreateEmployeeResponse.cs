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
        public IList<CreateEmployeeResponseObject> CreatedEmployee { get; set; }
    }
}
