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
    public void MakeDamage(Character attackTarget)
    {
        if (attackTarget == null)
            return;

        if (attackTarget.HealthComponent == null)
            return;

        if (attackTimer > 0)
            return; // 

        float distance = Vector3.Distance(
            selfCharacter.transform.position,
            attackTarget.transform.position
        );

        if (distance > AttackRange)
            return;

        attackTarget.HealthComponent.SetDamage((int)Damage);

        attackTimer = AttackCooldown; 
    }

    public void Tick(float deltaTime)
    {
        if (attackTimer > 0)
            attackTimer -= deltaTime;
    }
}