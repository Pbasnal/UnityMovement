using UnityEngine;

public class OnGroundState : IBehaviourState<IJump, JumpStates>
{
    public BehaviourStateMachine<IJump, JumpStates> stateMachine { get; set; }

    public void OnEnter(IJump jumper)
    {
        
    }

    public void OnExit(IJump jumper)
    {
        
    }

    public void Update(IJump jumper)
    {
        if (Input.GetButtonUp("Jump") && jumper.JumpHeight > 0)
        {
            jumper.ExtraJumps = 0;
            stateMachine.ChangeState(JumpStates.Jump);
        }
    }
}

