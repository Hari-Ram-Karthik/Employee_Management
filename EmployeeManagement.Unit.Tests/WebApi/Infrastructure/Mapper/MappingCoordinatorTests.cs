using EmployeeManagement.WebApi.Infrastructure.Mappers;
using Xunit;

namespace EmployeeManagement.Unit.Tests.WebApi.Infrastructure.Mapper
{
    public class MappingCoordinatorTests
    {
        [Fact]
        public async Task MappingCoordinator_ValidateMappedConfiguration_ExecutedSuccessfully()
        {
            var mappingCoordinator = new TestMappingCoordinator();
            mappingCoordinator.AssertConfigurationIsValid();
        }

        /// <summary>
        /// Inherits the mapping coordinator class and implements a method to verify configurations
        /// </summary>
        private class TestMappingCoordinator : MappingCoordinator
        {
            /// <summary>
            /// Dry run all configured type maps
            /// </summary>
            public void AssertConfigurationIsValid()
            {
                Mapper.ConfigurationProvider.AssertConfigurationIsValid();
            }
        }
    }
}