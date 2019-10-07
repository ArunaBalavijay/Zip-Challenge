using Microsoft.Extensions.Hosting;
using RawRabbit;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Events;
using Zip.Challenge.Common.Queries;
using Zip.Challenge.Common.RabbitMq;

namespace Zip.Challenge.Common.Services
{
    public class BusBuilder : BuilderBase
    {
        private readonly IHost _host;
        private IBusClient _bus;

        public BusBuilder(IHost host, IBusClient bus)
        {
            _host = host;
            _bus = bus;
        }

        public BusBuilder SubscribeToCommand<TCommand>() where TCommand : ICommand
        {
            var handler = (ICommandHandler<TCommand>)_host.Services
                    .GetService(typeof(ICommandHandler<TCommand>));

            _bus.SubscribeToCommand(handler);

            return this;
        }

        public BusBuilder SubscribeToEvent<TEvent>() where TEvent : IEvent
        {
            var handler = (IEventHandler<TEvent>)_host.Services
                    .GetService(typeof(IEventHandler<TEvent>));

            _bus.SubscribeToEvent(handler);

            return this;
        }

        public BusBuilder SubscribeToQuery<TQuery, TResult>() where TQuery : IQuery<TResult>
        {
            var handler = (IQueryHandler<TQuery, TResult>)_host.Services
                    .GetService(typeof(IQueryHandler<TQuery, TResult>));

            _bus.SubscribeToQuery(handler);

            return this;
        }

        public override IServiceHost Build()
        {
            return new ServiceHost(_host);
        }
    }
}
