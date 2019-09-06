using System;

public interface IBehaviourState<T, S> where S : struct, IConvertible
{
    BehaviourStateMachine<T, S> stateMachine { get; set; }

    void OnEnter(T gameObject);
    void OnExit(T gameObject);
    void Update(T gameObject);
}
