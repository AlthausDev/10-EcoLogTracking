namespace EcoLogTracking.Server.Models
{
    public class User
    {
        /// <summary>
        /// Identificador de usuario
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de usuario
        /// </summary>
        public string? UserName { get; set; }
        
        /// <summary>
        /// Password del usuario
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Email del usuario
        /// </summary>
        public string? Mail { get; set; }
        
        /// <summary>
        /// Variable que controla el estado del usuario (si ha sido borrado o no (borrado logico))
        /// </summary>
        public int Deleted { get; set; }

        /// <summary>
        /// Fecha en la que se elimino la cuenta del usuario
        /// </summary>
        public DateTime DeletedDate { get; set; }

        public User(int id, string username, string pass, string mail, int deleted ,DateTime deletedDate)
        {
            Id = id;
            UserName = username;
            Password = pass;
            Mail = mail;
            Deleted = deleted;
            DeletedDate = deletedDate;
        }

        public User(string username, string pass, string mail,int deleted, DateTime deletedDate)
        {
            UserName = username;
            Password = pass;
            Mail = mail;
            Deleted = deleted;
            DeletedDate = deletedDate;
        }

        public User() { }

    }
}
