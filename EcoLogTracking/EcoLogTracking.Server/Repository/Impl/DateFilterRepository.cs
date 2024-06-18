using EcoLogTracking.Server.Repository.Interfaces;
using NLog;

namespace EcoLogTracking.Server.Repository.Impl
{
    public class DateFilterRepository : IDateFilterRepository
    {
        private readonly IConfiguration _configuration;
        private string? _connectionString => _configuration.GetConnectionString("EcoLogTrackingDB");

        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public DateFilterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }




    }

}
