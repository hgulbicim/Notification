using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
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
            var exePath = Process.GetCurrentProcess().MainModule.FileName;
            var rootFolder = Path.GetDirectoryName(exePath);

            await WebHost.CreateDefaultBuilder(args)
                                .UseContentRoot(rootFolder)
                                .UseStartup<Startup>()
                                .UseUrls("http://localhost:5000")
                                .Build().RunAsync();
        }
    }
}