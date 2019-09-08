using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardController : MonoBehaviour
{
    public int maxVelocity = 5;
    public float timeToMaxVelocitySec = 3;
    public float timeToZeroVelocitySec = 3;

    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAl = "Vertical";
    public float m_currentVelocity = 0.0f;

    public float m_acceleration = 0.0f;
    public float m_deacceleration = 0.0f;

    private Action<Transform, int>[] m_states;

    public ForwardState m_currentState;

    private Vector3 prevPos;

    Rigidbody rigidBody;
       
    public enum ForwardState
    {
        Standing = 0,
        Acceleration,
        MaxVelocity,
        Deacceleration,
        Turning
    }

    void Awake()
    {
        rigidBody = gameObject.GetComponent<Rigidbody>();
        m_acceleration = maxVelocity / timeToMaxVelocitySec;
        m_deacceleration = maxVelocity / timeToZeroVelocitySec;

        m_currentState = ForwardState.Standing;
        m_states = new Action<Transform, int>[]
        {
            Standing,
            AccelerationState,
            MaxVelocityMovement,
            Deacceleration
        };
    }

    void Start()
    {
        prevPos = gameObject.transform.position;
    }

    void FixedUpdate()
    {
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");

        var tr = gameObject.GetComponent<Transform>();
        rigidBody.velocity = tr.forward * 5 * mV + tr.right * 5 * mH;
        //Move(gameObject.transform);
    }

    public void Move(Transform transform)
    {
        var horizontal = (int)Input.GetAxisRaw(HORIZONTAL);
        //Debug.Log("State: " + m_currentState + " --- CurrentVel: " + m_currentVelocity + "   --- horizontal: " + horizontal + " --- X-travel: " + (transform.position.x - prevPos.x));
        m_states[(int)m_currentState](transform, horizontal);
    }

    public void Standing(Transform transform, int horizontal)
    {
        if (horizontal != 0)
        {
            m_currentState = ForwardState.Acceleration;
        }
    }

    public void MaxVelocityMovement(Transform transform, int horizontal)
    {
        var translateBy = m_currentVelocity * Time.deltaTime;
        transform.Translate(transform.right * translateBy);

        if (horizontal == 0 || horizontal * m_currentVelocity < 0)
        {
            m_currentState = ForwardState.Deacceleration;
        }
    }

    public void Deacceleration(Transform transform, int horizontal)
    {
        if (m_currentVelocity < 0)
        {
            horizontal = 1;
        }
        else
        {
            horizontal = -1;
        }

        var prevVel = m_currentVelocity;
        m_currentVelocity += m_deacceleration * Time.deltaTime * horizontal;
        transform.Translate(transform.right * m_currentVelocity * Time.deltaTime);

        if (m_currentVelocity * m_currentVelocity < 0.01f || prevVel * m_currentVelocity < 0)
        {
            m_currentVelocity = 0.0f;
            m_currentState = ForwardState.Standing;
        }
    }

    public void AccelerationState(Transform transform, int horizontal)
    {
        m_currentVelocity += m_acceleration * Time.deltaTime * horizontal;
        transform.Translate(transform.right * m_currentVelocity * Time.deltaTime);

        if (horizontal == 0)
        {
            m_currentState = ForwardState.Deacceleration;
        }
        else if (m_currentVelocity * horizontal >= maxVelocity )
        {
            m_currentVelocity = maxVelocity * horizontal;
            m_currentState = ForwardState.MaxVelocity;
        }
    }
}
