using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Services.Interfaces;
using System.Diagnostics;
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


        /// <summary>
        /// MÉTODO QUE OBTIENE DE LA BASE DE DATOS LOS DATOS DE UN USUARIO A PARTIR DE SU USERNAME AND PASSWORD
        /// </summary>
        /// <param name="user">Nombre de usuario introducido por teclado</param>
        /// <param name="pass">Contraseña introducida por teclado</param>
        /// <returns>Devuelve objeto usuario con nombre, contraseña e id del usuario (si existe)</returns>
        public async Task<User> GetUserByUsernameAndPass(string user, string pass)
        {
            return await userRepository.GetUserByUsernameAndPass(user, pass);
        }



        /// <summary>
        /// MÉTODO QUE OBTIENE LOS DATOS DE UN USUARIO EN FUNCIÓN DEL NOMBRE DE USUARIO
        /// </summary>
        /// <param name="user">Nombre de usuario introducido por teclado</param>
        /// <returns>Objeto usuario con los datos del usuario cuyo UserName coincide con el introducido</returns>
        public async Task<User> GetUserByUsername(string user)
        {
            var userData = await userRepository.GetUserByUsername(user);
            if (userData == null)
            {
                return null;
            }
            User userDecrypt = new()
            {
                Id = userData.Id,
                UserName = userData.UserName,
                Password = encryptionHelper.Decrypt(userData.Password)
            };
            return userDecrypt;

        }

        /// <summary>
        /// MÉTODO PARA REGISTRAR UN NUEVO USUARIO
        /// </summary>
        /// <param name="user">Objeto usuario con los datos del nuevo usuario que se quiere registrar</param>
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: no afecta a ninguna tupla)</returns>
        public async Task<bool> PostUser(User user)
        {
            try
            {
                var userData = await userRepository.GetUserByUsername(user.UserName);
                if (userData == null)
                {
                    User userEncrypt = new()
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        Password = encryptionHelper.Encrypt(user.Password)
                    };
                    return await userRepository.PostUser(userEncrypt);
                }

                return false;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            return false;
        }


        /// <summary>
        /// MÉTODO QUE ELIMINA LA CUENTA DEL PROPIO USUARIO A PARTIR DE SU ID
        /// </summary>
        /// <param name="id">Id del usuario que está borrando su cuenta</param>
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: no afecta a ninguna tupla)</returns>
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                bool response = await userRepository.DeleteUser(id);
                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }




        /// <summary>
        /// MÉTODO QUE ACTUALIZA LOS DATOS DEL PROPIO USUARIO
        /// </summary>
        /// <param name="user">Objeto usuario con mismo id( para poder filtrar la tabla) pero con los nuevos datos del usuario</param>
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: no afecta a ninguna tupla)</returns>
        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                //User userToUpdate = new()
                //{
                //    Id = user.Id,
                //    UserName = user.UserName,
                //    Password = encryptionHelper.Encrypt(user.Password)
                //};
                bool response = await userRepository.UpdateUser(user);
                return response;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
