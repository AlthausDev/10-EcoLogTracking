using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Services.Interfaces
{
    public interface ILogService 
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

        /// <summary>
        /// MÉTODO QUE FILTRA LOS REGISTROS EN LA BASE DE DATOS EN FUNCIÓN DE SU FECHA
        /// </summary>
        /// <param name="start">Fecha a partir de la cual se quieren obtener los registros</param>
        /// <param name="end">Fecha hasta la cual se quieren obtener los registros</param>
        /// <returns>IEnumerable con la lista de registros existentes en el rango de fechas proporcionado</returns>
        public Task<IEnumerable<Log>> GetLogsBetween(DateTime start, DateTime end);
    }
}
