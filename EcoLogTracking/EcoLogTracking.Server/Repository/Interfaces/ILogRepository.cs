using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Repository.Interfaces
{
    public interface ILogRepository 
    {
        Task<IEnumerable<Log>> GetAll();      
        
        public bool PostLog(Log log);
    }
}
