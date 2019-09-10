using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IMove
{
    public int maxVelocity;
    public float timeToMaxVelocitySec;
    public float timeToZeroVelocitySec;
    public MovementState currentState;

    public int MaxVelocity => maxVelocity;
    public float TimeToMaxVelocitySec => timeToMaxVelocitySec;
    public float TimeToZeroVelocitySec => timeToZeroVelocitySec;
    public Transform Transform => gameObject.transform;
    public Rigidbody Rigidbody => m_rigidBody;
    public MovementState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public Vector3 PreviousVelocity => m_previousVelocity;

    private Rigidbody m_rigidBody;
    private Vector3 m_previousVelocity;
    private BehaviourStateMachine<IMove, MovementState> movementMachine;

    void Awake()
    {
        movementMachine = new BehaviourStateMachine<IMove, MovementState>(new Dictionary<MovementState, IBehaviourState<IMove, MovementState>>
        {
            { MovementState.NotMoving, new NotMovingState() },
            { MovementState.Acceleration, new AccelerationState() },
            { MovementState.MaxSpeed, new MaxSpeedState() },
            { MovementState.Deacceleration, new DeaccelerationState() }
        }, this);

        movementMachine.ChangeState(MovementState.NotMoving);

        m_rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        movementMachine.Update();
        m_previousVelocity = m_rigidBody.velocity;
        currentState = movementMachine.currentState;
    }
}

