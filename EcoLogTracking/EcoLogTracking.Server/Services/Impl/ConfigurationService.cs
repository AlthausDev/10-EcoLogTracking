using EcoLogTracking.Server.Models;
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

        /// <summary>
        /// MÉTODO QUE ACTUALIZA LA CONFIGURACIÓN (PERIODO DE BORRADO)
        /// </summary>
        /// <param name="configuration">Objeto Configuración que porta el período de configuración y la fecha
        /// del último borrado </param>
        /// <returns>Return (true: si la consulta afecta a alguna tupla; false: si ocurre lo contrario)</returns>
        public async Task<bool> updateConfiguration(Configuration configuration)
        {
            try
            {
                bool response = await configurationRepository.updateConfiguration(configuration);
                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// MÉTODO QUE OBTIENE LA CONFIGURACIÓN ESTABLECIDA PARA LA APLICACION
        /// </summary>
        /// <returns>Devuelve objeto Configuración con el período y el último borrado</returns>
        public async Task<Configuration> getConfiguration()
        {
            try
            {
                Configuration response = configurationRepository.getConfiguration();
                return response;
            }
            catch (Exception E)
            {
                return null;
            }
        }
    }
}
