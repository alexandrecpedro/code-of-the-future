using Azure.Messaging.ServiceBus;

namespace QueueSender;

class Program
{
    static string connectionString = Environment.GetEnvironmentVariable("AZURE_PRIMARY_CONNECTION_STRING")!;
    static string queueName = "minha-fila";

    // name of your Service Bus queue
    // the client that owns the connection and can be used to create senders and receivers
    static ServiceBusClient client = default!;

    // the sender used to publish messages to the queue
    static ServiceBusSender sender = default!;

    // number of messages to be sent to the queue
    private const int numOfMessages = 3;
    
    static async Task Main(string[] args)
    {
        // The Service Bus client types are safe to cache and use as a singleton for the lifetime
        // of the application, which is best practice when messages are being published or read
        // regularly.
        //
        // Set the transport type to AmqpWebSockets so that the ServiceBusClient uses the port 443. 
        // If you use the default AmqpTcp, ensure that ports 5671 and 5672 are open.
        /* var clientOptions = new ServiceBusClientOptions
        { 
            TransportType = ServiceBusTransportType.AmqpWebSockets
        };
        client = new ServiceBusClient(
        "<NAMESPACE-NAME>.servicebus.windows.net",
        new DefaultAzureCredential(),
        clientOptions); */
        client = new ServiceBusClient(connectionString);
        sender = client.CreateSender(queueName);

        // create a batch
        using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

        for (int i = 1; i <= numOfMessages; i++)
        {
            // try adding a message to the batch
            if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
            {
                // if it is too large for the batch
                throw new Exception($"The message {i} is too large to fit in the batch.");
            }
        }

        try
        {
            // Use the producer client to send the batch of messages to the Service Bus queue
            await sender.SendMessagesAsync(messageBatch);
            Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
        }
        finally
        {
            // Calling DisposeAsync on client types is required to ensure that network
            // resources and other unmanaged objects are properly cleaned up.
            await sender.DisposeAsync();
            await client.DisposeAsync();
        }

        Console.WriteLine("Press any key to end the application");
        Console.ReadKey();
    }
}