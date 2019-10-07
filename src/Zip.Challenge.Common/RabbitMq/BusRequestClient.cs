using RawRabbit;
using RawRabbit.Configuration.Request;
using System;
using System.Threading.Tasks;
using Zip.Challenge.Common.Dto;

namespace Zip.Challenge.Common.RabbitMq
{
    public class BusRequestClient : IBusRequestClient
    {
        private readonly IBusClient _busClient;

        public BusRequestClient(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest message = default, Guid globalMessageId = default, 
            Action<IRequestConfigurationBuilder> configuration = null)
        {
            var response = await _busClient.RequestAsync<TRequest, ApiResponse<TResponse>>(message, globalMessageId, configuration);

            if(response.Error != null)
            {
                throw response.Error;
            }

            return response.Data;
        }
    }
}
