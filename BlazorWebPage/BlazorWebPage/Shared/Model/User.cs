using System.ComponentModel.DataAnnotations;


namespace BlazorWebPage.Shared
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string? Nombre { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;               
        public string FechaRegistro { get; set; }


        public User()
        {
        }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public User(string userName, string password, string? nombre) : this(userName, password)
        {
            Nombre = nombre;
        }

        public User(string userName, string password, string? nombre, string? email) : this(userName, password, nombre)
        {
            Email = email;
        }

        public User(int id, string userName, string password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }

        public User(int id, string userName, string password, string? nombre, string? email)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Nombre = nombre;
            Email = email;
        }
  

        public User(int id, string userName, string password, string? nombre, string? email, string FechaRegistro) : this(id, userName, password, nombre, email)
        {
            this.FechaRegistro = FechaRegistro;
        }

        public User(string userName, string password, string? nombre, string? email, string FechaRegistro) : this(userName, password, nombre, email)
        {
            this.FechaRegistro = FechaRegistro;
        }

        public override string ToString()
        {
            Console.WriteLine($"Id: {Id}" +
                $"\nUsername: {UserName}" +
                $"\nContraseña: {Password}" +
                $"\nNombre: {Nombre}" +
                $"\nEmail: {Email}" +
                $"\nFecha de Registro: {FechaRegistro}");

            return "";

        }
    }
}