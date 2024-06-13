using Dapper;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace EcoLogTracking.Server.Repository.Impl
{
    public class UserRepository: IUserRepository
    {

        private readonly IConfiguration _configuration;
        private string con => _configuration.GetConnectionString("EcoLogTrackingDB");

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

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
                    return connection.QuerySingleOrDefault<User>(query, new { user});
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
