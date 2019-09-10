using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AccelerationState : IBehaviourState<IMove, MovementState>
{
    public BehaviourStateMachine<IMove, MovementState> stateMachine { get; set; }
    private float m_accel;
    
    public void OnEnter(IMove gameObject)
    {
        var time = gameObject.TimeToMaxVelocitySec;
        if (time == 0)
        {
            time = 0.5f;
        }
        m_accel = gameObject.MaxVelocity / time;
    }

    public void OnExit(IMove gameObject)
    {}

    public void Update(IMove gameObject)
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var tr = gameObject.Transform;
        gameObject.PreviousVelocity += (tr.forward * m_accel * v + tr.right * m_accel * h) * Time.deltaTime;

        if (h == 0 && v == 0)
        {
            stateMachine.ChangeState(MovementState.Deacceleration);
        }
        else if (gameObject.PreviousVelocity.magnitude >= gameObject.MaxVelocity)
        {
            stateMachine.ChangeState(MovementState.MaxSpeed);
        }
        else
        {
            gameObject.Rigidbody.MovePosition(gameObject.Transform.position + gameObject.PreviousVelocity);
        }
    }
}

