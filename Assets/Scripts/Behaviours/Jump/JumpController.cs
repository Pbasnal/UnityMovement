using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour, IJump
{
    private BehaviourStateMachine<IJump, JumpStates> jumpMachine;

    public int maxExtraJumps;
    public float jumpHeight;
    //public float jumpPower;
    public float fallGravityMultiplier;
    //public float timeToMaxHeight;
    public float initialVelocity;
    public int extraJumps;    
    public new Rigidbody rigidbody;

    public JumpStates currentState;

    public int MaxExtraJumps => maxExtraJumps;
    public float JumpHeight => jumpHeight;
//    public float JumpPower => jumpPower;
    public Rigidbody Rigidbody => rigidbody;

    public int ExtraJumps
    {
        get { return extraJumps; }
        set { extraJumps = value; }
    }

    public float FallGravityMultiplier => fallGravityMultiplier;

  //  public float TimeToMaxHeight => timeToMaxHeight;

    public float InitialVelocity => initialVelocity;

    void Awake()
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        jumpMachine.Update();
        currentState = jumpMachine.currentState;
    }
}
