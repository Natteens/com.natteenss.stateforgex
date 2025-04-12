using UnityEngine;

namespace StateForgeX.Code.Core
{
    public class StateForgeXComponent : MonoBehaviour
    {
        private StateMachine stateMachine;
        
        private void Awake()
        {
            stateMachine = new StateMachine();
        }
        
        private void Update()
        {
            stateMachine.Update();
        }
        
        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
    }
}