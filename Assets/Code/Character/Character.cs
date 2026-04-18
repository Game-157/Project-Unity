using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CharacterType characterType;

    [SerializeField] protected HealthComponent healthComponent;

    [SerializeField] private MoveComponent moveComponent;
    [SerializeField] private AttackComponent attackComponent;


    public IMoveComponent MoveComponent { get; private set; }
    public IAttackComponent AttackComponent { get; private set; }
    public IHealthComponent HealthComponent { get; private set; }
    
    public IInputProvider InputProvider { get; protected set; }
    public CharacterData CharacterData => characterData;

    public CharacterType CharacterType => characterType;

    public virtual Character CharacterTarget { get; }
    

    public virtual void Initialize()
    {
        MoveComponent = moveComponent;
        AttackComponent = attackComponent;
        HealthComponent = healthComponent;

        moveComponent.Initialize(this);
        attackComponent.Initialize(this);
        healthComponent.Initialize(this);

        Debug.Log($"MOVE: {moveComponent}");
        Debug.Log($"ATTACK: {attackComponent}");
        Debug.Log($"HEALTH: {healthComponent}");
    }

    

    protected abstract void Update();
    

    public void SetHealthComponent(IHealthComponent newHealth)
    {
        HealthComponent = newHealth;
        HealthComponent.OnCharacterDeath += GameManager.Instance.CharacterDeathHandler;
    }
}
