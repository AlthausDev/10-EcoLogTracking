using EcoLogTracking.Shared.Models;
using System.Diagnostics;

namespace EcoLogTracking.Server.Models
{
    public class UserCredentials : BaseModel
    {
        public int UserId { get; set; }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set => _userName = value.ToUpper();
        }
        public string EncryptedPassword { get; set; }

        public UserCredentials()
        {
        }

        public UserCredentials(string username, string encryptedPassword)
        {
            UserName = username;
            EncryptedPassword = encryptedPassword;
        }

        public UserCredentials(int userId, string username, string encryptedPassword)
        {
            UserId = userId;
            UserName = username;
            EncryptedPassword = encryptedPassword;
        }

        public override string ToString()
        {
            Debug.WriteLine($"Id: {UserId}" +
               $"\nUsername: {UserName}" +
               $"\nPassword: {EncryptedPassword}");
            return "";
        }
    }
}
