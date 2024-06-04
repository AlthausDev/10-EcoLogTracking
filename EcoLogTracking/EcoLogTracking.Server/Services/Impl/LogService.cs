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

        public async Task<bool> Add(Log log, object? secondEntity)
        {
            try
            {
                return await LogRepository.Add(log, secondEntity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al agregar la categoría: {ex.Message}");
                throw;
            }
        }

        public async Task<int> Count()
        {
            try
            {
                return await LogRepository.Count();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void Delete(int entityId)
        {
            try
            {
                _ = LogRepository.Delete(entityId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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

        public async Task<IEnumerable<Log>> GetAllLogic()
        {
            try
            {
                return await LogRepository.GetAllLogic();
            }
            catch (Exception ex)
            {             
                throw;
            }
        }

        public async Task<Log> GetById(int entityId)
        {
            try
            {
                return await LogRepository.GetById(entityId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void LogicDelete(int entityId)
        {
            try
            {
                _ = LogRepository.LogicDelete(entityId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Log> Update(Log log, object? secondEntity)
        {
            try
            {
                return await LogRepository.Update(log, secondEntity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
