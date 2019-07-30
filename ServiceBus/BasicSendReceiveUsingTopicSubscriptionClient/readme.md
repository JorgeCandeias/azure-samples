## Azure Service Bus Sample: Basic Send & Receive Using Topic Subscription Client

This sample demonstrates how to connect to an Azure Service Bus to send and receive topic messages in a basic way.

This sample uses the generic host pattern to show correct integration into a real-life application.

### How To Use

1. Create or select a Service Bus namespace on your Azure subscription.
1. Create or select a Topic on your Service Bus namespace.
1. Create or select a Subscription on your Service Bus Topic.
1. Run `dotnet user-secrets set ConnectionStrings:ServiceBus "your-service-bus-connection-string" --project Sender` to let the sender sample use your Service Bus namespace.
1. Run `dotnet user-secrets set ConnectionStrings:ServiceBus "your-service-bus-connection-string" --project Receiver` to let the receiver sample use your Service Bus namespace.
1. Run `dotnet user-secrets set TopicName your-topic-name --project Sender` to let the sender sample use your Service Bus Topic.
1. Run `dotnet user-secrets set TopicName your-topic-name --project Receiver` to let the receiver sample use your Service Bus Topic.
1. Run `dotnet user-secrets set SubscriptionName your-subscription-name --project Receiver` to let the receiver sample use your Service Bus Subscription.
1. Run `dotnet run --project Sender` to start the message sender sample.
1. Run `dotnet run --project Receiver` to start the message receiver sample.
