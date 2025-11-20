using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private float defaultSpeed;

    public CharacterController CharacterController => characterController;
    public Transform CharacterTransform => characterTransform;
    public float DefaultSpeed => defaultSpeed;
    
}
