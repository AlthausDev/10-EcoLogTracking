using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Services.Interfaces;

namespace EcoLogTracking.Server.Services.Impl
{
    public class LogService : ILogService
    {
        private readonly ILogRepository LogRepository;
        private readonly IConfiguration configuration;

        public LogService(ILogRepository logRepository, IConfiguration configuration)
        {
            this.configuration = configuration;
            LogRepository = logRepository;
        }

        /// <summary>
        /// MÉTODO QUE ACCEDE A LA BASE DE DATOS Y OBTIENE TODOS LOS REGISTROS
        /// </summary>
        /// <returns>Lista _connectionString los registros obtenidos de la base de datos: EcoLogTrackingDB</returns>
        public async Task<IEnumerable<Log>> GetAll()
        {
            try
            {
                return await LogRepository.GetAll();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// MÉTODO QUE GUARDA EN LA BASE DE DATOS LOS REGISTROS RECIBIDOS
        /// </summary>
        /// <param name="log">NLog generado por los programas que implementan nuestro software</param>
        /// <returns>Boolean True si el guardado es satifactorio. False en caso contrario.</returns>
        public bool PostLog(Log log)
        {
            try
            {
                return  LogRepository.PostLog(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }
    }
}
