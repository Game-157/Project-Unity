using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData")]
public class LevelData : ScriptableObject
{
    [Header("Level Settings")]
    public int level = 1;
    public int maxLevel = 10;
    public int experience = 0;
    public int experienceCrip = 15;

    [HideInInspector] public int experienceLevelUp;
    
    public void ResetData()
    {
        level = 1;
        experience = 0;
    }
}
