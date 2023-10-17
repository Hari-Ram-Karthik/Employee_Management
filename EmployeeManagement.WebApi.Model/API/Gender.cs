using System.Text.Json.Serialization;

namespace EmployeeManagement.WebApi.Model.API
{
    /// <summary>
    /// Gender
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        /// <summary>
        /// Male
        /// </summary>
        Male,

        /// <summary>
        /// Female
        /// </summary>
        Female
    }
}
