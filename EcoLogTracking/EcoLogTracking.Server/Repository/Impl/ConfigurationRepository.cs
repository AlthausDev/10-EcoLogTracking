﻿using Dapper;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using NLog;
using System.Diagnostics;

namespace EcoLogTracking.Server.Repository.Impl
{
    public class ConfigurationRepository: IConfigurationRepository
    {
        private readonly IConfiguration _configuration;
        private string? con => _configuration.GetConnectionString("EcoLogTrackingDB");

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ConfigurationRepository(IConfiguration configuration)
        {
            _configuration = configuration;
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
                using (var connection = new SqlConnection(con))
                {
                    string query = "UPDATE Configuration SET Period = @time, DeletedDate = @date WHERE Id = 1";
                    bool response =  connection.Execute(query, new {time = configuration.Period, date = configuration.DeletedDate}) > 0;
                    return  response;
                }
            }
            catch 
            {
                return false;
            }
        }


        /// <summary>
        /// MÉTODO QUE OBTIENE LA CONFIGURACIÓN ESTABLECIDA PARA LA APLICACION
        /// </summary>
        /// <returns>Devuelve objeto Configuración con el período y el último borrado</returns>
        public Configuration getConfiguration()
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string query = @"SELECT Id, Period, DeletedDate FROM Configuration WHERE Id = 1";
                    Configuration config = connection.QuerySingle<Configuration>(query);
                    return config;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return null;
            }
        }
    }
}
