using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDieComponent : IHealthComponent
{
    public float Health => 100;
    public float MaxHealth => 100;

    public void SetDamage(int gamage)
    {
        Debug.Log("Not Die character");
    }
}
