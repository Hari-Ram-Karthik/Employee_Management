using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using System.Xml;

namespace EmployeeManagement.WebApi.Infrastructure.Bootstrap
{
    internal class ApplicationSettingsProvider : ISettingsProvider
    {
        public ApplicationSettingsProvider(IConfiguration configuration)
        {
            MongoConnectionString = configuration.GetValue<string>(SettingsConstants.MongoConnectionString) ?? "mongodb://localhost:27017";
}

        /// <inheritdoc/>
        public string MongoConnectionString { get; private set; }
    }
}

