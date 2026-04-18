using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState aiState;
    [SerializeField] private Character characterTarget;

    public override Character CharacterTarget => characterTarget;

    public override void Initialize()
    {
        base.Initialize();
        
    }

    protected override void Update()
    {
        if (HealthComponent.Health <= 0)
            return;

        if (characterTarget == null)
        {
            aiState = AiState.Idle;
            return;
        }

        float distance = Vector3.Distance(transform.position, characterTarget.transform.position);

        switch (aiState)
        {
            case AiState.Idle:
                aiState = AiState.MoveToTarget;
                break;

            case AiState.MoveToTarget:
                if (distance <= AttackComponent.AttackRange)
                {
                    aiState = AiState.AttackToTarget;
                    break;
                }

                Vector3 dir = (characterTarget.transform.position - transform.position).normalized;
                MoveComponent.Move(dir);
                MoveComponent.Rotation(dir);
                break;

            case AiState.AttackToTarget:
                if (distance > AttackComponent.AttackRange)
                {
                    aiState = AiState.MoveToTarget;
                    break;
                }

                MoveComponent.Move(Vector3.zero);

                Vector3 lookDir = characterTarget.transform.position - transform.position;
                lookDir.y = 0;

                if (lookDir != Vector3.zero)
                    MoveComponent.Rotation(lookDir.normalized);

                AttackComponent.MakeDamage(characterTarget);
                break;
        }
    }

    public void SetTarget(Character target)
    {
        characterTarget = target;
        aiState = AiState.MoveToTarget;
    }

}
