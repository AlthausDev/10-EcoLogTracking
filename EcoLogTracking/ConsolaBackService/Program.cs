using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Net.Http;
namespace ConsolaBackService
{
    internal class Program
    {
        public static async Task Main(string[] args)
            {
            
            await CreateHostBuilder(args).RunConsoleAsync();
            }

            public static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddHttpClient();
                        services.AddHostedService<Service>();
                    });
        }
    }

