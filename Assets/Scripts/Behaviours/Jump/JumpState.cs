using UnityEngine;

public class JumpState : IBehaviourState<IJump, JumpStates>
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
        if (jumper.ExtraJumps <= jumper.MaxExtraJumps)
        {
            jumper.Rigidbody.velocity = Vector3.up * jumper.InitialVelocity;// jumpVelocity;
            jumper.ExtraJumps++;
            stateMachine.ChangeState(JumpStates.AirTime);
        }
    }
}

