using System;
public interface IHealthComponent :  ICharacterComponent
{
    public event Action<Character> OnCharacterDeath;

    float Health { get; }
    float MaxHealth { get; }

    void SetDamage(int damage);

    void Kill();
} 
 