using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Repository.Interfaces
{
    public interface ILogRepository : IGenericRepository<Log, object>
    {
        Task<IEnumerable<Log>> GetAll();        
    }
}
