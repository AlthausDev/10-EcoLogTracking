using Dapper;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace EcoLogTracking.Server.Repository.Impl
{
    public class LogRepository : ILogRepository
    {
        private readonly IConfiguration _configuration;
        private string? con => _configuration.GetConnectionString("EcoLogTrackingDB");
        


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
            using (var connection = new SqlConnection(con)) {
                string query = @"SELECT
                               ID, MachineName, Logged,
                               Level, Message, Logger,
                               request_method 
                               FROM NLog";
                var logs =  await connection.QueryAsync<Log>(query);
                return logs.ToList();
            }
        }



    }
}
