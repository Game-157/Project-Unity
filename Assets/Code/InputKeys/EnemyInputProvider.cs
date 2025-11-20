using UnityEngine;

public class EnemyInputProvider : IInputProvider
{
    private Character self;
    private Character target;

    public EnemyInputProvider(Character self, Character target)
    {
        this.self = self;
        this.target = target;
    }

    public Vector3 GetMoveDirection()
    {
        if (target == null) return Vector3.zero;
        Vector3 dir = target.transform.position - self.transform.position;
        dir.y = 0;
        return dir.normalized;
    }

    public bool IsAttackPressed()
    {
        if (target == null) return false;
        float distance = Vector3.Distance(self.transform.position, target.transform.position);
        return distance <= self.AttackComponent.AttackRange;
    }
}