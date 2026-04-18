using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelData levelData; // Ссылка на ScriptableObject
    
    [SerializeField] private TextMeshProUGUI levelText;

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
            levelData.experience += levelData.experienceCrip;
            Debug.Log($"+{levelData.experienceCrip} experience");
        }

        if (levelData.experience >= levelData.experienceLevelUp && levelData.level < levelData.maxLevel)
        {
            levelData.level++;
            levelData.experience -= levelData.experienceLevelUp; 
            CalculateExpForLevel(levelData.level);
        }

        if (levelData.level >= levelData.maxLevel)
        {
            levelText.text = $"Level: {levelData.level} Experience: MAX";
            return;
        }

        levelText.text = $"Level: {levelData.level} Experience: {levelData.experience} / {levelData.experienceLevelUp}";
    }
}