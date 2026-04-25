using System;
using UnityEngine;

public class AttackComponent : MonoBehaviour, IAttackComponent
{
    private CharacterData characterData;
    private Character selfCharacter;
    public float Damage => 15;
    public float AttackRange => 3.0f;

    public float AttackCooldown => 1.0f;

    public float AttackTimer => attackTimer;

    private float attackTimer;

    public void Initialize(Character selfCharacter)
    {
        this.selfCharacter = selfCharacter;
        

    }
    public void MakeDamage(IHealthComponent attackTarget)
    {
        if (attackTarget == null)
            return;

        if (attackTimer > 0)
            return;

        attackTarget.SetDamage((int)Damage);

        attackTimer = AttackCooldown;

        Debug.Log($"Attacker {selfCharacter.CharacterType}, Attack: {attackTarget}, Target health: {attackTarget.Health}");
    }

    public void Tick(float deltaTime)
    {
        if (attackTimer > 0)
            attackTimer -= deltaTime;
    }
}