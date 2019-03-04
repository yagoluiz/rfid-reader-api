using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Read.API.Settings;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Read.API.Features.Read
{
    public class ReadTagsBackgroundService : BackgroundService
    {
        private readonly ReadTagsBackgroundSettings _readTagsBackgroundSettings;
        private readonly ReadContext _readContext;
        private readonly IReadRepository _readRepository;
        private readonly ILogger<ReadTagsBackgroundService> _logger;

        public ReadTagsBackgroundService(IOptions<ReadTagsBackgroundSettings> options, ReadContext readContext, IReadRepository readRepository, ILogger<ReadTagsBackgroundService> logger)
        {
            _readTagsBackgroundSettings = options.Value;
            _readContext = readContext;
            _readRepository = readRepository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("ReadTagsBackgroundService is starting.");

            stoppingToken.Register(() => _logger.LogDebug("#1 ReadTagsBackgroundService background task is stopping."));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("ReadTagsBackgroundService background task is doing background work.");

                await CheckReadTagsAsync();

                await Task.Delay(_readTagsBackgroundSettings.UpdateReadTime, stoppingToken);
            }

            _logger.LogDebug("ReadTagsBackgroundService background task is stopping.");

            await Task.CompletedTask;
        }

        private async Task CheckReadTagsAsync()
        {
            _logger.LogDebug("Read tags background confirmed");

            var reads = await _readRepository.GetAllEpc();

            if (reads?.Count() > 0)
            {
                foreach (var read in reads)
                {
                    _logger.LogInformation("Publishing event: EPC => {Epc})", read.Epc);

                    var message = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(read)));

                    await _readContext.QueueClient.SendAsync(message);
                }

                await _readContext.QueueClient.CloseAsync();
            }
        }
    }
}
