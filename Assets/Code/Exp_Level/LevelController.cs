using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int level = 1;
    [SerializeField] private int maxLevel = 10;
    [SerializeField] private int experience = 0;
    [SerializeField] private int experienceLevelUp;
    [SerializeField] private int experienceCrip = 15;

    [SerializeField] private TextMeshProUGUI levelText;

    private void CalculateExpForLevel(int level)
    {
        experienceLevelUp = (int)(100 * (Mathf.Pow(1.5f, level) - 1) / 0.5f);
    }

    void Start()
    {
        CalculateExpForLevel(level);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            experience += experienceCrip;
            Debug.Log("+15 experience");
        }

        if (experience >= experienceLevelUp && level < maxLevel)
        {
            level++;
            experience -= experienceLevelUp; 
            CalculateExpForLevel(level);
        }

        if (level >= maxLevel)
        {
            levelText.text = $"Level: {level} Experience: MAX";
            return;
        }

        levelText.text = $"Level: {level} Experience: {experience} / {experienceLevelUp}";
    }
}
