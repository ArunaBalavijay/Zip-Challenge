using System.Threading.Tasks;

namespace Zip.Challenge.Common.Events
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}
