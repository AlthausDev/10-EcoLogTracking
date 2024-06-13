using EcoLogTracking.Client.Models;

namespace EcoLogTracking.Client.Services
{
    public class MockLogService
    {
        public List<Log> GetMockLogs()
        {
            var mockLogs = new List<Log>();

            // Generar 20 registros de prueba
            for (int i = 1; i <= 200; i++)
            {
                mockLogs.Add(new Log
                {
                    Id = i,
                    MachineName = $"Machine {i}",
                    Logged = DateTime.Now.AddDays(-i),
                    Level = "Info",
                    Message = $"This is a really loooooooong log message {i} This is a really loooooooong log message This is a really loooooooong log message",
                    Logger = "MockLogger",
                    Request_method = "GET"
                });
            }

            return mockLogs;
        }
    }
}
