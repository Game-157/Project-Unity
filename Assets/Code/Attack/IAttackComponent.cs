using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackComponent : ICharacterComponent
{
    float Damage { get; }
    float AttackRange { get; }

    float AttackCooldown { get; }

    float AttackTimer { get; }
    
    void Tick(float deltaTime);
    void MakeDamage(IHealthComponent attackTarget);
}

