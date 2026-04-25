using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelData levelData; // Ссылка на ScriptableObject
    
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private TextMeshProUGUI expIndicator;

    [SerializeField] private LevelBarUI levelBarUI;

    private void CalculateExpForLevel(int level)
    {
        levelData.experienceLevelUp = (int)(100 * (Mathf.Pow(1.5f, level) - 1) / 0.5f);
    }

    void Start()
    {
        CalculateExpForLevel(levelData.level);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            levelBarUI.SetValues(levelData.experience, levelData.experienceLevelUp);

            levelData.experience += levelData.experienceCrip;
            
            Debug.Log($"+{levelData.experienceCrip} experience");
        }

        if (levelData.experience >= levelData.experienceLevelUp && levelData.level < levelData.maxLevel)
        {
            
            levelData.level++;
            levelData.experience -= levelData.experienceLevelUp; 
            CalculateExpForLevel(levelData.level);
            levelBarUI.SetValues(levelData.experience, levelData.experienceLevelUp);
        }

        if (levelData.level >= levelData.maxLevel)
        {
            levelText.text = $"{levelData.level}";
            return;
        }

        levelText.text = $"{levelData.level}";
        expIndicator.text = $"{levelData.experience} / {levelData.experienceLevelUp}";
    }
}