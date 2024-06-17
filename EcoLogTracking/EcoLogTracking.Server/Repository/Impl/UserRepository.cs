using Dapper;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace EcoLogTracking.Server.Repository.Impl
{
    public class UserRepository : IUserRepository
    {

        private readonly IConfiguration _configuration;
        private string con => _configuration.GetConnectionString("EcoLogTrackingDB");

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// MÉTODO QUE OBTIENE DE LA BASE DE DATOS LOS DATOS DE UN USUARIO A PARTIR DE SU USERNAME AND PASSWORD
        /// </summary>
        /// <param name="user">Nombre de usuario introducido por teclado</param>
        /// <param name="pass">Contraseña introducida por teclado</param>
        /// <returns>Devuelve objeto usuario con nombre, contraseña e id del usuario (si existe)</returns>
        public async Task<User> GetUserByUsernameAndPass(string user, string pass)
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string query = @"SELECT Id, UserName, Password FROM Users
                                 WHERE UserName = @user AND Password = @pass AND Deleted = 0";
                    return connection.QuerySingle<User>(query, new { user, pass });
                }
            }
            catch (Exception)
            {
                return null;
            }
        }



        public async Task<User?> GetUserById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string query = @"SELECT * FROM Users
                                 WHERE Id = @Id AND Deleted = 0";
                    return connection.QuerySingle<User>(query, new { Id = id });
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// MÉTODO QUE OBTIENE LOS DATOS DE UN USUARIO EN FUNCIÓN DEL NOMBRE DE USUARIO
        /// </summary>
        /// <param name="user">Nombre de usuario introducido por teclado</param>
        /// <returns>Objeto usuario con los datos del usuario cuyo UserName coincide con el introducido</returns>
        public async Task<User>? GetUserByUsername(string userName)
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string query = @"SELECT Id, UserName, Password, Mail FROM Users
                             WHERE UserName = @UserName AND Deleted = 0";
                    return await connection.QuerySingleOrDefaultAsync<User>(query, new { UserName = userName });
                }
            }
            catch (Exception e)
            {
                // Aquí sería útil registrar la excepción para futuras investigaciones
                Console.WriteLine($"Error al obtener el usuario por nombre de usuario: {e.Message}");
                return null;
            }
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
                using (var connection = new SqlConnection(con))
                {
                    string query = @"INSERT INTO Users(UserName, Password, Mail) Values(@name,@pass,@mail)";
                    return connection.Execute(query, new { name = user.UserName, pass = user.Password, mail = user.Mail }) > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
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
                using (var connection = new SqlConnection(con))
                {
                    string query = "UPDATE Users SET Deleted = 1,DeletedDate = GETDATE() WHERE Id = @id";
                    return connection.Execute(query, new { id }) > 0;
                }
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
                using (var connection = new SqlConnection(con))
                {
                    string query = "UPDATE Users SET UserName = @username, Mail = @mail WHERE Id = @id";
                    return connection.Execute(query, new { username = user.UserName, pass = user.Password, id = user.Id, mail = user.Mail }) > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        
    }
}
