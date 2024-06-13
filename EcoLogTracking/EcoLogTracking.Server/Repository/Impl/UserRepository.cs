using Dapper;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace EcoLogTracking.Server.Repository.Impl
{
    public class UserRepository: IUserRepository
    {
        string con = "Server=ECO3018;DATABASE=DBEcoLogTracking;User Id=sa; Password=sa;TrustServerCertificate=True";
        public User GetUserByUsernameAndPass(string user, string pass)
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string query = @"SELECT IdUser, UserName, Password FROM Users
                                 WHERE UserName = @user AND Password = @pass";
                    return connection.QuerySingle<User>(query, new { user, pass });
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public User GetUserByUsername(string user)
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string query = @"SELECT Id, UserName, Password FROM Users
                                 WHERE UserName = @user";
                    return connection.QuerySingle<User>(query, new { user});
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public bool PostUser(User user)
        {
            try
            {
                using (var connection = new SqlConnection(con))
                {
                    string query = @"INSERT INTO Users(UserName, Password) Values(@name,@pass)";
                    return connection.Execute(query, new { name = user.UserName, pass = user.Password}) > 0;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
