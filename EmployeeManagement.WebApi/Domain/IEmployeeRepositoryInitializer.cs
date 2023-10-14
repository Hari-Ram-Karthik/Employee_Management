namespace EmployeeManagement.WebApi.Domain
{
    /// <summary>
    /// Interface with methods to initialize data store.
    /// </summary>
    public interface IEmployeeRepositoryInitializer
    {
        /// <summary>
        /// Perform the database initialization.
        /// </summary>
        Task InitializeAsync();
    }
}
