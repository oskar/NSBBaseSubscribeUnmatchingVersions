namespace Messages
{
    public interface IEvent { }
    public interface IAnimalChanged : IEvent { }
    public interface IDogChanged : IAnimalChanged { }
    public interface ICatChanged : IAnimalChanged { }
}
