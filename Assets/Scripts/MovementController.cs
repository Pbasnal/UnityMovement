using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour, IJump
{
    private BehaviourStateMachine<IJump, JumpStates> jumpMachine;

    public int maxExtraJumps;
    public float jumpHeight;
    public float jumpPower;
    public float fallGravityMultiplier;
    public float timeToMaxHeight;
    public float initialVelocity;
    public int extraJumps;    
    public new Rigidbody rigidbody;

    public float time;
    public float y;

    public JumpStates currentState;


    public int MaxExtraJumps => maxExtraJumps;
    public float JumpHeight => jumpHeight;
    public float JumpPower => jumpPower;
    public Rigidbody Rigidbody => rigidbody;

    public int ExtraJumps
    {
        get { return extraJumps; }
        set { extraJumps = value; }
    }

    public float FallGravityMultiplier => fallGravityMultiplier;

    public float TimeToMaxHeight => timeToMaxHeight;

    public float InitialVelocity => initialVelocity;

    private void Awake()
    {
        jumpMachine = new BehaviourStateMachine<IJump, JumpStates>(new Dictionary<JumpStates, IBehaviourState<IJump, JumpStates>>
        {
            { JumpStates.OnGround, new OnGroundState() },
            { JumpStates.Jump, new JumpState() },
            { JumpStates.AirTime, new AirTimeState() },
            { JumpStates.Falling, new FallingState() }
        }, this);

        jumpMachine.ChangeState(JumpStates.OnGround);

        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (timeToMaxHeight == 0)
        {
            timeToMaxHeight = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        jumpMachine.Update();
        currentState = jumpMachine.currentState;
    }

    void FixedUpdate()
    {
        if (time < TimeToMaxHeight && jumpMachine.currentState != JumpStates.OnGround)
        {
            y = gameObject.transform.position.y;
            time += Time.deltaTime;
        }
    }
}
