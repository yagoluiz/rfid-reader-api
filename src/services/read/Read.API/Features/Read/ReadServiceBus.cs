using Microsoft.Azure.ServiceBus;
using System.Text;
using System.Threading.Tasks;

namespace Read.API.Features.Read
{
    public class ReadServiceBus : IReadServiceBus
    {
        private readonly ReadContext _readServiceBus;

        public ReadServiceBus(ReadContext readServiceBus)
        {
            _readServiceBus = readServiceBus;
        }

        public async Task SendAsync(string message)
        {
            await _readServiceBus.MessageSender.SendAsync(new Message(Encoding.UTF8.GetBytes(message)));
        }

        public async Task CloseAsync()
        {
            await _readServiceBus.MessageSender.CloseAsync();
        }
    }
}
