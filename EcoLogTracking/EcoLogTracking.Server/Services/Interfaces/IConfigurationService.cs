namespace EcoLogTracking.Server.Services.Interfaces
{
    public interface IConfigurationService
    {
        public Task<bool> updateConfig(int time);
    }
}
