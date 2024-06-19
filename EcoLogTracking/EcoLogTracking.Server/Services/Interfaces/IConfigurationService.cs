using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Services.Interfaces
{
    public interface IConfigurationService
    {
        public Task<bool> updateConfiguration(Configuration configuration);

        
        public Task<Configuration> getConfiguration();
    }
}
