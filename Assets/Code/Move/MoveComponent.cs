using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour, IMoveComponent
{
    private Character selfCharacter;
    private CharacterData characterData;

    private float speed;
    private float turnSmoothVelocity;

    public float Speed
    {
        get => speed;
        set => speed = Mathf.Max(0, value);
    }

    public Vector3 Position => selfCharacter.transform.position;

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        this.characterData = selfCharacter.CharacterData;

        speed = characterData.DefaultSpeed;
    }

    public void Move(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        Vector3 move = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

        selfCharacter.GetComponent<CharacterController>()
            .Move(move * speed * Time.deltaTime);
    }

    public void Rotation(Vector3 direction)
    {
        if (direction == Vector3.zero) return;

        float rotationSpeed = 180f; // ← регулируй (градусы/сек)
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        

        selfCharacter.transform.rotation = Quaternion.RotateTowards(
        characterData.CharacterTransform.rotation,
        targetRotation,
        rotationSpeed * Time.deltaTime
    );
    }
}
