using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IMove
{
    [Range(0, 1)]
    public float maxVelocity;
    public float timeToMaxVelocitySec;
    public float timeToZeroVelocitySec;
    public MovementState currentState;

    public float MaxVelocity => maxVelocity;
    public float TimeToMaxVelocitySec => timeToMaxVelocitySec;
    public float TimeToZeroVelocitySec => timeToZeroVelocitySec;
    public Transform Transform => gameObject.transform;
    public Rigidbody Rigidbody => m_rigidBody;
    public MovementState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public Vector3 PreviousVelocity
    {
        get { return m_previousVelocity; }
        set { m_previousVelocity = value; }
    }

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
        currentState = movementMachine.currentState;
    }
}

