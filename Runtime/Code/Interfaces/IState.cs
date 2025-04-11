namespace StateForgeX.Code.Interfaces
{
    public interface IState
    {
        void StateEnter();
        void StateUpdate();
        void StateFixedUpdate();
        void StateExit();
    }
}