﻿using Dapper;
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

        public async Task<bool> updateConfig(int time)
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string query = "UPDATE Configuration SET Period = @time WHERE Id = 1";
                    bool response = connection.Execute(query, new {time}) > 0;
                    return  response;
                }
            }
            catch 
            {
                return false;
            }
        }


        public int getPeriod()
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string query = @"SELECT Period FROM Configuration";
                    int period = connection.QuerySingle<int>(query);
                    return period;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
                return 10;
            }
        }
    }
}
