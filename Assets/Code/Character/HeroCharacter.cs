using System.Collections.Generic;
using UnityEngine;

public class HeroCharacter : Character
{
    [SerializeField] private Character characterTarget;

    public override Character CharacterTarget
    {
        get
        {
            Character target = null;
            float minDistance = float.MaxValue;
            List<Character> list = GameManager.Instance.CharacterFactory.ActiveCharacters;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].CharacterType == CharacterType.Hero)
                    continue;
                
                float distanceBetween = Vector3.Distance(list[i].transform.position, transform.position);
                if (distanceBetween < minDistance)
                {
                    target = list[i];
                    minDistance = distanceBetween;
                    
                }
            }

            return target;
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        HealthComponent = new NotDieComponent();
        HealthComponent.Initialize(this);
        AttackComponent = new AttackComponent();
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

        if (CharacterTarget == null)
        {
           MoveComponent.Rotation(moveDir);
        }
        else
        {
            Vector3 rotationDirection = CharacterTarget.transform.position - transform.position;
            MoveComponent.Rotation(moveDir);

            if (Input.GetKeyDown(KeyCode.Space))
                AttackComponent.MakeDamage(CharacterTarget);
                Debug.Log("Attack");
            

        }

        MoveComponent.Move(moveDir); 
    }
}
