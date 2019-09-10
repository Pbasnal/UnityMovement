using UnityEngine;

public interface IMove
{
    float MaxVelocity { get; }
    float TimeToMaxVelocitySec { get; }
    float TimeToZeroVelocitySec { get; }
    Vector3 PreviousVelocity { get; set; }
    Rigidbody Rigidbody { get; }
    Transform Transform { get; }
    MovementState CurrentState { get; set; }
}

