using UnityEngine;

public class MaxSpeedState : IBehaviourState<IMove, MovementState>
{
    public BehaviourStateMachine<IMove, MovementState> stateMachine { get; set; }

    public void OnEnter(IMove gameObject)
    {
    }

    public void OnExit(IMove gameObject)
    {
    }

    public void Update(IMove gameObject)
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        if (h == 0 && v == 0)
        {
            stateMachine.ChangeState(MovementState.Deacceleration);
        }
        else
        {
            gameObject.PreviousVelocity = (Vector3.forward * v + Vector3.right * h) * gameObject.MaxVelocity;
            gameObject.Rigidbody.MovePosition(gameObject.Transform.position + gameObject.PreviousVelocity);
        }
    }
}