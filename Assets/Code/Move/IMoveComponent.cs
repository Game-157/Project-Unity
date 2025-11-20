using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveComponent
{
    float Speed { get; set; }

    Vector3 Position { get; }

    void Move(Vector3 direction);

    void Rotation(Vector3 direction);

    void Initialize(CharacterData characterData);
}