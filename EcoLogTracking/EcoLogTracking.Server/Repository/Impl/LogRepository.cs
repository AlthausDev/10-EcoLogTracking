﻿using Dapper;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using NLog;

namespace EcoLogTracking.Server.Repository.Impl
{
    public class LogRepository : ILogRepository
    {
        private readonly IConfiguration _configuration;
        private string _connectionString => _configuration.GetConnectionString("EcoLogTrackingDB");

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LogRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// MÉTODO QUE ACCEDE A LA BASE DE DATOS Y OBTIENE TODOS LOS REGISTROS
        /// </summary>
        /// <returns>Lista _connectionString los registros obtenidos de la base de datos: EcoLogTrackingDB</returns>
        public async Task<IEnumerable<Log>> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @"SELECT
                               *
                               FROM [dbo].[Log]";

                var logs = await connection.QueryAsync<Log>(query);
                return logs.ToList();
            }
        }

        public bool PostLog(Log log)
        {
            _logger.Info("Entra en Post log");
            using (var connection = new SqlConnection(_connectionString))
            {
                string query = @$"INSERT INTO [dbo].[Log](Logged, Level, Message, MachineName, Logger, Request_method, Stacktrace, File_name, All_event_properties) 
                                  VALUES(@Logged, @Level, @Message, @MachineName, @Logger, @Request_method, @Stacktrace, @File_name, @All_event_properties)";

                return connection.Execute(query, log) > 0;
            }
        }
    }
}
