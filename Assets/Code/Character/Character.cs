using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] private CharacterData characterData;
    [SerializeField] private CharacterType characterType;

    public IHealthComponent HealthComponent { get; protected set;}
    public IMoveComponent MoveComponent { get; protected set;}
    public IAttackComponent AttackComponent { get; protected set;}
    public IInputProvider InputProvider { get; protected set; }
    public CharacterData CharacterData => characterData;

    public CharacterType CharacterType => characterType;

    public virtual Character CharacterTarget { get; }
    

    public virtual void Initialize()
    {
        MoveComponent = new MoveComponent();
        MoveComponent.Initialize(characterData);

        AttackComponent = new AttackComponent();
        AttackComponent.Initialize(characterData);
    }

    void Start()
    {
        Initialize();
    }

    protected abstract void Update();
    
}
