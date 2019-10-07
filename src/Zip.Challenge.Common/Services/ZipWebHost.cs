using Microsoft.Extensions.Hosting;

namespace Zip.Challenge.Common.Services
{
    public static class ZipWebHost
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args);
        }
    }
}
