using BlazorWebPage.Shared;

namespace BlazorWebPage.Server.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetAllAdmin();
    }
}
