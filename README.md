# NServiceBus base subscribe with unmatching versions

This app reproduces a situation where subscribing to a base message type when the assembly versions dont match results in an unexpected message instance of the base type instead of what was published.

The message hierarchy is simple:

```csharp
interface IAnimalChanged : IEvent { }
interface IDogChanged : IAnimalChanged { }
interface ICatChanged : IAnimalChanged { }
```

The subscriber subscribes to `IAnimalChanged` from the `Messages` assembly with version 1.0.0.0.

The publisher publishes `ICatChanged` and `IDogChanged` from the `Messages` assembly with version 2.0.0.0.

Expect for the assembly version, the two messages assemblies are identical.

Steps to reproduce:

1. Start RabbitMQ with `docker compose up -d`
2. Start subscriber with `dotnet run --project Subscriber`
3. Start publisher with `dotnet run --project Publisher`
4. Publish a message (with key `C` or `D`)
5. Received message in subscriber is an instance of `IAnimalChanged__impl` instead of the expected `IDogChanged__impl` or `ICatChanged__impl`. This behaviour has changed with NSB 8. In NSB 7 this worked as expected.

If the `Version` attribute in `MessagesV2.csproj` is removed, the app starts to behave as expected. The same is true if NServiceBus is downgraded from 8 to 7.
