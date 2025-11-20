using UnityEngine;

public class HeroCharacter : Character
{
    [SerializeField] private Character characterTarget;

    public override void Initialize()
    {
        base.Initialize();
        HealthComponent = new NotDieComponent();
        InputProvider = new HeroInputProvider();
    } 

    protected override void Update()
    {
        if (HealthComponent == null || MoveComponent == null || InputProvider == null)
        {
            return;
        }

        if(HealthComponent.Health <= 0)
        {
            return;
        }

       Vector3 moveDir = InputProvider.GetMoveDirection();
        if (moveDir != Vector3.zero)
        {
            MoveComponent.Move(moveDir);
            MoveComponent.Rotation(moveDir);
        }

    }
}
