using BlazorWebPage.Shared;

namespace BlazorWebPage.Server.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);       
        void Remove(int id);
        void LogicRemove(int id);

    }
}
