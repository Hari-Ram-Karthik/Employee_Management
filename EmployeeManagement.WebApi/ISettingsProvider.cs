namespace EmployeeManagement.WebApi
{
    /// <summary>
    /// Definition for settings provider
    /// </summary>
    public interface ISettingsProvider
    {
        /// <summary>
        /// Mongo Connection String
        /// </summary>
        string MongoConnectionString { get; }
    }
}