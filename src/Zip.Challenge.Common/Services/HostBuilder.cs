using Microsoft.Extensions.Hosting;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zip.Challenge.Common.Services
{
    public abstract class BuilderBase
    {
        public abstract IServiceHost Build();
    }

    public class HostBuilder : BuilderBase
    {
        private readonly IHost _host;
        private IBusClient _bus;

        public HostBuilder(IHost host)
        {
            _host = host;
        }

        public BusBuilder UseRabbitMq()
        {
            _bus = (IBusClient)_host.Services.GetService(typeof(IBusClient));

            return new BusBuilder(_host, _bus);
        }

        public override IServiceHost Build()
        {
            return new ServiceHost(_host);
        }
    }
}
