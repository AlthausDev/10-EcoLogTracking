namespace EcoLogTracking.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }

        public User(int id, string username, string pass)
        {
            Id = id;
            UserName = username;
            Password = pass;
        }

        public User(string username, string pass)
        {
            UserName = username;
            Password = pass;
        }

    }
}
