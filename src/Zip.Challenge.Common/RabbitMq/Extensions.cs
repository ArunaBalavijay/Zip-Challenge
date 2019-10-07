using RawRabbit;
using RawRabbit.Common;
using System;
using System.Reflection;
using Zip.Challenge.Common.Commands;
using Zip.Challenge.Common.Dto;
using Zip.Challenge.Common.Events;
using Zip.Challenge.Common.Queries;

namespace Zip.Challenge.Common.RabbitMq
{
    public static class Extensions
    {
        public static ISubscription SubscribeToCommand<TCommand>(
            this IBusClient bus,
            ICommandHandler<TCommand> handler, 
            string name = null
        ) where TCommand : ICommand
            => bus.SubscribeAsync<TCommand>(
                async (msg, context) => await handler.HandleAsync(msg),
                cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TCommand>(name)))
            );

        public static ISubscription SubscribeToEvent<TEvent>(
            this IBusClient bus,
            IEventHandler<TEvent> handler,
            string name = null
        ) where TEvent : IEvent
            => bus.SubscribeAsync<TEvent>(
                async (msg, context) => await handler.HandleAsync(msg),
                cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TEvent>(name)))
            );

        public static ISubscription SubscribeToQuery<TQuery, TResult>(
            this IBusClient bus,
            IQueryHandler<TQuery, TResult> handler,
            string name = null
        ) where TQuery : IQuery<TResult>
        {
            return bus.RespondAsync<TQuery, ApiResponse<TResult>>(
                async (msg, context) =>
                {
                    try
                    {
                        var data = await handler.HandleAsync(msg);
                        return new ApiResponse<TResult>(data);
                    }
                    catch (Exception ex)
                    {
                        return new ApiResponse<TResult>(default) { Error = ex };
                    }
                },
                cfg => cfg.WithQueue(q => q.WithName(GetExchangeName<TQuery>(name)))
            );
        }

        private static string GetExchangeName<T>(string name = null)
            => string.IsNullOrWhiteSpace(name)
                ? $"{Assembly.GetEntryAssembly().GetName()}/{typeof(T).Name}"
                : $"{name}/{typeof(T).Name}";
    }
}
