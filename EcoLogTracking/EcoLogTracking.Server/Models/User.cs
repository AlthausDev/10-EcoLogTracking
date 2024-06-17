namespace EcoLogTracking.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Mail { get; set; }
        
        public int Deleted { get; set; }
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
