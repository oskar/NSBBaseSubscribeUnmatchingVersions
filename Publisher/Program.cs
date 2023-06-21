using System;
using Messages;
using NServiceBus;

var configuration = new EndpointConfiguration("Publisher");
configuration.EnableInstallers();
configuration.UseSerialization<NewtonsoftJsonSerializer>();
configuration.Conventions().DefiningEventsAs(t => t.IsAssignableTo(typeof(Messages.IEvent)));
configuration.SendOnly();

var transport = configuration.UseTransport<RabbitMQTransport>();
transport.UseConventionalRoutingTopology(QueueType.Classic);
transport.ConnectionString("host=localhost");

var endpoint = await Endpoint.Start(configuration);

Console.WriteLine("Press D to publish IDogChanged and C to publish ICatChanged. Press any other key to exit.");

while (true)
{
    var key = Console.ReadKey();
    Console.WriteLine();

    switch (key.Key)
    {
        case ConsoleKey.D: await endpoint.Publish<IDogChanged>(); break;
        case ConsoleKey.C: await endpoint.Publish<ICatChanged>(); break;
        default: return;
    }
}
