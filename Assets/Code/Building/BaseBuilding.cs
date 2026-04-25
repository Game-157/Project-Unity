using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBuilding : MonoBehaviour
{
    public IHealthComponent HealthComponent { get; private set; }

    [SerializeField] private HealthComponent healthComponent;

    private void Awake()
    {
        HealthComponent = healthComponent;
        HealthComponent.Initialize(null); // можно без Character
    }
}
