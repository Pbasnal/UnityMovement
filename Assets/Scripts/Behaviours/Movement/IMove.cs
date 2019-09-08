using UnityEngine;

public interface IMove
{
    int MaxVelocity { get; }
    float TimeToMaxVelocitySec { get; }
    float TimeToZeroVelocitySec { get; }
    float CurrentVelocity { get; set; }
    Rigidbody Rigidbody { get; }
    ForwardState currentState { get; set; }
}

