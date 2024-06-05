using Dapper;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using NLog;
using System.Data;

namespace EcoLogTracking.Server.Repository.Impl
{
    public class LogRepository : ILogRepository
    {
        private readonly IConfiguration _configuration;
        private string? con => _configuration.GetConnectionString("EcoLogTrackingDB");

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LogRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// MÉTODO QUE ACCEDE A LA BASE DE DATOS Y OBTIENE TODOS LOS REGISTROS
        /// </summary>
        /// <returns>Lista con los registros obtenidos de la base de datos: EcoLogTrackingDB</returns>
        public async Task<IEnumerable<Log>> GetAll()
        {
            _logger.Info("Entra en get log");
            using (var connection = new SqlConnection(con)) {
                string query = @"SELECT
                               ID, MachineName, Logged,
                               Level, Message, Logger,
                               request_method, Stacktrace, File_name, All_event_properties
                               FROM NLog";

                var logs =  await connection.QueryAsync<Log>(query);
                return logs.ToList();
            }
        }
 //      
        public bool PostLog(Log log)
        {
            _logger.Info("Entra en Post log");
            using (var connection = new SqlConnection(con))
            {
                string query = @$"INSERT INTO NLog(MachineName,
        Logged,
    Level,
   Message,
   Logger,
method,
Stacktrace,
File_name,
All_event_properties) Values('{log.MachineName}','{log.Logged}','{log.Level}','{log.Message}','{log.Logger}',
                                '{log.Method}','{log.Stacktrace}','{log.File_name}','{log.All_event_properties}')";
                return  connection.Execute(query) > 0;
                
            }
        }
    }
}
