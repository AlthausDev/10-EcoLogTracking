using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Repository.Interfaces
{
    public interface IGenericRepository<T, Y> where T : BaseModel
    {
        Task<bool> Add(T entity, Y? secondEntity);
        Task<T> Update(T entity, Y? secondEntity);
        Task<bool> Delete(int entityId);
        Task<bool> LogicDelete(int entityId);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllLogic();
        Task<T> GetById(int entityId);
        Task<int> Count();
    }
}
