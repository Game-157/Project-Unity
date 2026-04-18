using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthDecorator : IHealthComponent
{
    protected IHealthComponent wrapped;
    protected Character selfCharacter;

    public event Action<Character> OnCharacterDeath
    {
        add    { wrapped.OnCharacterDeath += value; }
        remove { wrapped.OnCharacterDeath -= value; }
    }

    public HealthDecorator(IHealthComponent wrapped)
    {
        this.wrapped = wrapped;
    }

    public virtual float Health => wrapped.Health;
    public virtual float MaxHealth => wrapped.MaxHealth;

    public virtual void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        wrapped.Initialize(selfCharacter);
    }

    public virtual void SetDamage(int damage)
    {
        wrapped.SetDamage(damage);
    }

    public virtual void Kill()
    {
        wrapped.Kill();
    }
}
