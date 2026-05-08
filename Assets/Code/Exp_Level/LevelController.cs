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
        levelData.level = 1;
        levelData.experience = 0;
        CalculateExpForLevel(levelData.level);

    }

    public void UpdateLevelUI()
    {
        levelBarUI.SetValues(levelData.experience, levelData.experienceLevelUp);

        levelText.text = $"{levelData.level}";
        expIndicator.text = $"{levelData.experience} / {levelData.experienceLevelUp}";
    }

    public void AddExperience(int amount)
    {
        if (levelData.level >= levelData.maxLevel)
            return;

        levelData.experience += amount;

        Debug.Log($"+{amount} experience");

        while (levelData.experience >= levelData.experienceLevelUp)
        {
            levelData.experience -= levelData.experienceLevelUp;

            levelData.level++;

            CalculateExpForLevel(levelData.level);

            if (levelData.level >= levelData.maxLevel)
            {
                levelData.level = levelData.maxLevel;
                break;
            }
        }

        levelBarUI.SetValues(levelData.experience, levelData.experienceLevelUp);

        levelText.text = $"{levelData.level}";
        expIndicator.text = $"{levelData.experience} / {levelData.experienceLevelUp}";
    }
}