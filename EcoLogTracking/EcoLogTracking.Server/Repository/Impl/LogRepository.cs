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
        private string ConnectionString => _configuration.GetConnectionString("EcoLogTrackingDB");

        public LogRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> Add(Log log, object? secondEntity)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
                {
                   
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar categoría: {ex.Message}");
                return false;
            }
        }

        public async Task<int> Count()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return 0;

            }
        }

        public async Task<bool> Delete(int entityId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
                {
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar: {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return null;
            }
        }

        public async Task<IEnumerable<Log>> GetAllLogic()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return null;
            }
        }

        public async Task<Log> GetById(int entityId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                return null;
            }
        }

        public async Task<bool> LogicDelete(int entityId)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
                {
                    return true;
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar lógicamente categoría: {ex.Message}");
                return false;
            }
        }

        public async Task<Log> Update(Log log, object? secondEntity)
        {
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
                {
                    return null;

                }
                return log;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar categoría: {ex.Message}");
                return null;
            }
        }
   
    }
}
