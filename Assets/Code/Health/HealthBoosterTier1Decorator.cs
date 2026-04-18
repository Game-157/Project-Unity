using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthBoosterTier1Decorator : HealthDecorator
{

    private float bonus;

    public HealthBoosterTier1Decorator(IHealthComponent wrapped, float bonus)
        : base(wrapped)
    {
        this.bonus = bonus;
    }

    public override float MaxHealth => wrapped.MaxHealth + bonus;

    public override float Health
    {
        get
        {
            
            return Mathf.Min(wrapped.Health + bonus, MaxHealth);
        }
    }
}

