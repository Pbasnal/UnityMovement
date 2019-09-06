using System;
using System.Collections.Generic;

public class BehaviourStateMachine<T, S> where S : struct, IConvertible
{
    public IDictionary<S, IBehaviourState<T, S>> _behaviourStates;
    public S currentState;
    private T _agent;

    public BehaviourStateMachine(IDictionary<S, IBehaviourState<T, S>> states, T agent)
    {
        _behaviourStates = states;

        foreach (var state in _behaviourStates.Values)
        {
            state.stateMachine = this;
        }

        _agent = agent;
    }

    public void ChangeState(S state)
    {
        if (!state.GetType().IsEnum)
        {
            throw new ArgumentException("Argument must be an Enum");
        }

        _behaviourStates[currentState].OnExit(_agent);
        currentState = state;
        _behaviourStates[currentState].OnEnter(_agent);
    }

    public void Update()
    {
        _behaviourStates[currentState].Update(_agent);
    }
}

