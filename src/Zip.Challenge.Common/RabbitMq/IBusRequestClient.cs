using RawRabbit.Configuration.Request;
using System;
using System.Threading.Tasks;

namespace Zip.Challenge.Common.RabbitMq
{
    public interface IBusRequestClient
    {
        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest message = default, Guid globalMessageId = default,
            Action<IRequestConfigurationBuilder> configuration = null);
    }
}
