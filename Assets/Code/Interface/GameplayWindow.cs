using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Slider healthSlider;

    [Space] [SerializeField] private Slider experienceSlider;

    [Space] [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text coinsText;

    public override void Initialize()
    {
        
    }

    protected override void OpenStart()
    {
        base.OpenStart();
        var hero = GameManager.Instance.HeroCharacter;
        UpdateHealthVisual(hero);
        hero.HealthComponent.OnHealthChanged += UpdateHealth;
    }
    
    protected override void CloseStart()
    {
        base.CloseStart();

        var hero = GameManager.Instance.Hero;
        if (hero == null)
        {
            return;
        }
        hero.Health.OnHealthChanged -= UpdateHealth;
    }

    private void UpdateHealthVisual(Character character)
    {
        int health = (int)character.HealthComponent.Health;
        int healthMax = character.HealthComponent.HealthMax;

        healthText.text = health + " / " + healthMax;
        healthSlider.MaxValue = healthMax;
        healthSlider.Value = health;
    }
}
