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
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// MÉTODO QUE FILTRA LOS REGISTROS EN LA BASE DE DATOS EN FUNCIÓN DE SU FECHA
        /// </summary>
        /// <param name="start">Fecha a partir de la cual se quieren obtener los registros</param>
        /// <param name="end">Fecha hasta la cual se quieren obtener los registros</param>
        /// <returns>IEnumerable con la lista de registros existentes en el rango de fechas proporcionado</returns>
        public Task<IEnumerable<Log>> GetLogsBetween(DateFilter dates)
        {
            try
            {
                return LogRepository.GetLogsBetween(dates);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// MÉTODO QUE GUARDA EN LA BASE DE DATOS LOS REGISTROS RECIBIDOS
        /// </summary>
        /// <param name="log">NLog generado por los programas que implementan nuestro software</param>
        /// <returns>Boolean True si el guardado es satifactorio. False en caso contrario.</returns>
        public async Task<bool> PostLog(Log log)
        {
            try
            {
                return await LogRepository.PostLog(log);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return false;
            }
        }

        /// <summary>
        /// MÉTODO QUE ELIMINA LOS LOGS ANTERIORES AL NÚMERO DE DÍAS QUE RECIBE EL MÉTODO
        /// </summary>
        /// <param name="numDias">Número de días desde los que se quieren mantener los logs</param>
        /// RECIBE EL NÚMERO DE DÍAS Y OBTIENE LA FECHA HASTA LA CUAL DEBE REALIZARSE EL BORRADO
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: caso contrario)</returns>
        public async Task<bool> DeleteLogsByDate(int numDias)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                DateTime dateTimeFilter = dateTime.AddDays(-numDias);
                bool response = await LogRepository.DeleteLogsByDate(dateTimeFilter);
                return response;
            }
            catch
            {
                return false;
            }
        }


        public async Task<IEnumerable<Log>> GetLogsByDate(DateTime date) { 
            return await LogRepository.GetLogsByDate(date);
        }
    }
}
