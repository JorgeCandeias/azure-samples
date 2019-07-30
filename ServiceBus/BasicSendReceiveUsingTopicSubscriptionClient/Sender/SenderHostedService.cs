using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sender
{
    public class SenderHostedService : IHostedService
    {
        public SenderHostedService(ILogger<SenderHostedService> logger, IOptions<SenderOptions> options)
        {
            this.logger = logger;
            this.options = options.Value;
        }

        private readonly ILogger<SenderHostedService> logger;
        private readonly SenderOptions options;

        private ITopicClient topic;
        private Timer timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            topic = new TopicClient(options.ConnectionString, options.TopicName);
            timer = new Timer(async _ => await SendMessageAsync(), null, options.SendDelay, options.SendPeriod);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();

            return topic.CloseAsync();
        }

        private async Task SendMessageAsync()
        {
            var body = Guid.NewGuid();
            var message = new Message(body.ToByteArray());
            await topic.SendAsync(message);

            logger.LogInformation("Sent message {@Message}", body);
        }
    }
}