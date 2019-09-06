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
        Debug.Log("Jump");
        if (jumper.ExtraJumps <= jumper.MaxExtraJumps)
        {
            Debug.Log("Gravity: " + Physics.gravity.y);
            //var jumpVelocity = Mathf.Sqrt(2 * -Physics.gravity.y * jumper.JumpHeight);

            jumper.Rigidbody.velocity = Vector3.up * jumper.InitialVelocity;// jumpVelocity;
            jumper.ExtraJumps++;
            stateMachine.ChangeState(JumpStates.AirTime);
        }
    }
}

