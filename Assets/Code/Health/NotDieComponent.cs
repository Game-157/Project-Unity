using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NotDieComponent : IHealthComponent
{
    public float Health => 100;
    public float MaxHealth => 100;

    public event Action<Character> OnCharacterDeath;

    public void Initialize(Character selfCharacter)
    {
        //throw new NotImplementedException();
    }

    public void SetDamage(int gamage)
    {
        Debug.Log("Not Die character");
    }
}
