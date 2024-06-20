namespace EcoLogTracking.Server.Models
{
    /// <summary>
    /// Clase que porta informacion de un usuario y del token que adquiere al loguearse
    /// para poder acceder a la aplicacion
    /// </summary>
    public class LoginResponse
    {    
        /// <summary>
        /// Objeto usuario completo
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Token para la autenticacion de usuario
        /// </summary>
        public string Token { get; set; }

        public LoginResponse()
        {
        }

        public LoginResponse(User user, string token)
        {
            User = user;
            Token = token;
        }
    }

}
