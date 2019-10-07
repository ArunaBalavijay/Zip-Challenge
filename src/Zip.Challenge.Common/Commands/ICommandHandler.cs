using System.Threading.Tasks;

namespace Zip.Challenge.Common.Commands
{
    public interface ICommandHandler<in T> where T : ICommand
    {
        Task HandleAsync(T command);
    }
}
