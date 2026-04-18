using System.Collections.Generic;
using UnityEngine;

public class HeroCharacter : Character
{
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

                float distance = Vector3.Distance(list[i].transform.position, transform.position);

                if (distance < minDistance)
                {
                    target = list[i];
                    minDistance = distance;
                }
            }

            return target;
        }
    }

    public override void Initialize()
    {
        base.Initialize();

        InputProvider = new HeroInputProvider();

        HealthComponent.Initialize(this);
        AttackComponent.Initialize(this);
    }

    protected override void Update()
    {
        if (HealthComponent == null || MoveComponent == null || InputProvider == null)
            return;

        if (HealthComponent.Health <= 0)
            return;

        Vector3 moveDir = InputProvider.GetMoveDirection();

        if (moveDir != Vector3.zero)
        {
            MoveComponent.Move(moveDir);
            MoveComponent.Rotation(moveDir);
        }

        Character target = CharacterTarget;

        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            MoveComponent.Rotation(direction);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                AttackComponent.MakeDamage(target);
            }
        }

        MoveComponent.Move(moveDir);
    }
}