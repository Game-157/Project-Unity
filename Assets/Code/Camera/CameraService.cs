using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraService : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lerpSpeed;

    private void LateUpdate()
    {
        var player = GameManager.Instance.CharacterFactory.Hero;

        if (player == null || Camera.main == null)
        {
            return;
        }

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, player.transform.position + offset, lerpSpeed * Time.deltaTime);  

    }
}
