using BlazorWebPage.Shared;

namespace BlazorWebPage.Server.Services.Interfaces
{
    public interface IUserService : IGenericService<User>
    {
        IEnumerable<User> GetAllAdmin();
    }
}
