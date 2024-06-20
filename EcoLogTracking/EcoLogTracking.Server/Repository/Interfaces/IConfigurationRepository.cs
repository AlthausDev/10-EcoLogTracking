using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Repository.Interfaces
{
    public interface IConfigurationRepository
    {

        /// <summary>
        /// MÉTODO QUE ACTUALIZA LA CONFIGURACIÓN (PERIODO DE BORRADO)
        /// </summary>
        /// <param name="configuration">Objeto Configuración que porta el período de configuración y la fecha
        /// del último borrado </param>
        /// <returns>Return (true: si la consulta afecta a alguna tupla; false: si ocurre lo contrario)</returns>
        public Task<bool> updateConfiguration(Configuration configuration);

        /// <summary>
        /// MÉTODO QUE OBTIENE LA CONFIGURACIÓN ESTABLECIDA PARA LA APLICACION
        /// </summary>
        /// <returns>Devuelve objeto Configuración con el período y el último borrado</returns>
        public Configuration getConfiguration();
    }
}
