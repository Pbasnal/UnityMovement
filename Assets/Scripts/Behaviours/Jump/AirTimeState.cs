
using UnityEngine;

public class AirTimeState : IBehaviourState<IJump, JumpStates>
{
    public BehaviourStateMachine<IJump, JumpStates> stateMachine { get; set; }
    private float initialVelocity;
    private float gravity;

    public void OnEnter(IJump jumper)
    {
        initialVelocity = jumper.Rigidbody.velocity.y;
        gravity = initialVelocity * initialVelocity / (2 * jumper.JumpHeight);
    }

    public void OnExit(IJump jumper)
    {

    }

    public void Update(IJump jumper)
    {
        if (Input.GetButtonUp("Jump") && jumper.ExtraJumps <= jumper.MaxExtraJumps)
        {
            stateMachine.ChangeState(JumpStates.Jump);
        }
        else if (jumper.Rigidbody.velocity.y < 0)
        {
            stateMachine.ChangeState(JumpStates.Falling);
        }
        else
        {
            jumper.Rigidbody.velocity -= Vector3.up * gravity * Time.deltaTime;
        }
    }
}


