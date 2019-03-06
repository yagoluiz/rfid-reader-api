using System.Threading.Tasks;

namespace Read.API.Features.Read
{
    public interface IReadServiceBus
    {
        Task SendAsync(string message);
        Task CloseAsync();
    }
}
