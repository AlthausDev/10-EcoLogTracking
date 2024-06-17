using EcoLogTracking.Server.Repository.Impl;
using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Services.Interfaces;
using todoAPI.Helpers;

namespace EcoLogTracking.Server.Services.Impl
{
    public class ConfigurationService : IConfigurationService
    {
        public readonly IConfigurationRepository configurationRepository;
        private readonly EncryptionHelper encryptionHelper;

        public ConfigurationService(IConfigurationRepository configRepository, EncryptionHelper encryptionHelper)
        {
            this.configurationRepository = configRepository;
            this.encryptionHelper = encryptionHelper;
        }

        public async Task<bool> updateConfig(int time)
        {
            try
            {
                bool response = await configurationRepository.updateConfig(time);
                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
