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

                AttackComponent.MakeDamage(characterTarget);

                return;
                
            
        }
    }
}
