using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
