//using BlazorWebPage.Server.Repository.Interfaces;
//using System.Linq.Expressions;

//namespace BlazorWebPage.Server.Repository.Impl
//{
//    public class GenericRepository<T> : IGenericRepository<T> where T : class
//    {
//        public string dbConnection = "Server=ECO3042;Database=BlazorWebPageTest;UserId=sa;Password=sarusted_Connection=True";

//        public void Add(T entity)
//        {
//            dbConnection.Set<T>().Add(entity);
//        }
//        public void AddRange(IEnumerable<T> entities)
//        {
//            context.Set<T>().AddRange(entities);
//        }
//        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
//        {
//            return context.Set<T>().Where(expression);
//        }
//        public IEnumerable<T> GetAll()
//        {
//            return context.Set<T>().ToList();
//        }
//        public T GetById(int id)
//        {
//            return context.Set<T>().Find(id);
//        }
//        public void Remove(T entity)
//        {
//            context.Set<T>().Remove(entity);
//        }
//        public void RemoveRange(IEnumerable<T> entities)
//        {
//            context.Set<T>().RemoveRange(entities);
//        }

//        public void Update(T entity)
//        {
//            context.Set<T>();
//        }
//    }
//}
