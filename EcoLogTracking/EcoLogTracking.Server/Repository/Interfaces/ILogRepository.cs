using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Repository.Interfaces
{
    public interface ILogRepository 
    {
        /// <summary>
        /// MÉTODO QUE ACCEDE A LA BASE DE DATOS Y OBTIENE TODOS LOS REGISTROS
        /// </summary>
        /// <returns>Lista _connectionString los registros obtenidos de la base de datos: EcoLogTrackingDB</returns>
        Task<IEnumerable<Log>> GetAll();

        /// <summary>
        /// MÉTODO QUE GUARDA EN LA BASE DE DATOS LOS REGISTROS RECIBIDOS
        /// </summary>
        /// <param name="log">NLog generado por los programas que implementan nuestro software</param>
        /// <returns>Boolean True si el guardado es satifactorio. False en caso contrario.</returns>
        public bool PostLog(Log log);
    }
}
