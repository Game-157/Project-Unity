using System.Collections.Generic;
using UnityEngine;

public class HeroCharacter : Character
{
    private Character currentTarget;

    [Header("Target Settings")]
    [SerializeField] private float enterRadius = 5f; // вход в агр
    [SerializeField] private float exitRadius = 6f;  // выход из агра

    public override void Initialize()
    {
        base.Initialize();
        InputProvider = new HeroInputProvider();
    }

    protected override void Update()
    {
        if (HealthComponent == null || MoveComponent == null || InputProvider == null)
            return;

        if (HealthComponent.Health <= 0)
            return;

        AttackComponent.Tick(Time.deltaTime);

        Vector3 input = InputProvider.GetMoveDirection();

            Vector3 camForward = Camera.main.transform.forward;
            Vector3 camRight = Camera.main.transform.right;

            // убираем наклон камеры
            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            Vector3 moveDir = camForward * input.z + camRight * input.x;

        // --- ОБНОВЛЕНИЕ ЦЕЛИ ---
        UpdateTarget();

        // --- ПОВОРОТ ---
        if (currentTarget != null)
        {
            Vector3 lookDir = currentTarget.transform.position - transform.position;
            lookDir.y = 0;

            if (lookDir != Vector3.zero)
            {
                MoveComponent.Rotation(lookDir.normalized);
            }

            // (опционально) атака
            if (currentTarget != null)
            {
                AttackComponent.MakeDamage( currentTarget.HealthComponent);
            }
        }
        else
        {
            if (moveDir != Vector3.zero)
            {
                MoveComponent.Rotation(moveDir);
            }
        }

        // --- ДВИЖЕНИЕ ---
        MoveComponent.Move(moveDir);
    }

    private void UpdateTarget()
    {
        // если цели нет — ищем в радиусе
        if (currentTarget == null)
        {
            currentTarget = GetTargetInRadius(enterRadius);
        }
        else
        {
            // проверяем дистанцию до текущей цели
            float dist = Vector3.Distance(transform.position, currentTarget.transform.position);

            // если вышел за радиус — теряем цель
            if (dist > exitRadius || currentTarget.HealthComponent.Health <= 0)
            {
                currentTarget = null;
            }
        }
    }

    private Character GetTargetInRadius(float radius)
    {
        Character target = null;
        float minDistance = radius;

        List<Character> list = GameManager.Instance.CharacterFactory.ActiveCharacters;

        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].CharacterType == CharacterType.Hero)
                continue;

            float dist = Vector3.Distance(transform.position, list[i].transform.position);

            if (dist <= minDistance)
            {
                minDistance = dist;
                target = list[i];
            }
        }

        return target;
    }
}