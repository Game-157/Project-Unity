using UnityEngine;

public class HeroCharacter : Character
{
    public override void Initialize()
    {
        base.Initialize();
        HealthComponent = new NotDieComponent();
    } 

    protected override void Update()
    {
        if(HealthComponent.Health <= 0)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical).normalized;

        MoveComponent.Move(moveDirection);
    }
}
