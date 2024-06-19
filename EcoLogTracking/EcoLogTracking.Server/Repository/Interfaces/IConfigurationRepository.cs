using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Repository.Interfaces
{
    public interface IConfigurationRepository
    {

        public Task<bool> updateConfiguration(Configuration configuration);

        public Configuration getConfiguration();
    }
}
