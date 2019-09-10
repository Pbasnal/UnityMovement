using UnityEngine;

public class DeaccelerationState : IBehaviourState<IMove, MovementState>
{
    public BehaviourStateMachine<IMove, MovementState> stateMachine { get; set; }
    private float m_accel;
    private Vector3 velocity;
    private Vector3 unitVelocity;

    public void OnEnter(IMove gameObject)
    {
        var time = gameObject.TimeToZeroVelocitySec;
        if (time == 0)
        {
            time = 0.5f;
        }
        m_accel = gameObject.MaxVelocity / time;
        velocity = gameObject.PreviousVelocity;

        unitVelocity = velocity / velocity.magnitude;
    }

    public void OnExit(IMove gameObject)
    {
    }

    public void Update(IMove gameObject)
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        velocity -= unitVelocity * m_accel * Time.deltaTime;

        if (h != 0 || v != 0)
        {
            stateMachine.ChangeState(MovementState.Acceleration);
        }
        else if (gameObject.PreviousVelocity.magnitude - velocity.magnitude <= 0)
        {
            gameObject.PreviousVelocity = Vector3.zero;
            stateMachine.ChangeState(MovementState.NotMoving);
        }
        else
        {
            gameObject.Rigidbody.MovePosition(gameObject.Transform.position + velocity);
            gameObject.PreviousVelocity = velocity;
        }        
    }
}

