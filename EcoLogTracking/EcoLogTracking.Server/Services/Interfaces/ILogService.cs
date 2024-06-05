using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Services.Interfaces
{
    public interface ILogService 
    {
        Task<IEnumerable<Log>> GetAll();

        public bool PostLog(Log log);
    }
}
