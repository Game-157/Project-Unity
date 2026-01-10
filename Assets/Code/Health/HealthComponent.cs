using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthComponent : IHealthComponent
{
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;

    private Character selfCharacter;

    public event Action<Character> OnCharacterDeath;


    public float Health 
    { 
        get 
        { 
            return health; 
        } 

        private set 
        { 
            health = Mathf.Clamp(value, 0, MaxHealth); 
            if (health <= 0)
            {
                SetDeath();
            }
        }
    }

    public float MaxHealth 
    { 
        get 
        { 
            return maxHealth; 
        } 
    }

    public void SetDamage(int damage)
    {
        health -= damage;
        if (health < 0)
        {
            health = 0;
        }
    }

    private void SetDeath()
    {
        OnCharacterDeath?.Invoke(selfCharacter);
        Debug.Log("Character is dead");
        
    }

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
    }
}
