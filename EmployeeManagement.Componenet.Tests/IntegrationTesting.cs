using Microsoft.AspNetCore.Mvc.Testing;

namespace EmployeeManagement.Componenet.Tests
{
    public class IntegrationTesting
    {
        protected readonly HttpClient _httpClient;
        public IntegrationTesting() 
        {
            var appFactory= new WebApplicationFactory<Program>();
            _httpClient = appFactory.CreateClient();
        }
    }
}
