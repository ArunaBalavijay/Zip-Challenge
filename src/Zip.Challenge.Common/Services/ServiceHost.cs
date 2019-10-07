using Microsoft.Extensions.Hosting;

namespace Zip.Challenge.Common.Services
{
    public class ServiceHost : IServiceHost
    {
        private readonly IHost _host;

        public ServiceHost(IHost host)
        {
            _host = host;
        }

        public void Run() => _host.Run();
    }
}
