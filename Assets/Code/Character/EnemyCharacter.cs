using UnityEngine;

public class EnemyCharacter : Character
{
    [SerializeField] private AiState aiState;
    [SerializeField] private Character characterTarget;

    public override void Initialize()
    {
        base.Initialize();
        HealthComponent = new HealthComponent();
    }

    protected override void Update()
    {
        if (HealthComponent.Health <= 0)
        {
            return;
        }

        switch (aiState)
        {
            case AiState.Idle:
                 
                return;
            
            case AiState.MoveToTarget:
                Vector3 moveDirection = characterTarget.transform.position - transform.position;
                moveDirection.Normalize();

                MoveComponent.Move(moveDirection);
                MoveComponent.Rotation(moveDirection);

                return;
                
            case AiState.AttackToTarget:
                if (Vector3.Distance(transform.position, characterTarget.transform.position) <= AttackComponent.AttackRange)
                {
                    MoveComponent.Move(Vector3.zero);

                    Vector3 direction = characterTarget.transform.position - transform.position;
                    direction.y = 0;
                    if (direction != Vector3.zero)
                        MoveComponent.Rotation(direction.normalized);

                    AttackComponent.MakeDamage(characterTarget);
                    Debug.Log("Attack to Target invisible baseball bats");
                    return;
                }

                else
                {
                    Vector3 toTarget = characterTarget.transform.position - transform.position;
                    toTarget.Normalize();

                    MoveComponent.Move(toTarget);
                    MoveComponent.Rotation(toTarget);
                return;
                }
        }
    }
}
