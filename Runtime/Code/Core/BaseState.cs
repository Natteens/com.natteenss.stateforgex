using StateForgeX.Code.Interfaces;

namespace StateForgeX.Code.Core
{
    public abstract class BaseState : IState
    {
        /// <summary>
        ///  Initializes the state.
        /// </summary>
        public virtual void StateEnter() { }
        
        /// <summary>
        /// Updates the state.
        /// </summary>
        public virtual void StateUpdate() { }

        /// <summary>
        /// Fixed Update the state
        /// </summary>
        public virtual void StateFixedUpdate() { }
        
        /// <summary>
        /// Exits the state.
        /// </summary>
        public virtual void StateExit() { }
    }
}