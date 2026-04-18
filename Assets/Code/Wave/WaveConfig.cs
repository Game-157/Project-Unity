using UnityEngine;

[CreateAssetMenu(menuName = "Game/Wave")]
public class WaveConfig : ScriptableObject
{
    public int enemyCount;
    public float spawnDelay;

    public CharacterType enemyType;

    [Header("Spawn Settings")]
    public float minOffset = 3f;
    public float maxOffset = 6f;
}