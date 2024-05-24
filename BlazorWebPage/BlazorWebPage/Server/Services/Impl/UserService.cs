using BlazorWebPage.Server.Repository.Interfaces;
using BlazorWebPage.Server.Services.Interfaces;
using BlazorWebPage.Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Data.Common;
using BlazorWebPage.Server.Utils;

namespace BlazorWebPage.Server.Services.Impl
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;
        private readonly EncryptionUtil encryptionUtil;
   
        public UserService(IUserRepository userRepository, EncryptionUtil encryptionUtil)
        {
            this.userRepository = userRepository;
            this.encryptionUtil = encryptionUtil;
        }

        public IEnumerable<User> GetAllAdmin()
        {
            return userRepository.GetAllAdmin();
        }

        public IEnumerable<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User GetById(int id)
        {
            User user = userRepository.GetById(id);           
            user.Password = encryptionUtil.Decrypt(user.Password);         

            return user;
        }

        public void Add(User user)
        {
            user.Password = encryptionUtil.Encrypt(user.Password);
            userRepository.Add(user);
        }

        public void Update(User user)
        {
            user.Password = encryptionUtil.Encrypt(user.Password);
            userRepository.Update(user);
        }

        public void Remove(int id)
        {
            userRepository.Remove(id);
        }

        public void LogicRemove(int id)
        {
            userRepository.LogicRemove(id);
        }
    }
}
