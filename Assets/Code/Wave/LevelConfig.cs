using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level")]

public class LevelConfig : ScriptableObject
{
    public List<WaveConfig> waves;
}