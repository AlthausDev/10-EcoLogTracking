using EcoLogTracking.Server.Models;

namespace EcoLogTracking.Server.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// MÉTODO QUE OBTIENE DE LA BASE DE DATOS LOS DATOS DE UN USUARIO A PARTIR DE SU USERNAME AND PASSWORD
        /// </summary>
        /// <param name="user">Nombre de usuario introducido por teclado</param>
        /// <param name="pass">Contraseña introducida por teclado</param>
        /// <returns>Devuelve objeto usuario con nombre, contraseña e id del usuario (si existe)</returns>
        public Task<User> GetUserByUsernameAndPass(string user, string pass);


        /// <summary>
        /// MÉTODO QUE OBTIENE LOS DATOS DE UN USUARIO EN FUNCIÓN DEL NOMBRE DE USUARIO
        /// </summary>
        /// <param name="user">Nombre de usuario introducido por teclado</param>
        /// <returns>Objeto usuario con los datos del usuario cuyo UserName coincide con el introducido</returns>
        public Task<User> GetUserByUsername(string user);


        /// <summary>
        /// MÉTODO PARA REGISTRAR UN NUEVO USUARIO
        /// </summary>
        /// <param name="user">Objeto usuario con los datos del nuevo usuario que se quiere registrar</param>
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: no afecta a ninguna tupla)</returns>
        public Task<bool> PostUser(User user);


        /// <summary>
        /// MÉTODO QUE ELIMINA LA CUENTA DEL PROPIO USUARIO A PARTIR DE SU ID
        /// </summary>
        /// <param name="id">Id del usuario que está borrando su cuenta</param>
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: no afecta a ninguna tupla)</returns>
        public Task<bool> DeleteUser(int id);



        /// <summary>
        /// MÉTODO QUE ACTUALIZA LOS DATOS DEL PROPIO USUARIO
        /// </summary>
        /// <param name="user">Objeto usuario con mismo id( para poder filtrar la tabla) pero con los nuevos datos del usuario</param>
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: no afecta a ninguna tupla)</returns>
        public Task<bool> UpdateUser(User user);
    }

}
