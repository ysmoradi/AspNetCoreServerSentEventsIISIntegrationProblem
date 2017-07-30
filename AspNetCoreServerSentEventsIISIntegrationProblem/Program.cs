using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AspNetCoreServerSentEventsIISIntegrationProblem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHost host = new WebHostBuilder()
                .UseKestrel()
                .UseIISIntegration()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                .UseStartup<AppStartup>()
                .Build();

            host.Run();
        }
    }
}
