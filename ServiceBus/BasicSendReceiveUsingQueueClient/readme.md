## Azure Service Bus Sample: Basic Send & Receive Using Queue Client

This sample demonstrates how to connect to an Azure Service Bus to send and receive messages in a basic way.

This sample uses the generic host pattern to show correct integration into a real-life application.

### How To Use

1. Create or select a Service Bus namespace on your Azure subscription.
1. Create or select a Queue on your Service Bus namespace.
1. Run `dotnet user-secrets set ConnectionStrings:ServiceBus "your-service-bus-connection-string" --project Sender` to let the sender sample use your Service Bus namespace.
1. Run `dotnet user-secrets set ConnectionStrings:ServiceBus "your-service-bus-connection-string" --project Receiver` to let the receiver sample use your Service Bus namespace.
1. Run `dotnet user-secrets set ServiceBus:Queue:Name your-queue-name --project Sender` to let the sender sample use your Service Bus Queue.
1. Run `dotnet user-secrets set ServiceBus:Queue:Name your-queue-name --project Receiver` to let the receiver sample use your Service Bus Queue.
1. Run `dotnet run --project Sender` to start the queue message sender sample.
1. Run `dotnet run --project Receiver` to start the queue message receiver sample.
