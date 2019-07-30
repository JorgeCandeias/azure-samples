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

        private ISubscriptionClient subscription;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscription = new SubscriptionClient(options.ConnectionString, options.TopicName, options.SubscriptionName);
            subscription.RegisterMessageHandler(ReceiveMessageAsync, new MessageHandlerOptions(ReceiveExceptionAsync)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false,
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return subscription.CloseAsync();
        }

        private async Task ReceiveMessageAsync(Message message, CancellationToken token)
        {
            var content = new Guid(message.Body);

            logger.LogInformation("Received message {@Message} with sequence {@SequenceNumber}",
                content, message.SystemProperties.SequenceNumber);

            await subscription.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ReceiveExceptionAsync(ExceptionReceivedEventArgs args)
        {
            logger.LogError(args.Exception, args.Exception.ToString());

            return Task.CompletedTask;
        }
    }
}