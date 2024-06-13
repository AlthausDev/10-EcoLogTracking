using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Services.Interfaces;
using todoAPI.Helpers;

namespace EcoLogTracking.Server.Services.Impl
{
    public class UserService : IUserService
    {

        public readonly IUserRepository userRepository;
        private readonly EncryptionHelper encryptionHelper;

        public UserService(IUserRepository userRepository, EncryptionHelper encryptionHelper)
        {
            this.userRepository = userRepository;
            this.encryptionHelper = encryptionHelper;
        }

        public User GetUserByUsernameAndPass(string user, string pass)
        {
            return userRepository.GetUserByUsernameAndPass(user,pass); 
        }



        public User GetUserByUsername(string user)
        {
            var userData = userRepository.GetUserByUsername(user);
            if (userData == null)
            {
                return null;
            }
            User userDecrypt = new User { 
                Id = userData.Id,
                UserName = userData.UserName,
                //Password = encryptionHelper.Decrypt(userData.Password)
                Password = userData.Password
            };
            return userDecrypt;

        }
    }
}
