using System;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IHealthComponent
{
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;

    private Character selfCharacter;
    private bool isDead;

    public event Action<Character> OnCharacterDeath;

    public float Health => health;
    public float MaxHealth => maxHealth;

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        isDead = false;
        health = maxHealth;
    }

    public void SetDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0)
        {
            health = 0;
            Die();
        }
    }

    public void Kill()
    {
        if (isDead) return;

        health = 0;
        Die();
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        OnCharacterDeath?.Invoke(selfCharacter);
        Debug.Log("Character is dead");
    }
}