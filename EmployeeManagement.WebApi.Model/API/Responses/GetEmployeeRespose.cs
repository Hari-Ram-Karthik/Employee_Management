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
