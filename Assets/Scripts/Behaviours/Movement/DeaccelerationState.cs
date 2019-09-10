using UnityEngine;

public class DeaccelerationState : IBehaviourState<IMove, MovementState>
{
    public BehaviourStateMachine<IMove, MovementState> stateMachine { get; set; }
    private float m_accel;
    private Vector3 m_velocity;

    public void OnEnter(IMove gameObject)
    {
        var time = gameObject.TimeToZeroVelocitySec;
        if (time == 0)
        {
            time = 0.5f;
        }
        m_accel = gameObject.MaxVelocity / time;

        m_velocity = gameObject.Rigidbody.velocity;
    }

    public void OnExit(IMove gameObject)
    {
    }

    public void Update(IMove gameObject)
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var tr = gameObject.Transform;
        m_velocity -= (tr.forward * m_accel + tr.right * m_accel) * Time.deltaTime;

        if (h != 0 && v != 0)
        {
            stateMachine.ChangeState(MovementState.Acceleration);
        }
        else if (gameObject.PreviousVelocity.magnitude - m_velocity.magnitude <= 0)
        {
            gameObject.Rigidbody.velocity = Vector3.zero;
            stateMachine.ChangeState(MovementState.NotMoving);
        }
        else
        {
            gameObject.Rigidbody.velocity = m_velocity;
        }
    }
}

