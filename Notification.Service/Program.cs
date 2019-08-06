using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Notification.Service
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                                .SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                .Build();

            await WebHost.CreateDefaultBuilder(args)
                                .UseConfiguration(configuration)
                                .UseContentRoot(Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName))
                                .UseStartup<Startup>()
                                .UseUrls(configuration.GetSection("HostingUrls").Value.Split('|'))
                                .Build().RunAsync();
        }
    }
}