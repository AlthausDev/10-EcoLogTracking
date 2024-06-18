using EcoLogTracking.Server.Repository.Interfaces;
using EcoLogTracking.Server.Services.Interfaces;

namespace EcoLogTracking.Server.Services.Impl
{
    public class DateFilterService: IDateFilterService
    {
        private readonly IDateFilterRepository DateFilterRepository;
        private readonly IConfiguration configuration;

        public DateFilterService(IDateFilterRepository dateFilterRepository, IConfiguration configuration)
        {
            this.configuration = configuration;
            DateFilterRepository = dateFilterRepository;
        }
    }
}
