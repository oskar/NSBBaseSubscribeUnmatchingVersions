using System;
using NServiceBus;

var configuration = new EndpointConfiguration("Subscriber");
configuration.EnableInstallers();
configuration.UseSerialization<NewtonsoftJsonSerializer>();
configuration.Conventions().DefiningEventsAs(t => t.IsAssignableTo(typeof(Messages.IEvent)));

var transport = configuration.UseTransport<RabbitMQTransport>();
transport.UseConventionalRoutingTopology(QueueType.Classic);
transport.ConnectionString("host=localhost");

var endpoint = await Endpoint.Start(configuration);

Console.WriteLine("Subscribing to IAnimalChanged. Press any key to exit");
Console.ReadKey();
