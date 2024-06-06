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
