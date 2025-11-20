public interface IHealthComponent
{
    float Health { get; }
    float MaxHealth { get; }

    void SetDamage(int damage);
} 
