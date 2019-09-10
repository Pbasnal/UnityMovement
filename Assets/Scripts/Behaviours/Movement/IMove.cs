using UnityEngine;

public interface IMove
{
    int MaxVelocity { get; }
    float TimeToMaxVelocitySec { get; }
    float TimeToZeroVelocitySec { get; }
    Vector3 PreviousVelocity { get; }
    Rigidbody Rigidbody { get; }
    Transform Transform { get; }
    MovementState CurrentState { get; set; }
}

