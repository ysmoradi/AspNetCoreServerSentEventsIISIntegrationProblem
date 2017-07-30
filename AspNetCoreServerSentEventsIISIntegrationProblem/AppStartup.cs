using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AspNetCoreServerSentEventsIISIntegrationProblem
{
    public class AppStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDirectoryBrowser();
        }

        public void Configure(IApplicationBuilder aspNetCoreApp)
        {
            aspNetCoreApp.UseFileServer(new FileServerOptions
            {
                EnableDirectoryBrowsing = true
            });

            aspNetCoreApp.Map("/sse", sseAspNetCoreApp =>
            {
                sseAspNetCoreApp.Run(async cntx =>
                {
                    cntx.Response.Headers.Add("Content-Type", "text/event-stream");

                    for (var i = 0; true; ++i)
                    {
                        await cntx.Response.WriteAsync($"data: {i} at server side time: {DateTime.Now}\r\r", cntx.RequestAborted);

                        await cntx.Response.Body.FlushAsync(cntx.RequestAborted);

                        await Task.Delay(TimeSpan.FromSeconds(5));
                    }
                });
            });
        }
    }
}
