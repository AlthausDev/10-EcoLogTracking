namespace EcoLogTracking.Server.Repository.Interfaces
{
    public interface IConfigurationRepository
    {

        public Task<bool> updateConfig(int time);
    }
}
