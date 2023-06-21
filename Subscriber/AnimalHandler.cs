using System;
using System.Threading.Tasks;
using Messages;
using NServiceBus;

namespace Subscriber;

public class AnimalHandler : IHandleMessages<IAnimalChanged>
{
    public Task Handle(IAnimalChanged message, IMessageHandlerContext context)
    {
        Console.WriteLine(message switch
        {
            IDogChanged => "Received an IDogChanged",
            ICatChanged => "Received an ICatChanged",
            _ => "Unexpectedly received a message instance that was not an IDogChanged or ICatChanged: " + message.GetType()
        });
        return Task.CompletedTask;
    }
}
