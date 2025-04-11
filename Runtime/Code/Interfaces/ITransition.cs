namespace StateForgeX.Code.Interfaces
{
    public interface ITransition
    {
        IState To { get; }
        IPredicate Condition { get; }
    }
}