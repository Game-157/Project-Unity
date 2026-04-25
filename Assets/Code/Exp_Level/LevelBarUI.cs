using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarUI : MonoBehaviour
{
    [SerializeField] private Slider expSlider;
    [SerializeField] private LevelData levelData;
    
    public void SetValues(int current, int max)
    {
        levelData.experience = current;
        levelData.experienceLevelUp = max;

        UpdateUI();
    }

    void UpdateUI()
    {
        if (levelData.level >= levelData.maxLevel)
            return;
            
        expSlider.value = (float)levelData.experience / levelData.experienceLevelUp;
    }
}
