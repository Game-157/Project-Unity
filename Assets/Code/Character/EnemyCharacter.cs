using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState aiState;
    [SerializeField] private Character characterTarget;

    [SerializeField] private float detectionRadius = 5f;

    private Character heroTarget;
    private BaseBuilding baseTarget;
    private Transform currentTarget;
    public override Character CharacterTarget => characterTarget;

    public override void Initialize()
    {
        base.Initialize();
        heroTarget = GameManager.Instance.CharacterFactory.Hero;
        baseTarget = GameManager.Instance.BaseBuilding;
    }

    protected override void Update()
    {
        if (HealthComponent.Health <= 0)
            return;

        AttackComponent.Tick(Time.deltaTime);

        UpdateTarget();

        if (currentTarget == null)
        {
            aiState = AiState.Idle;
            return;
        }

        float distance = Vector3.Distance(transform.position, currentTarget.position);

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

                Vector3 dir = (currentTarget.position - transform.position).normalized;

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

                Vector3 lookDir = currentTarget.position - transform.position;
                lookDir.y = 0;

                if (lookDir != Vector3.zero)
                    MoveComponent.Rotation(lookDir.normalized);

                // атака
                if (currentTarget.TryGetComponent<Character>(out var character))
                {
                    AttackComponent.MakeDamage(character.HealthComponent);
                }
                else if (currentTarget.TryGetComponent<BaseBuilding>(out var building))
                {
                    AttackComponent.MakeDamage(building.HealthComponent);
                }

                break;
        }
    }

    public void SetTarget(Character target)
    {
        characterTarget = target;
        aiState = AiState.MoveToTarget;
    }

    private void UpdateTarget()
    {
        if (heroTarget != null)
        {
            float dist = Vector3.Distance(transform.position, heroTarget.transform.position);

            if (dist <= detectionRadius && heroTarget.HealthComponent.Health > 0)
            {
                currentTarget = heroTarget.transform;
                return;
            }
        }

        currentTarget = baseTarget.transform;
    }
}
