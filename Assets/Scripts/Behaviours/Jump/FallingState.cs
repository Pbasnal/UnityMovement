using UnityEngine;

public class FallingState : IBehaviourState<IJump, JumpStates>
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
        if (Input.GetButtonUp("Jump") && jumper.ExtraJumps <= jumper.MaxExtraJumps)
        {
            stateMachine.ChangeState(JumpStates.Jump);
            //var jumpVelocity = Mathf.Sqrt(2 * -Physics.gravity.y * jumper.JumpHeight);

            //jumper.Rigidbody.velocity = Vector3.up * jumpVelocity;
            //jumper.ExtraJumps++;
        }
        else if (jumper.Rigidbody.velocity.y == 0)
        {
            stateMachine.ChangeState(JumpStates.OnGround);
        }
        else
        {
            jumper.Rigidbody.velocity += Vector3.up * Physics2D.gravity.y * jumper.FallGravityMultiplier * Time.deltaTime;
        }
    }
}


