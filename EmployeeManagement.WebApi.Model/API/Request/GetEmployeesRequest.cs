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
