using UnityEngine;

public class AttackComponent : IAttackComponent
{
    private CharacterData characterData;

    public float Damage => 10;
    public float AttackRange => 3.0f;

    public void Initialize(CharacterData characterData)
    {
        this.characterData = characterData;

    }
    public void MakeDamage(Character attacktarget)
    {
        if(Vector3.Distance(characterData.CharacterTransform.position, attacktarget.transform.position) <= AttackRange)
        attacktarget.HealthComponent.SetDamage((int)Damage);
    }
}