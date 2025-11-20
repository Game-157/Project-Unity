using UnityEngine;

public interface IInputProvider
{
    Vector3 GetMoveDirection();
    bool IsAttackPressed();
}