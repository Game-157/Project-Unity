using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZoneController : MonoBehaviour
{
    public Vector3 GetRandomPoint()
    {
        BoxCollider box = GetComponent<BoxCollider>();

        Vector3 center = box.bounds.center;
        Vector3 size = box.bounds.size;

        float randomX = Random.Range(center.x - size.x / 2, center.x + size.x / 2);
        float randomZ = Random.Range(center.z - size.z / 2, center.z + size.z / 2);

        return new Vector3(randomX, center.y, randomZ);
    }
    
}
