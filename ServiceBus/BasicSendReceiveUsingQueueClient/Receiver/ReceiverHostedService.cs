using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Receiver
{
    public class ReceiverHostedService : IHostedService
    {
        public ReceiverHostedService(ILogger<ReceiverHostedService> logger, IOptions<ReceiverOptions> options)
        {
            this.logger = logger;
            this.options = options.Value;
        }

        private readonly ILogger<ReceiverHostedService> logger;
        private readonly ReceiverOptions options;

        private QueueClient client;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            client = new QueueClient(options.ConnectionString, options.QueueName);
            client.RegisterMessageHandler(ReceiveMessageAsync, new MessageHandlerOptions(ReceiveExceptionAsync)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return client.CloseAsync();
        }

        private async Task ReceiveMessageAsync(Message message, CancellationToken token)
        {
            var content = new Guid(message.Body);

            logger.LogInformation("Received message {@Message} with sequence {@SequenceNumber} and id {@MessageId}",
                content, message.SystemProperties.SequenceNumber, message.PartitionKey);

            await client.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ReceiveExceptionAsync(ExceptionReceivedEventArgs args)
        {
            logger.LogError(args.Exception, args.Exception.ToString());

            return Task.CompletedTask;
        }
    }
}