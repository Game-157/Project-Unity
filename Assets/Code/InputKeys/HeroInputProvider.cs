using UnityEngine;

public class HeroInputProvider : IInputProvider
{
    public Vector3 GetMoveDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        return new Vector3(horizontal, 0, vertical);
    }

    public bool IsAttackPressed()
    {
        return false;
    }
}