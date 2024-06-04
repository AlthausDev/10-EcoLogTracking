

using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Services.Interfaces
{
    public interface IGenericService<T, Y> where T : BaseModel
    {
        Task<bool> Add(T entity, Y? secondEntity);
        Task<T> Update(T entity, Y? secondEntity);
        void Delete(int entityId);
        void LogicDelete(int entityId);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllLogic();
        Task<T> GetById(int entityId);
        Task<int> Count();
    }
}
